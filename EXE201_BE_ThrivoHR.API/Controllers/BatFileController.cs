using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EXE201_BE_ThrivoHR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatFileController : ControllerBase
    {
        [HttpPost("run")]
        public IActionResult RunBatFile()
        {
            string filePath = @"C:\Users\xuanghi\Desktop\EXE201_BE_THRIVOHR\Run As Admin.bat";

            try
            {
                var processInfo = new ProcessStartInfo("cmd.exe", "/c " + filePath)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = new Process();
                process.StartInfo = processInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return Ok(new { Output = output, Error = error });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        //Test auto deploy
    }
}
