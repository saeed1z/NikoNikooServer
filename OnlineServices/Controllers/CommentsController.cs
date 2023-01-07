using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnlineServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpPost]
        [Route("SaveImage")]
        public async Task<IActionResult> SaveImage(CommentModel comment)
        {
            try
            {
                string path = "wwwroot/Images/Tickets/Comments/";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string imageName = Guid.NewGuid() + comment.Extention;
                byte[] imageBytes = Convert.FromBase64String(comment.Base64);

                await System.IO.File.WriteAllBytesAsync(path + imageName, imageBytes);
                return Ok(new { Status = 0, ImageName = imageName });

            }
            catch 
            {
                return Ok(new { Status = -2, ImageName = "null.png" });
            }
        }
    }
}
