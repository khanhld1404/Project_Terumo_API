using System.ComponentModel.DataAnnotations;

namespace Project_API.Class
{
    public class Product
    {
        [Key]
        public Guid IDItem { get; set; }
        public string ItemCode { get; set; } = "";
        public string LotNo {  get; set; }
        public string Location { get; set; }
        public string HeThong { get; set; }
        public double? R_float1 { get; set; } //Qty
        public double? R_float2 {  get; set; } //QtyAllocate
        public double? R_float3 { get; set; } // Tồn khả dụng
    }
}
