using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CityInfo.Entities
{
    public class PointOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int cityid { get; set; }

        [ForeignKey("cityid")]
        [JsonIgnore]
        public City city { get; set; }

        //public PointOfInterest(string name)
        //{
        //    Name = name;
        //}
    }
}
