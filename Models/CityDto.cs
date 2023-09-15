using CityInfo.Entities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CityInfo.Models
{
    public  class CityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }=String.Empty;
        public string? Description { get; set; }
        public ICollection<PointofinteristcityDTO> PointofinteristcityDTO { get; set; }


    }
}
