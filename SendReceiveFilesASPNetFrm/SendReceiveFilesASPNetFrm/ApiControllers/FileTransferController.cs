using SendReceiveFilesASPNetFrm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SendReceiveFilesASPNetFrm.ApiControllers
{
    public class FileTransferController : ApiController
    {

        [HttpGet]
        [Route("api/FileTransfer/GetFiles")]
        public IHttpActionResult GetFiles()
        {

            var listFiles = new List<FileModel>();

            string path = HttpContext.Current.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                return new HttpActionResult(HttpStatusCode.NotFound, "Files not found.");
            }

            // Get the root directory and print out some information about it.
            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(path);
            // Get the files in the directory and print out some information about them.
            System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");
            int index = 1;
            foreach (System.IO.FileInfo fi in fileNames)
            {
                listFiles.Add(new FileModel { Id = index, Name = fi.Name, Path = fi.FullName });
                index++;
            }

            return Json(new
            {
                data = listFiles
            }) ;
               


        }


        [HttpPost]
        [Route("api/FileTransfer/UploadFile")]
        public  IHttpActionResult UploadFile()
        {
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var postedFile = httpRequest.Files["postedFile"];
                var path = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
            }
            else
            {
                return new HttpActionResult(HttpStatusCode.BadRequest, "File not uploaded.");
            }

            return new HttpActionResult(HttpStatusCode.Created, "File uploaded successfully.");
        }


    }
}