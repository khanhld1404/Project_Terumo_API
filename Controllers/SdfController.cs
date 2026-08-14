using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Project_API.Class;

[ApiController]
[Route("api/v1/[controller]")] // => /api/v1/sdf
public class SdfController : ControllerBase
{

    public SdfController()
    {
        // Bảo đảm nơi chứa thông tin tồn tại
        Directory.CreateDirectory(Cl_Connection.Root_data);
    }


    /// <summary>
    /// Upload file (multipart/form-data, key = "file")
    /// </summary>
    /// 

    [HttpPost]
    [RequestSizeLimit(524288000)]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string name)
    {
        if (file == null || file.Length == 0)
            return BadRequest("file missing");
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("name missing");

        var safeName = Path.GetFileName(name); // chống traversal
        var finalPath = Path.Combine(Cl_Connection.csv_folder, safeName);

        using (var fs = System.IO.File.Create(finalPath))
            await file.CopyToAsync(fs);

        return Created("/api/v1/sdf", new { ok = true });
    }

    /// <summary>
    /// Tải file
    /// </summary>
    [HttpGet] // GET /api/v1/sdf
    public IActionResult Download()
    {
        // đường dẫn chứa file cần tải về thiết bị quét mã 
        if (!System.IO.File.Exists(Cl_Connection.sdf_path))
            return NotFound();

        // "KeyenceData_latest.sdf" là gợi ý tên tải về, cái tên này lúc gọi về sẽ thay đổi tùy thuộc cách gọi
        const string downloadName = "KeyenceData_latest.sdf";
        return PhysicalFile(
            Cl_Connection.sdf_path,
            "application/octet-stream",
            fileDownloadName: downloadName,
            enableRangeProcessing: true
        );
    }
}

