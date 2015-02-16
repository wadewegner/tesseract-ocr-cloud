using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;

namespace OCR.Web.Controllers
{
    public class OcrController : ApiController
    {
        private Tesseract _ocr;

        public async Task<string> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            var streamProvider = new MultipartFormDataStreamProvider(root);

            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var fileData = streamProvider.FileData[0];
            var fileInfo = new FileInfo(fileData.LocalFileName);
            var image = new Image<Bgr, byte>(fileInfo.FullName);
            var tessdata = Path.Combine(HttpRuntime.AppDomainAppPath, "tessdata");

            _ocr = new Tesseract(tessdata, "eng", Tesseract.OcrEngineMode.OEM_TESSERACT_CUBE_COMBINED);

            using (Image<Gray, byte> gray = image.Convert<Gray, Byte>())
            {
                _ocr.Recognize(gray);

                var text = _ocr.GetText();
                return text;
            }
        }
    }
}
