using System.ComponentModel.DataAnnotations;

namespace Project_API .Class
{
    public class Labelstatus
    {
        public string ID { get; set; }
        [Key]
        public string MAC {  get; set; }
        public string VARIANT { get; set; }
    }
}
