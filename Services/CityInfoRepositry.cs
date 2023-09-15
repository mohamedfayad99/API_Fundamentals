using CityInfo.DBContexts;
using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.ModelBinding;
using System.Xml.XPath;
using System.Linq;

namespace CityInfo.Services
{
    public class CityInfoRepositry : ICityInfoRepositry
    {
        private readonly ApplicationDb _context;
        public CityInfoRepositry(ApplicationDb context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<(IEnumerable<City>,Pagenationmetadate)> GetCitiesAsync(string? queryserach, int pagenumber, int pagesize)
        {
            //collection to start from
            var collection= _context.cities.Include(i => i.pointOfInterests) as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(queryserach))
            {
                queryserach = queryserach.Trim();
                collection=collection.Where(a => a.Name.Contains(queryserach) || 
                (a.Description.Contains(queryserach) && a.Description !=null));
            }

            var TotalItemCount = await collection.CountAsync();
            var paginationmetadata = new Pagenationmetadate(
                TotalItemCount, pagesize, pagenumber);

            var coolectiontoreturn= await collection.OrderBy(a => a.Name)
                .Skip(pagesize *(pagenumber - 1))
                .Take(pagesize)
                .ToListAsync();
            return (coolectiontoreturn, paginationmetadata);
        }

        public async Task<City> GetCityAsync(int cityId)
        {

            var query =  _context.cities.AsQueryable();
            query = query.Include(c => c.pointOfInterests);

            return await query.FirstOrDefaultAsync(c => c.Id == cityId);
        }
        
        public  async Task<bool> CityNameMatchesCityId(string? cityName, int? cityId)
        {
            return await _context.cities.AnyAsync(a => a.Name == cityName && a.Id == cityId);
        }

        //////////      crud oper
        public async Task<City> ADDCity(Upsertcity addcity)
        {

            City city = new()
            {
                Name = addcity.Name,
                Description = addcity.Description
            };
            _context.cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }
        public async Task<City> UpdateCity(int? id, Upsertcity Updatecity)
        {
            if (id == null || id == 0)
            {
                throw new ArgumentException("Invalid id. Please enter a valid id.");
            }
            var updatedcity = _context.cities.FirstOrDefault(f => f.Id == id);
            if (updatedcity == null)
            {
                throw new NotFoundException($"City with id {id} not found.");
            }
            updatedcity.Name = Updatecity.Name;
            updatedcity.Description = Updatecity.Description;
            _context.cities.Update(updatedcity);
            await _context.SaveChangesAsync();
            return updatedcity;

        }
        public async Task<City> GetCityByIdAsync(int? id)
        {
            var city = await _context.cities.FirstOrDefaultAsync(u => u.Id == id);

            if (city != null)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return null;
                }
            }

            return city;
        }

        public async Task<City> DeleteCity(int? id)
        {
            var city = await _context.cities.FirstOrDefaultAsync(f => f.Id == id);
            if (city == null)
            {
                return null; 
             }
            _context.cities.Remove(city);
            await _context.SaveChangesAsync();
            return city;
        }
    }
}
