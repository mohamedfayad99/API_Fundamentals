﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CityInfo.Entities
{
    public class City
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<PointOfInterest> pointOfInterests { get; set; } 
         //   = new List<PointOfInterest>();
        //public City(string name)
        //{
        //    Name = name;
        //}

    }
}
