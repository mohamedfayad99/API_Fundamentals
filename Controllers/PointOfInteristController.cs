using CityInfo.Services;
using CityInfo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CityInfo.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.Controllers
{
    [Route("api/pointOfInterist")]
    [Authorize]
    [ApiController]

    public class PointOfInteristController : ControllerBase
    {
        private readonly IPointInfoRepositry _pointRepository;
        private readonly ILogger<CitiesController> _logger;
        private readonly ImailService _mail;
        public PointOfInteristController(IPointInfoRepositry pointRepository, ILogger<CitiesController> logger, ImailService mail)
        {
            _pointRepository = pointRepository;
            _logger = logger;
            _mail = mail;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterest>>> GetPointOfInterestsAsync()
        {
            _logger.LogInformation("the all PointOfInterest reads");
            var PointOfInterestsAsync = await _pointRepository.GetPointOfInterestsAsync();
            return Ok(PointOfInterestsAsync);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetPointOfInterestAsync(int cityid, [FromRoute] int id)
        {
            try
            {
                var PointsOfInterest = await _pointRepository.GetPointOfInterestAsync(cityid, id);

                if (PointsOfInterest == null)
                {
                    return NotFound();
                }
                return Ok(JsonConvert.SerializeObject(PointsOfInterest, Formatting.None,
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


        [HttpPost]
        public async Task<ActionResult> createPointOfInterest(PointofinteristcityDTO pointofinteristcityDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(pointofinteristcityDTO);
            }
            await _pointRepository.ADDPointOfInterest(pointofinteristcityDTO);
            _logger.LogInformation($"the pointOfInterests that have id {_pointRepository.ADDPointOfInterest(pointofinteristcityDTO).Id} is created");
            return Ok(pointofinteristcityDTO);
        }

        [HttpPut("{id?}")]
        public async Task<ActionResult> PutpointofinteristcityDTO([FromRoute] int? id, PointofinteristcityDTO pointofinteristcityDTO)
        {
            _logger.LogInformation($"the pointofinterist that have id {id} is Updated");
            return Ok(await _pointRepository.UpdatePointOfInterestC(id, pointofinteristcityDTO));
        }


        [HttpPatch("{id?}")]
        public async Task<IActionResult> UpdatePatchPointOfInterest([FromRoute] int? id, JsonPatchDocument<PointofinteristcityDTO> jsonPatchDocument)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //check id
            var pointToUpdate = await _pointRepository.GetPointOfInterestByIdAsync(id);

            if (pointToUpdate == null)
            {
                return NotFound();
            }

            var pointofinterist = new PointofinteristcityDTO()
            {
                Name = pointToUpdate.Name,
                Description = pointToUpdate.Description,
                cityid = pointToUpdate.cityid,
            };

            jsonPatchDocument.ApplyTo(pointofinterist, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointToUpdate.Name = pointofinterist.Name;
            pointToUpdate.Description = pointofinterist.Description;
            pointToUpdate.cityid = pointofinterist.cityid;
            //to save changes  ❤️
            await _pointRepository.GetPointOfInterestByIdAsync(id);
            return Ok(pointToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePointOfInterest(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("Please enter a correct id");
            }

            var PointOfInterestTodelete = await _pointRepository.DeletePointOfInterest(id);

            if (PointOfInterestTodelete == null)
            {
                return NotFound("Please choose an existing city");
            }
            return Ok("Delete Done");
        }
    }
}
