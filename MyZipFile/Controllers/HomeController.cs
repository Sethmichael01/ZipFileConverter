using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyZipFile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));
                ViewBag.message = "File uploaded successfully";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Zip(HttpPostedFileBase files)
        {
            try
            {
                string path = Server.MapPath("~/Uploads/");
                string archive_path = Server.MapPath("~/Archived/");

                if (!Directory.Exists(archive_path))
                {
                    Directory.CreateDirectory(archive_path);
                }
                //ZipFile.CreateFromDirectory(path, Path.GetFileName(), CompressionLevel.Fastest, true);
                //Directory.GetFiles(path, archive_path + @"\uploaded_" + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".zip");
                ZipFile.CreateFromDirectory
                   (path, archive_path, CompressionLevel.Fastest,true);
            }
            catch (Exception ex)
            {
                throw;
            }
            return View("Index");
        }
      
        public ActionResult About()
        {

            DirectoryInfo files = new DirectoryInfo(@"c:\Users\sethm\Desktop\CodeMaze\MyZipFile\MyZipFile\Archived");
            FileInfo[] allFiles = files.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in allFiles)
            {
                ViewBag.file = file.Name;
            }
            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}