using olightvn.Common;
using olightvn.Models;
using olightvn.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T.Core.Filters;
using T.Core.Common;

namespace olightvn.Areas.Admin.Controllers
{
    public class ImageController : BaseController
    {
        readonly IImageRepository _image;
        public ImageController(IImageRepository image)
        {
            _image = image;
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public bool FilesUpload(FileModel file, Image image, string path, string fileNamePrefix)
        {
            return FilesUpload(file, image, path, fileNamePrefix, false);
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public bool FilesUpload(FileModel file, Image image, string path, string fileNamePrefix, bool onlyUploadFilePhisical)
        {
            List<FileModel> files = new List<FileModel>();
            files.Add(file);
            return FilesUpload(files, image, path, fileNamePrefix, onlyUploadFilePhisical);
        }
         [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public bool FilesUpload(List<FileModel> files, Image image, string path, string fileNamePrefix)
        {
            return FilesUpload(files, image, path, fileNamePrefix,false);
        }

        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public bool FilesUpload(List<FileModel> files, Image image, string path, string fileNamePrefix, bool onlyUploadFilePhisical)
        {
            bool result = false;

            if (files != null)
            {
                try
                {
                    foreach (FileModel file in files)
                    {
                        if (file.Base64.IsNotNullOrEmpty())
                        {

                            //convert from  base64 to byte array
                            byte[] byteArray = Convert.FromBase64String(file.Base64);
                            string fileName = GeneralFuncs.UploadFile(byteArray, file.Name, path, fileNamePrefix, image.Width, image.Height, out result, image.Signature);
                            if (result && !onlyUploadFilePhisical)
                            {
                                image.Name = fileName;
                                image.IsSelected = file.Selected;
                                image.ActiveStatus = 1;
                                result = _image.Insert(image, CurrentUserLogin);
                            }
                        }
                        else
                        {
                            if (!onlyUploadFilePhisical)
                            {
                                image.Name = file.Name;
                                image.IsSelected = file.Selected;
                                image.ActiveStatus = 1;
                                result = _image.Insert(image, CurrentUserLogin);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    T.Code.Common.LogManager.LogError("Upload file false: ");
                    T.Code.Common.LogManager.LogError(ex);
                    T.Code.Common.LogManager.LogError("File path: " + path);
                    T.Code.Common.LogManager.LogError("Files count: " + files.Count);
                }
                
            }
            else
            {
                T.Code.Common.LogManager.LogDebug("List files are empty");
            }
            return result;
        }

        public ActionResult FileUpload(Image image, string fileCollections)
        {
            List<FileModel> files = fileCollections.ConvertToFiles();
            string action = "management";
            string route = "image";
            image.ProductId = -1;
            image.Width = WebConfigurations.ImageSlideSize.Width;
            image.Height = WebConfigurations.ImageSlideSize.Height;
            bool result = FilesUpload(files, image, WebConfigurations.SliderPath, DateTime.Now.Ticks.ToStringToDefault());
            TempData["upload"] = result ? "Upload thành công." : "Lỗi trong quá trình upload.";
            return RedirectToAction(action, route);
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public bool Update(Image image)
        {
            var result = _image.Update(image, CurrentUserLogin);
            return result;
        }
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Management()
        {
            var result = _image.GetAll(-1);
            return View(result);
        }

        [HttpPost]
        [AuthorizationRequired(Permissions = "ModAll,AdminAll")]
        public ActionResult Delete(int id, string name)
        {
            var result = _image.Delete(id, CurrentUserLogin);
            if (result)
                System.IO.File.Delete(HttpContext.Server.MapPath(name));
            return Json(result);
        }

    }
}
