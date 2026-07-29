
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API.Data;
using Project_API.Class;
using EFCore.BulkExtensions;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductController(AppDbContext db) => _db = db;

    // GET: /api/products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _db.tblProducts.AsNoTracking()
            .Select(p => new Product
            {
                IDItem = p.IDItem,
                LotNo = p.LotNo,
                ItemCode = p.ItemCode,
                HeThong = p.HeThong,
                R_float1 = p.R_float1,
                R_float2 = p.R_float2,
            })
            .ToListAsync();
        return Ok(items);
    }

    // GET: /api/products/by-code/code
    //  Get ở đây chỉ lấy giá  trị  đầu tiên trong cơ sở dữ liệu khi so sánh với ItemCode với Lotno
    // Đang bị một chỗ là nếu không có tìm đc  thì nó trả về giá trị 000000000 hêt
    [HttpGet("{code}/{Lotno}/{Location}")]
    public async Task<IActionResult> GetById(string code, string Lotno, string Location)
    {
        try
        {
            var item = await _db.tblProducts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ItemCode == code && x.LotNo == Lotno && x.Remark2 == Location);

            if (item is null)
                return NotFound("Không tìm thấy sản phẩm tương ứng.");

            return Ok(item.IDItem);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //POST: /api/products
    // Nghiên cứu chỗ này có thể post bằng Product được không thay vì ProductPost
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductPost req)
    {

        Guid  newid;
        var allProductEinks = await _db.tblProducts.ToListAsync();
        var existedProduct = allProductEinks
                        .FirstOrDefault(s => s.ItemCode == req.ItemCode &&
                    s.LotNo == req.LotNo
                    && s.Remark2 == req.Location);
        if(existedProduct != null)
        {
            newid = existedProduct.IDItem;
        }
        else
        {
            newid = Guid.NewGuid();
            var p = new tblProduct
            {
                // Giả sử bảng có các cột Name, Price; sửa theo schema của bạn
                IDItem = newid,
                ItemCode = req.ItemCode,
                Remark2 = req.Location,
                LotNo = req.LotNo,
                QRCode = req.ItemCode + "%" + req.LotNo,
                HeThong = "EVS_Eink",
                R_float1 = req.R_float1,
                R_float2 = req.R_float2,
            };

            _db.tblProducts.Add(p);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Trả ra lỗi SQL thật (FK/NOT NULL/UNIQUE/len…)
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //return NoContent();
        //return CreatedAtAction(nameof(GetById), new { code = p.ItemCode }, p);
        return Ok(newid.ToString());
    }

    [HttpPost("Stock/")]
    public async Task<IActionResult> CreateProductStock([FromBody] List<ProductPost> productStockItems)
    {
        if (productStockItems == null || productStockItems.Count == 0)
        {
            return BadRequest(new { message = "Không có dữ liệu được gửi lên." });
        }

        var allProductEinks = await _db.tblProducts.ToListAsync();
        var productsToAdd = new List<tblProduct>();
        var productsToUpdate = new List<tblProduct>();
        var productsToDelete = new List<tblProduct>();

        try
        {
            // Phase 1: thêm/cập nhật sản phẩm mới
            foreach (var item in productStockItems)
            {
                // Kiểm tra có tồn tại sản phẩm ở server eink mà đồng thời tồn tại ở server local không
                var existedProduct = allProductEinks
                    .FirstOrDefault(s => s.ItemCode == item.ItemCode &&
                                    s.LotNo == item.LotNo && s.Remark2 == item.Location);

                Guid productId = Guid.NewGuid();
                if (existedProduct != null)
                {
                    existedProduct.R_float1 = item.R_float1;
                    existedProduct.R_float2 = item.R_float2;
                    productsToUpdate.Add(existedProduct);
                }
                else
                {
                    var newProduct = new tblProduct
                    {
                        IDItem = productId,
                        ItemCode = item.ItemCode,
                        LotNo = item.LotNo,
                        Remark2 = item.Location,
                        QRCode = item.ItemCode + '%' +item.LotNo,
                        HeThong = "EVS_Eink",
                        R_float1 = item.R_float1,
                        R_float2 = item.R_float2,
                    };
                    productsToAdd.Add(newProduct);
                }
            }

            if (productsToUpdate.Any())
            {
                await _db.BulkUpdateAsync(productsToUpdate);
            }
            if (productsToAdd.Any())
            {
                await _db.BulkInsertAsync(productsToAdd);
            }

            // Phase 2: xử lý xoá/unlink 
            // Lấy tất cả thông tin sản phẩm được lưu trên server Eink theo hệ thống chương trình của bản thân
            var oldProducts = _db.tblProducts
                  .Where(s => s.HeThong == "EVS_Eink")
                  .OrderBy(x => x.ItemCode)
                  .ToList();
            // Kiểm tra từng sản phẩm được thêm, sửa, xóa trong server Eink có tồn tại ở bảng dữ liệu cũ không 
            foreach (var itemDelete in oldProducts)
            {
                bool isExistInMES = productStockItems.Any(si =>
                    si.ItemCode == itemDelete.ItemCode &&
                    si.LotNo == itemDelete.LotNo
                    && si.Location == itemDelete.Remark2);
                //Nếu không tồn tại thì xóa nó đi
                if (!isExistInMES)
                {
                    // Kiểm tra sản phẩm đó có được kết nối đến thẻ eink không
                    var xoalink = await _db.links
                        .FirstOrDefaultAsync(s => s.ID == itemDelete.IDItem.ToString());

                    if (xoalink != null)
                    {
                        // Unlink thẻ khi không tồn tại product trong dữ liệu được đẩy lên
                        var macEink = xoalink.MAC;
                        var variantEink = xoalink.Variant;
                        var eslLabel = await _db.labelstatuses
                            .FirstOrDefaultAsync(x => x.MAC == macEink);
                        if (eslLabel == null)
                        {
                            return NotFound("\u26A0 Không tìm thấy thẻ Eink này.");
                        }
                        _db.links.Remove(xoalink);
                    }
                    // Xóa dữ liệu product đó khi không tồn tại
                    productsToDelete.Add(itemDelete);
                }
            }
            if (productsToDelete.Count > 0)
            {
                await _db.BulkDeleteAsync(productsToDelete);
            }


            //Lưu dữ liệu lại sau khi đã thực hiện việc thêm sửa xóa
            await _db.SaveChangesAsync();
            return Ok(new
            {
                message = "Nhập dữ liệu thành công.",
                timestamp = DateTime.Now
            });
        }
        catch (HttpRequestException ex)
        {
            return BadRequest($"HTTP error: {ex.Message}");
        }
        catch (TaskCanceledException)
        {
            return BadRequest("Request timed out.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Exception Error: {ex.Message}");
        }
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteById(string id, CancellationToken ct)
    //{

    //    // B1: Parse string -> Guid
    //    if (!Guid.TryParse(id, out var idGuid))
    //    {
    //        return BadRequest(new { message = "ID không hợp lệ. Vui lòng truyền chuỗi Guid (ví dụ: 2000284f-92a9-4e50-a765-06b29bbe2c44)." });
    //    }

    //    // Vì IDItem là khóa chính, dùng FindAsync sẽ tối ưu:
    //    var entity = await _db.tblProducts.FindAsync(new object[] { idGuid }, ct);  

    //    if (entity == null)
    //        return NotFound($"Không tìm thấy sản phẩm với IDItem = {idGuid}" );

    //    _db.tblProducts.Remove(entity);
    //    await _db.SaveChangesAsync(ct);

    //    return NoContent(); // 204 theo chuẩn khi xóa thành công
    //}

}


