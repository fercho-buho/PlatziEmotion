using EmotionPlatzi.web.Models;
using EmotionPlatzi.web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.web.Controllers
{
    public class EmoUploaderController : Controller
    {
        string serverFolderPath;
        EmotionHelper emoHelper;
        string key;
        EmotionPlatziwebContext db=new EmotionPlatziwebContext ();
        public EmoUploaderController()
        {
            
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        } 
        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            //if(file!=null&& file.ContentLength >0)
            if (file?.ContentLength > 0) {
                var pictureName = Guid.NewGuid().ToString();
                pictureName += Path.GetExtension(file.FileName);
                //routea un path de servidor a una ruta local
                var route = Server.MapPath(serverFolderPath );
                route = route +"/"+ pictureName;
                file.SaveAs(route);
                var emoPicture=await emoHelper.DetectAndExtractFacesAsync(file.InputStream );
                emoPicture.Name = file.FileName;
                //>Guardar un path relativo
                //emoPicture.Path = serverFolderPath + "/" + pictureName;
                //Interpolacion de cadenas
                emoPicture.Path = $"{ serverFolderPath}/{ pictureName }";
                db.EmoPictures.Add(emoPicture);
                await db.SaveChangesAsync();
                
                return RedirectToAction("Details", "EmoPictures",new { Id=emoPicture.Id });
            }
            
            return View();
        }
    }
    
}