using System;
using System.IO;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {
            var Result = new JsonResult();
            //Request.RequestContext.
            try
            {
                var file = Request.Files[0];
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                file.SaveAs(path);
                Result.Data = new
                {
                    Success = true,
                    ImageURL = path
                };
            }
            catch (Exception ex)
            {
                Result.Data = new { Success = false, Message = ex.Message };
            }

            return Result;
        }
    }
}