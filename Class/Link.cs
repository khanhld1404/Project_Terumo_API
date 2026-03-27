using System.ComponentModel.DataAnnotations;

namespace Project_API.Class
{
    public class Link
    {
        public string? ID { get; set; }
        public string? Variant {  get; set; }

        [Key]
        public string MAC { get; set; }
    }
}
