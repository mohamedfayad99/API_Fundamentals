using CityInfo.Entities;
using CityInfo.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.Services
{
    public interface ICityInfoRepositry
    {
        Task<(IEnumerable<City>, Pagenationmetadate)> GetCitiesAsync(string? queryserach, int pagenuber, int pagesize);
        Task<City?> GetCityAsync(int id);
        Task<bool> CityNameMatchesCityId(string? cityName, int? cityId);
        Task<City> ADDCity(Upsertcity addcity);
        Task<City> UpdateCity(int? id, Upsertcity updatecity);
        Task<City> GetCityByIdAsync(int? id);
        Task<City> DeleteCity(int? id);
    }
}
