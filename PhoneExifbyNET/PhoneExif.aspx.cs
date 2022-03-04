using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using ExifLibrary;

namespace PhoneExifbyNET
{
    public partial class PhoneExif : System.Web.UI.Page
    {
        public string _savePath 
        {
            get { return HiddenField1.Value; }
            set { HiddenField1.Value = value; }
        }

        public string _savePath2
        {
            get { return HiddenField2.Value; }
            set { HiddenField2.Value = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string savePath = @"~/UploadImg/";

            if (FileUpload1.HasFile)
            {
                // Get the name of the file to upload.
                string fileName = FileUpload1.FileName;

                // Append the name of the file to upload to the path.
                savePath = Server.MapPath(savePath + fileName);

                _savePath = savePath;

                FileUpload1.SaveAs(savePath);

                Label1.Text = GetPhotoEXIF(savePath);
            }
            else
            {
                Response.Write("alert('請上傳檔案！')");
                return;
            }


        }

        private string GetPhotoEXIF(string picPath)
        {
            var fileSize = new FileInfo(picPath).Length;

            var file = ImageFile.FromFile(picPath);

            // the type of the ISO speed rating tag value is unsigned short
            // see documentation for tag data types
            var isoTag = file.Properties.Get<ExifUShort>(ExifTag.ISOSpeedRatings);

            // the flash tag's value is an enum
            var flashTag = file.Properties.Get<ExifEnumProperty<Flash>>(ExifTag.Flash);

            // GPS latitude is a custom type with three rational values
            // representing degrees/minutes/seconds of the latitude 
            var latTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLatitude);

            var lngTag = file.Properties.Get<GPSLatitudeLongitude>(ExifTag.GPSLongitude);

            return "fileSize：" + fileSize + "<br />經度：" + latTag + "<br />" + "緯度：" + lngTag;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(_savePath);
            float aspectRatio = (float)image.Size.Width / (float)image.Size.Height;
            int newHeight = 200;
            int newWidth = Convert.ToInt32(aspectRatio * newHeight);
            Bitmap thumbBitmap = new Bitmap(newWidth, newHeight);
            Graphics thumbGraph = Graphics.FromImage(thumbBitmap);

            thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            _savePath2 = Server.MapPath("~/ResizeImg/" + new FileInfo(_savePath).Name);

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbBitmap.Save(_savePath2, System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbGraph.Dispose();
            thumbBitmap.Dispose();
            image.Dispose();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label2.Text = GetPhotoEXIF(_savePath2);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            var savePath = Server.MapPath("~/WriteImg/" + new FileInfo(_savePath2).Name);
            var file = ImageFile.FromFile(_savePath2);


            file.Properties.Set(ExifTag.GPSLatitude, 111, 222, 333);
            file.Properties.Set(ExifTag.GPSLongitude, 111, 222, 333);

            file.Save(savePath);

            Label3.Text = GetPhotoEXIF(savePath);

        }
    }
}