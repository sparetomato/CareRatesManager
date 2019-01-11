using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace CareRatesManager.Models
{
    [Table("PostcodeZones")]
    public class PostcodeModel
    {
        [Key]
        public int Id { get; set; }
        public string Postcode { get; set; }
        public string Zone { get; set; }
        public string RuralUrban { get; set; }
        public string Rate { get; set; }
        
        public int RateId { get; set; }

        [ForeignKey("RateId")]
        public virtual RatesModel RateDetails { get; set; }
        
        //
        //public virtual RatesModel RateDetails { get; set; }

        public string ToJSONString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Table("RateDetails")]
    public class RatesModel
    {

        [Key]
        public int RateId { get; set;}
        public string Zone { get; set; }
        public string Type { get; set; }
        [DataType("decimal(4,2)")]
        public decimal Rate { get; set; }

        //public ICollection<PostcodeModel> PostCodes { get; set; }
    }
}
