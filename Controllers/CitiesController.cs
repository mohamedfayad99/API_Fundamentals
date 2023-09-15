using CityInfo.Services;
using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.XPath;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AutoMapper;
using System.Web.Http.ModelBinding;
using Microsoft.VisualBasic;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CityInfo.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepositry _cityRepository;
        private readonly ILogger<CitiesController> _logger;
        private readonly ImailService _mail;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepositry repositrycontext, ILogger<CitiesController> logger
            , ImailService mail, IMapper mapper)
        {
            _cityRepository = repositrycontext;
            _logger = logger;
            _mail = mail;
            _mapper = mapper;
        }
        int maxpagesize = 20;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> Getcities(
            string? queryserach, int pagenumber = 1, int pagesize = 10)
        {
            _logger.LogInformation("the all city reads");
            if (pagesize > maxpagesize)
            {
                pagesize = maxpagesize;
            }

            var (allcities, pagenationmetadata) = await _cityRepository.GetCitiesAsync(queryserach, pagenumber, pagesize);
            Response.Headers.Add("X-pagination", System.Text.Json.JsonSerializer.Serialize(pagenationmetadata));
            // Response.Headers.Add("X-Pagination",JsonSerializer);
            return Ok(JsonConvert.SerializeObject(allcities, Formatting.None,
                       new JsonSerializerSettings()
                       {
                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                       }));
            //  return Ok( _mapper.Map<IEnumerable<CityDto>>(allcities));
        }
        /// <summary>
        /// getting my city
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a 200 getting  response if founded, or a 400 Bad Request , or a 404 if not found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Getcity(int id)
        {
            try
            {
                var citywithoutpoint = await _cityRepository.GetCityAsync(id);
                if (citywithoutpoint == null)
                {
                    return NotFound();
                }
                //return Ok(_mapper.Map<CityDto>(city));
                return Ok(JsonConvert.SerializeObject(citywithoutpoint, Formatting.None,
                   new JsonSerializerSettings()
                   {
                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                   }));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception when handling id {id} .", ex);
                return StatusCode(500, "Happen Problem");
            }
        }
        /// <summary>
        /// Create a new city.
        /// </summary>
        /// <param name="ct">The city data to be created.</param>
        /// <returns>Returns a 201 Created response if successful, or a 400 Bad Request response if there are validation errors.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> createcity(Upsertcity ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ct);
            }
            await _cityRepository.ADDCity(ct);
            _logger.LogInformation($"the City that have id {_cityRepository.ADDCity(ct).Id} is created");
            return Ok(_cityRepository.ADDCity(ct));
        }
        // <summary>
        /// Updates a city with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="updatedCity">The updated city data.</param>
        /// <returns>Returns a 200 OK response if successful, or a 404 Not Found response if the city with the given ID doesn't exist.</returns>


        [HttpPut("{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutCity([FromRoute] int? id, Upsertcity updatedcity)
        {
            _logger.LogInformation($"the City that have id {id} is Updated");
            return Ok(await _cityRepository.UpdateCity(id, updatedcity));
        }
        /// <summary>
        /// Partially updates a city with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the city to update.</param>
        /// <param name="patchCity">A JSON patch document containing partial updates for the city.</param>
        /// <returns>Returns a 200 OK response with the updated city data if successful, or a 400 Bad Request response if there are validation errors.</returns>

        [HttpPatch("{id?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePatchCity([FromRoute] int? id, JsonPatchDocument<Upsertcity> patchcity)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //check id
            var cityToUpdate = await _cityRepository.GetCityByIdAsync(id);

            if (cityToUpdate == null)
            {
                return BadRequest();
            }

            var cityDto = new Upsertcity()
            {
                Name = cityToUpdate.Name,
                Description = cityToUpdate.Description
            };

            patchcity.ApplyTo(cityDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cityToUpdate.Name = cityDto.Name;
            cityToUpdate.Description = cityDto.Description;
            //to save changes  ❤️
            await _cityRepository.GetCityByIdAsync(id);
            return Ok(cityToUpdate);

        }
        /// <summary>
        /// Deletes a city with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        /// <returns>
        /// Returns a 200 OK response with a success message if the city is deleted successfully,
        /// or a 400 Bad Request response if the ID is invalid, or a 404 Not Found response if the city doesn't exist.
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> Deletecity(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Please enter a correct id");
            }

            var cityTodelete = await _cityRepository.DeleteCity(id);

            if (cityTodelete == null)
            {
                return NotFound("Please choose an existing city");
            }
            return Ok("Delete Done");
        }

    }
}
