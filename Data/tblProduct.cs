using System;
using System.Collections.Generic;

namespace Project_API.Data
{
    public partial class tblProduct
    {
        public Guid IDItem { get; set; }
        public string? ItemCode { get; set; }
        public string? LotNo { get; set; }
        public int? Qty { get; set; }
        public string? QRCode { get; set; }
        public string? Barcode { get; set; }
        public string? Line { get; set; }
        public string? ItemType { get; set; }
        public string? TrangThaiSP { get; set; }
        public string? ViTri { get; set; }
        public string? WorkOder { get; set; }
        public string HeThong { get; set; } = null!;
        public string? MoTa { get; set; }
        public string? Remark { get; set; }
        public int? Qty_Plan { get; set; }
        public int? Qty_NG { get; set; }
        public int? Qty_OK { get; set; }
        public int? Qty_CDSau { get; set; }
        public int? ReserverQty { get; set; }
        public string? Image { get; set; }
        public string? ImagePath { get; set; }
        public int? TonChuaSD { get; set; }
        public string? SoMeSX { get; set; }
        public string? StatusHOLD { get; set; }
        public string? StatusUNDERQA { get; set; }
        public string? StatusPASSED { get; set; }
        public string? StatusNG { get; set; }
        public string? ChungLoaiSP { get; set; }
        public double? Qty_Float { get; set; }
        public string? TenSP { get; set; }
        public string? MaCD { get; set; }
        public string? TenCD { get; set; }
        public string? TrangThaiCD { get; set; }
        public string? NguoiThaoTac { get; set; }
        public string? HanSuDung { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ExpiryDate_Warning { get; set; }
        public string? SoThung { get; set; }
        public string? Template { get; set; }
        public string? Remark1 { get; set; }
        public string? Remark2 { get; set; }
        public string? Remark3 { get; set; }
        public string? Remark4 { get; set; }
        public string? Remark5 { get; set; }
        public int? R_int1 { get; set; }
        public int? R_int2 { get; set; }
        public int? R_int3 { get; set; }
        public double? R_float1 { get; set; }
        public double? R_float2 { get; set; }
        public double? R_float3 { get; set; }
        public DateTime? R_datetime1 { get; set; }
        public DateTime? R_datetime2 { get; set; }
        public DateTime? R_datetime3 { get; set; }
        public bool? R_bit1 { get; set; }
        public bool? R_bit2 { get; set; }
        public bool? R_bit3 { get; set; }
        public string? R_img1 { get; set; }
        public string? R_img2 { get; set; }
    }
}
