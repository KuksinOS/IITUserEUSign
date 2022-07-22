using SendReceiveFilesASPNetFrm.Data;
using SendReceiveFilesASPNetFrm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SendReceiveFilesASPNetFrm.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var listFiles = new List<FileModel>();

            string path = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                return View();
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
               

                return View(listFiles);
        }
               
        public ActionResult WebApi()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var postedFile = Request.Files["postedFile"];
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                TempData["Message"] = "File uploaded successfully.";
            }
            else
            {
                TempData["Message"] = "File not uploaded.";
            }

            return RedirectToAction("Index");
        }

        
    }

}
