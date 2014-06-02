using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient.Handlers
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    using CaptchaDotNet2;

    using WebFormsClient.Common;

    /// <summary>
    /// Summary description for CaptchaHandler
    /// </summary>
    public class CaptchaHandler : IHttpHandler
    {
        private const int IMAGE_WIDTH = 200;
		private const int IMAGE_HEIGHT = 70;
		private const string FONT_FAMILY = "Arial";
		private static readonly Brush _foreground = Brushes.Navy;
		private static readonly Color _background = Color.Silver;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.BufferOutput = false;
            string strCaptcha = context.Request.QueryString["c"].ToString();

            
            /*using (var stream = new MemoryStream())
            {
                using (var bmp = new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        using (var font = new Font(FONT_FAMILY, 1f))
                        {
                            g.Clear(_background);
                            SizeF finalSize;
                            SizeF testSize = g.MeasureString(strCaptcha, font);
                            float bestFontSize = Math.Min(IMAGE_WIDTH / testSize.Width, IMAGE_HEIGHT / testSize.Height)
                                                 * 0.95f;
                            using (var finalFont = new Font(FONT_FAMILY, bestFontSize))
                            {
                                finalSize = g.MeasureString(strCaptcha, finalFont);
                            }
                            g.PageUnit = GraphicsUnit.Point;
                            var textTopLeft = new PointF(
                                (IMAGE_WIDTH - finalSize.Width) / 2, (IMAGE_HEIGHT - finalSize.Height) / 2);
                            using (var path = new GraphicsPath())
                            {
                                path.AddString(
                                    strCaptcha,
                                    new FontFamily(FONT_FAMILY),
                                    0,
                                    bestFontSize,
                                    textTopLeft,
                                    StringFormat.GenericDefault);
                                g.SmoothingMode = SmoothingMode.HighQuality;
                                g.FillPath(_foreground, path);
                                g.Flush();
                                bmp.Save(stream, ImageFormat.Png);
                                stream.Seek(0, SeekOrigin.Begin);
                            }
                        }
                    }
                }
                }*/
            // Generate image
            CaptchaImage ci = new CaptchaImage(strCaptcha, Color.White, 120, 50);

            // Return
            ci.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            // Dispose
            ci.Dispose();
            }
            
            /* if (context.Session["Captcha"].ToString() != null)
             {
                 strCaptcha = context.Session["Captcha"].ToString();
             }*/
        

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}