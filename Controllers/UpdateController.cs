using Microsoft.AspNetCore.Mvc;
using Project_API.Class;
using System;
using System.IO;
namespace Project_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        // Lấy thông tin version hiện tại
        [HttpGet]
        public IActionResult Update()
        {
            // đường dẫn đến file chứa thông tin version
            if (!System.IO.File.Exists(Cl_Connection.server_version))
            {
                return NotFound();
            } ;
            //Đọc dữ liệu có trong file
            string sv_version = File.Re

            // Tải file về
            // "KeyenceData_latest.sdf" là gợi ý tên tải về, cái tên này lúc gọi về sẽ thay đổi tùy thuộc cách gọi
            const string downloadName = "Keyence_Program_Version.txt";
            return PhysicalFile(
                Cl_Connection.sdf_path,
                "application/octet-stream",
                fileDownloadName: downloadName,
                enableRangeProcessing: true
            );
        }
    }
}
