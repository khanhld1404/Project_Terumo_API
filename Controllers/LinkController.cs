using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API.Class;
using Project_API.Data;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class LinkController : ControllerBase
{
    private readonly AppDbContext _db;
    public LinkController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _db.links.AsNoTracking()
            .Select(p => new Link
            {
                ID = p.ID,
                Variant = p.Variant,
                MAC = p.MAC,
            })
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("by-id/{id}")] // với các kiểu dữ  liệu khác string thì  phải khai báo id : int
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var item = await _db.links
            .AsNoTracking() 
            .Where(x => x.ID == id)
            .Select(p => new Get_Link
            {

                Variant = p.Variant,
                MAC = p.MAC
            })
            .FirstOrDefaultAsync();
            return item is null ? NotFound("Không tìm thấy link cần tìm!") : Ok(item);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("by-mac/{id}")]
    public async Task<IActionResult> GetByMAC(string id)
    {
        try
        {
            var item = await _db.links
            .AsNoTracking()
            .Where(p => p.MAC == id)
            .Select(p => p.Variant)
            .FirstOrDefaultAsync();

            if (item is null)
                return NoContent();

            return Content(item ?? string.Empty, "text/plain", Encoding.UTF8);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    //POST: /api/products
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Link req)
    {
        var alllink =  await _db.links.ToListAsync();
        // Map DTO -> entity phù hợp với cột trong bảng
        var p = new link
        {
            // Giả sử bảng có các cột Name, Price; sửa theo schema của bạn
            ID = req.ID,
            Variant = req.Variant,
            MAC = req.MAC,
        };
        var macExists = await _db.links.AnyAsync(x => x.MAC == req.MAC);
        if (macExists)
            return Conflict("Thẻ elink đã được sử dụng!");
        _db.links.Add(p);

        await _db.SaveChangesAsync();

        //return NoContent();
        return CreatedAtAction(nameof(GetById), new { id = p.MAC }, p);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByMAC(string id, CancellationToken ct)
    {
        var entity = await _db.links.FindAsync(new object[] { id }, ct);

        if (entity == null)
            return NotFound($"Không tìm thấy thẻ elink với MAC = {id}");

        _db.links.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return NoContent(); // 204 theo chuẩn khi xóa thành công
    }
}

