using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using T.Core.Common;

namespace olightvn.Common
{
    public static class GeneralFuncs
    {

        public static string UploadFile(byte[] byteArray, string fileName, string path, string fileNamePrefix, int width, int height, out bool result, string textSignature = null, int fontSize = 48)
        {
            string fileNameNew = "Failed";
            result = false;
            try
            {
                // Find the fileUpload control
                fileNameNew = fileNamePrefix + fileName;

                // Specify the upload directory
                string directory = HttpContext.Current.Server.MapPath(path);

                // Check if the directory we want the image uploaded to actually exists or not
                if (!Directory.Exists(directory))
                {
                    // If it doesn't then we just create it before going any further
                    Directory.CreateDirectory(directory);
                }

                // Create a bitmap of the content of the fileUpload control in memory
                Bitmap originalBMP;
                using (var ms = new MemoryStream(byteArray))
                {
                    originalBMP = new Bitmap(ms);
                    Image newBMP = ResizeImage(originalBMP, width, height, textSignature, fontSize);

                    // Save the new graphic file to the server
                    newBMP.Save(directory + fileNameNew, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
               

                // Once finished with the bitmap objects, we deallocate them.
                //newBMP.Dispose();


                result = true;

                return fileNameNew;
            }
            catch (Exception)
            {
                return fileName;
                throw;
            }
        }
        public static string UploadFile(HttpPostedFileBase file, string path, string fileNamePrefix, out bool result, string textSignature = null, int fontSize = 48)
        {
            string fileName = "Failed";
            result = false;
            try
            {
                if (file != null)
                {
                    // Find the fileUpload control
                    fileName = fileNamePrefix + file.FileName;

                    // Specify the upload directory
                    string directory = HttpContext.Current.Server.MapPath(path);

                    // Check if the directory we want the image uploaded to actually exists or not
                    if (!Directory.Exists(directory))
                    {
                        // If it doesn't then we just create it before going any further
                        Directory.CreateDirectory(directory);
                    }

                    // Create a bitmap of the content of the fileUpload control in memory
                    Bitmap originalBMP = new Bitmap(file.InputStream);
                    Image newBMP = ResizeImage(originalBMP, 960, 640, textSignature, fontSize);

                    // Save the new graphic file to the server
                    newBMP.Save(directory + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // Once finished with the bitmap objects, we deallocate them.
                    newBMP.Dispose();


                    result = true;
                }

                return fileName;
            }
            catch (Exception)
            {
                return fileName;
                throw;
            }

        }

        private static Image ResizeImage(Image originalBMP, int maxWith, int maxHeight, string textSignature, int fontSize)
        {
            // Calculate the new image dimensions
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            double sngRatio = (double)origWidth / origHeight;
            int newWidth = origWidth;
            int newHeight = origHeight;

            if (origHeight < maxHeight && origWidth < maxWith)
                return originalBMP;

            if (origHeight > maxHeight)
            {
                newHeight = maxHeight;
                newWidth = (int)(newHeight * sngRatio);
            }
            else if (origWidth > maxWith)
            {
                newWidth = maxWith;
                newHeight = (int)(newWidth / sngRatio);
            }

            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
            //Set the resolution for new image
            newBMP.SetResolution(72, 72);

            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);

            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            oGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (!string.IsNullOrEmpty(textSignature))
            {
                RectangleF rectf = new RectangleF(100, 90, 400, 150);
                oGraphics.DrawString(textSignature, new Font("Tahoma", fontSize > 0 ? fontSize : 48), Brushes.LightBlue, rectf);
            }
            oGraphics.Flush();

            // Draw the new graphic based on the resized bitmap
            // oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            oGraphics.Dispose();
            return newBMP;
        }

        public static bool CheckPermission(string permission)
        {
            bool result = SessionManager.UserPermissions.Contains(permission);
            return result;
        }

        public static string ReadFile(string path)
        {
            string content = string.Empty;
            string LocalPath = HttpContext.Current.Server.MapPath("\\") + path;
            if (System.IO.File.Exists(LocalPath))
            {
                content = System.IO.File.ReadAllText(LocalPath);
            }
            return content;
        }
        public static XmlDocument ReadXML(string path)
        {
            XmlDocument xdoc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("\\") + path;
            if (System.IO.File.Exists(xmlPath))
            {
                xdoc.Load(xmlPath);
            }
            return xdoc;
        }

        public static Dictionary<string, string> GetExtendSettings()
        {
            Dictionary<string, string> extends = new Dictionary<string, string>();
            XmlDocument xdoc = ReadXML("App_Data\\ExtendSetting.xml");
            XmlNode node = default(XmlNode);
            node = xdoc.SelectSingleNode("settings/setting[@SiteId='" + SessionManager.SiteId + "']");

            XmlNodeList nodeExtend = node.SelectNodes("extend");
            foreach (XmlNode n in nodeExtend)
            {
                string key = n.Attributes["key"].Value;
                string value = n.Attributes["value"].Value;

                extends.Add(key, value);
            }
            return extends;
        }
        public static string GetSettingByKey(string keyName)
        {
            Dictionary<string, string> extends = SessionManager.ExtendSettings;
            string value = string.Empty;
            extends.TryGetValue(keyName, out value);
            return value;
        }

        public static XmlNode GetEmailFormat()
        {
            Dictionary<string, string> extends = new Dictionary<string, string>();
            XmlDocument xdoc = ReadXML("App_Data\\emailformat.xml");
            XmlNode node = xdoc.SelectSingleNode("MailFormats/MailFormat[@SiteId='" + SessionManager.SiteId + "']");

            return node;
        }
        public static string GetEmailFormatByKey(string keyName)
        {
            XmlNode email = GetEmailFormat();
            string value = email.SelectSingleNode(keyName).InnerText;
            return value;
        }

        public static string GetConfigValueByKey(string keyName)
        {
            Models.Configuration config = SessionManager.CurrentSite.Configs.Where(_ => _.Key == keyName).FirstOrDefault();
            if (config != null)
            {
                return config.Value;
            }
            return string.Empty;
        }

        public static int TotalSumPriceQty(DataTable data, string key1, string key2 )
        {
            int sum = 0;
            if(!data.HasData() || key1.IsNotNullOrEmpty() || key2.IsNotNullOrEmpty())
            {
                return 0;
            }
            foreach(DataRow row in data.Rows)
            {
                sum += row[key1].ToInt() * row[key2].ToInt();
            }
            return sum;
        }
    }
}