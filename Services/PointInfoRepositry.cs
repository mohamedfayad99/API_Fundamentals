using CityInfo.DBContexts;
using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Services
{
    public class PointInfoRepositry : IPointInfoRepositry
    {
        private readonly ApplicationDb _context;
        public PointInfoRepositry(ApplicationDb context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestsAsync()
        {
            var points = _context.pointOfInterests.ToList();
            //  return await _context.pointOfInterests.OrderBy(o => o.Name).ToListAsync();
            return points;
        }
        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityid, int PointOfInterestid)
        {
            if (PointOfInterestid != 0 && cityid == 0)
            {
                var result = await _context.pointOfInterests.FirstOrDefaultAsync(f => f.Id == PointOfInterestid);
                return result;

            }
            else
            {
                var result = _context.pointOfInterests.Where(w => w.cityid == cityid && w.Id == PointOfInterestid).FirstOrDefault();
                return result;
            }
        }

        //CRUD
        public async Task<PointOfInterest> ADDPointOfInterest(PointofinteristcityDTO pointofinteristcityDTO)
        {

            PointOfInterest pointOfInterest = new()
            {
                Name = pointofinteristcityDTO.Name,
                Description = pointofinteristcityDTO.Description,
                cityid = pointofinteristcityDTO.cityid
            };
            _context.pointOfInterests.Add(pointOfInterest);
            await _context.SaveChangesAsync();
            return pointOfInterest;
        }
        public async Task<PointOfInterest> UpdatePointOfInterestC(int? id, PointofinteristcityDTO pointofinteristcityDTO)
        {
            if (id == null || id == 0)
            {
                throw new ArgumentException("Invalid id. Please enter a valid id.");
            }
            var updatedpointOfInterests = _context.pointOfInterests.FirstOrDefault(f => f.Id == id);
            if (updatedpointOfInterests == null)
            {
                throw new NotFoundException($"pointOfInterests with id {id} not found.");
            }
            updatedpointOfInterests.Name = pointofinteristcityDTO.Name;
            updatedpointOfInterests.Description = pointofinteristcityDTO.Description;
            updatedpointOfInterests.cityid = pointofinteristcityDTO.cityid;
            _context.pointOfInterests.Update(updatedpointOfInterests);
            await _context.SaveChangesAsync();
            return updatedpointOfInterests;

        }

        public async Task<PointOfInterest> GetPointOfInterestByIdAsync(int? id)
        {
            var pointOfInterests = await _context.pointOfInterests.FirstOrDefaultAsync(u => u.Id == id);

            if (pointOfInterests != null)
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

            return pointOfInterests;
        }

        public async Task<string> DeletePointOfInterest(int? id)
        {
            var PointOfInterest = await _context.pointOfInterests.FirstOrDefaultAsync(f => f.Id == id);
            if (PointOfInterest == null)
            {
                return null;
            }
            _context.pointOfInterests.Remove(PointOfInterest);
            await _context.SaveChangesAsync();
            return "done";
        }

    }
}
