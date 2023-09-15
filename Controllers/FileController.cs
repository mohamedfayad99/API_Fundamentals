using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.Controllers
{
    [Authorize]
    [ApiController]

    public class FileController : Controller
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider= fileExtensionContentTypeProvider ??
                throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }
        [HttpGet("{fileid}")]
        public ActionResult Getfile(string fileid)
        {
            var pathtofile = "Mahmmoud-Kinawy-AspNetFullStack-With-Angular-cv.pdf";

            if (!System.IO.File.Exists(pathtofile))
            {
                return NotFound();
            }

            if(!_fileExtensionContentTypeProvider.TryGetContentType(
                pathtofile,out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes=System.IO.File.ReadAllBytes(pathtofile);

            return File(bytes,contenttype,Path.GetFileName(pathtofile));
        }
    }
}
