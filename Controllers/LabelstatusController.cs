using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API.Class;
using Project_API.Data;
using System.Text;
[ApiController]
[Route("api/[controller]")]
public class LabelstatusController : ControllerBase
{
    private readonly AppDbContext _db;
    public LabelstatusController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _db.labelstatuses.AsNoTracking()
            .Select(p => new Labelstatus
            {
                ID = p.ID,
                MAC = p.MAC,
                VARIANT = p.VARIANT,
            })
            .ToListAsync();
        return Ok(items);
    }
    // GET: /api/products/id 
    // với các kiểu dữ  liệu khác string thì  phải khai báo
    //public async Task<IActionResult> GetById(string id)
    //{
    //    var item = await _db.labelstatuses
    //        .FindAsync(id);
    //    return item is null ? NotFound() : Ok(item);
    //}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var item = await _db.labelstatuses
            .AsNoTracking()
            .Where(p => p.MAC == id)
            .Select(p => p.VARIANT)
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
}

