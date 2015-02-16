using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OCR.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // 64 bit
            if (IntPtr.Size == 8)
            {

                var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, @"bin\x64\");
                const string downloadUri = "http://wadescratch.blob.core.windows.net/public/x64/";

                var fileNames = new[] { "opencv_gpu2410.dll", "nppi64_65.dll", "cufft64_65.dll", "npps64_65.dll" };

                foreach (var fileName in fileNames)
                {
                    if (!File.Exists(Path.Combine(filePath, fileName)))
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(String.Concat(downloadUri, fileName), filePath + fileName);
                        }

                    }
                }
            }

            //32 bit
            if (IntPtr.Size == 4)
            {
                var filePath = Path.Combine(HttpRuntime.AppDomainAppPath, @"bin\x86\");
                const string downloadUri = "http://wadescratch.blob.core.windows.net/public/";

                var fileNames = new[] { "opencv_gpu2410.dll", "nppi32_65.dll", "cufft32_65.dll", "npps32_65.dll" };

                foreach (var fileName in fileNames)
                {
                    if (!File.Exists(Path.Combine(filePath, fileName)))
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(String.Concat(downloadUri, fileName), filePath + fileName);
                        }

                    }
                }
            }
        }
    }
}
