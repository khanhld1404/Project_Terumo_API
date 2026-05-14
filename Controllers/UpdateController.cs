using Microsoft.AspNetCore.Mvc;
using Project_API.Class;
using System;
using System.Collections.Generic;
using System.IO;

namespace Project_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        // GET api/update/version
        [HttpGet("version")]
        public IActionResult Get_Version()
        {
            if (!System.IO.File.Exists(Cl_Connection.server_version))
            {
                return NotFound("Không tìm thấy file version.");
            }

            string sv_version = System.IO.File.ReadAllText(Cl_Connection.server_version);

            return string.IsNullOrWhiteSpace(sv_version)
                ? NotFound("File version rỗng.")
                : Content(sv_version.Trim(), "text/plain");
        }

        // GET api/update/files
        // Trả về danh sách tên file, mỗi dòng 1 file
        [HttpGet("files")]
        public IActionResult Get_Files()
        {
            // Chỉ lấy file trong thư mục gốc, không lấy thư mục con
            string[] files = Directory.GetFiles(Cl_Connection.download_folder);

            List<string> fileNames = new List<string>();

            foreach (string filePath in files)
            {
                fileNames.Add(Path.GetFileName(filePath));
            }

            string content = string.Join("\n", fileNames.ToArray());

            return Content(content, "text/plain");
        }

        // GET api/update/download/{fileName}
        [HttpGet("download/{fileName}")]
        public IActionResult Download_File(string fileName)
        {
            // Chặn truyền đường dẫn kiểu ../abc.exe
            fileName = Path.GetFileName(fileName);

            string fullPath = Path.Combine(Cl_Connection.download_folder, fileName);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound("Không tìm thấy file.");
            }

            return PhysicalFile(fullPath, "application/octet-stream", fileName);
        }
    }
}