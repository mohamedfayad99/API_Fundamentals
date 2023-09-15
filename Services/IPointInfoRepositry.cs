using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.Services
{
    public interface IPointInfoRepositry
    {

        Task<IEnumerable<PointOfInterest>> GetPointOfInterestsAsync();

        Task<PointOfInterest?> GetPointOfInterestAsync(int cityid, int PointOfInterestid);
        Task<PointOfInterest> ADDPointOfInterest(PointofinteristcityDTO pointofinteristcityDTO);
        Task<PointOfInterest> UpdatePointOfInterestC(int? id, PointofinteristcityDTO pointofinteristcityDTO);
        Task<PointOfInterest> GetPointOfInterestByIdAsync(int? id);
        Task<string> DeletePointOfInterest(int? id);
    }
}
