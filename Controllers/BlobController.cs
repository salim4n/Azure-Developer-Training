using AzureBlobStorage.Models;
using AzureBlobStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorage.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Manage(string containerName)
        {
            var blobObjs = await _blobService.GetAllBlobs(containerName);
            return View(blobObjs);
        }


        [HttpGet]
        public  IActionResult AddFile(string containerName)
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(string containerName,Blob blob,IFormFile file)
        {
            if (file == null || file.Length < 1)
                return View();
            //filename - xps_image2.png
            //new filename - xps_image2_GUID.png
            var fileName = Path.GetFileNameWithoutExtension(file.Name)+"_"+Guid.NewGuid() + Path.GetExtension(file.FileName);
            var result = await _blobService.UploadBlob(fileName, file, containerName, blob);
            if (result)
                return RedirectToAction("Index", "Container");

            return View();
            
        }

        [HttpGet]
        public async Task<IActionResult> ViewFile(string name, string containerName)
        {

            return Redirect(await _blobService.GetBlob(name,containerName));

        }

        
        public async Task<IActionResult> DeleteFile(string name, string containerName)
        {
            await _blobService.DeleteBlob(name, containerName);
            return RedirectToAction("Index", "Container");

        }
    }
}
