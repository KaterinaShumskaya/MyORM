using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient.Handlers
{
    using WebFormsClient.Common;

    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/png";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.BufferOutput = false;
            string strCaptcha = new Captcha().GenerateSolution(null);

            /* if (context.Session["Captcha"].ToString() != null)
             {
                 strCaptcha = context.Session["Captcha"].ToString();
             }*/

            var ci = new Captcha().GenerateImage(strCaptcha);
            ci.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}