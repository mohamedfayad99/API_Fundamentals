using CityInfo.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Models
{
    public class PointofinteristcityDTO
    {

        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }

        public int cityid { get; set; }

    }

}
