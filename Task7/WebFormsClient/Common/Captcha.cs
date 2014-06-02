using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient.Common
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    public class Captcha : ICaptcha
    {
        protected readonly string AllowedChars;

		private const int IMAGE_WIDTH = 200;
		private const int IMAGE_HEIGHT = 70;
		private const string FONT_FAMILY = "Arial";
		private static readonly Brush _foreground = Brushes.Navy;
		private static readonly Color _background = Color.Silver;

		private const int WARP_FACTOR = 5;
		private const double X_AMP = WARP_FACTOR * IMAGE_WIDTH / 100;
		private const double Y_AMP = WARP_FACTOR * IMAGE_HEIGHT / 85;
		private const double X_FREQ = 2 * Math.PI / IMAGE_WIDTH;
		private const double Y_FREQ = 2 * Math.PI / IMAGE_HEIGHT;

		private const string DEFAULT_CAPTCHA_CHARS = "абвгдеклмн12456789";

		///<summary>
		/// Сгенерировать решение капчи
		///</summary>
		///<param name="allowedChars">Допустимые символы капчи</param>
		///<returns>решение капчи</returns>
		public string GenerateSolution(string allowedChars)
		{
				if (string.IsNullOrEmpty(allowedChars))
				{
					allowedChars = DEFAULT_CAPTCHA_CHARS;
				}

				var rng = new Random();
				int length = rng.Next(5, 7);
				var buf = new char[length];
				for (int i = 0; i < length; i++)
				{
					buf[i] = allowedChars[rng.Next(0, allowedChars.Length - 1)];
				}
				return new string(buf);
		}

		/// <summary>
		/// Генерация картинки по тексту решения
		/// </summary>
		/// <param name="solution">Текст для деформации</param>
		/// <returns>Объект потока картинки в формате PNG(!)</returns>
		public Stream GenerateImage(string solution)
		{
			Stream stream = new MemoryStream();
			if (solution != null)
			{
				using (var bmp = new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT))
				{
					using (Graphics g = Graphics.FromImage(bmp))
					{
						using (var font = new Font(FONT_FAMILY, 1f))
						{
							g.Clear(_background);
							SizeF finalSize;
							SizeF testSize = g.MeasureString(solution, font);
							float bestFontSize = Math.Min(IMAGE_WIDTH / testSize.Width, IMAGE_HEIGHT / testSize.Height) * 0.95f;
							using (var finalFont = new Font(FONT_FAMILY, bestFontSize))
							{
								finalSize = g.MeasureString(solution, finalFont);
							}
							g.PageUnit = GraphicsUnit.Point;
							var textTopLeft = new PointF((IMAGE_WIDTH - finalSize.Width) / 2, (IMAGE_HEIGHT - finalSize.Height) / 2);
							using (var path = new GraphicsPath())
							{
								path.AddString(solution, new FontFamily(FONT_FAMILY), 0, bestFontSize, textTopLeft, StringFormat.GenericDefault);
								g.SmoothingMode = SmoothingMode.HighQuality;
								g.FillPath(_foreground, DeformPath(path));
								g.Flush();
								bmp.Save(stream, ImageFormat.Png);
								stream.Seek(0, SeekOrigin.Begin);
							}
						}
					}
				}
			}
			else
			{
				return null;
			}
			return stream;
		}

		private static GraphicsPath DeformPath(GraphicsPath path)
		{
			var deformed = new PointF[path.PathPoints.Length];
			var rng = new Random();
			double xSeed = rng.NextDouble() * 2 * Math.PI;
			double ySeed = rng.NextDouble() * 2 * Math.PI;
			for (int i = 0; i < path.PathPoints.Length; i++)
			{
				PointF original = path.PathPoints[i];
				double val = X_FREQ * original.X + Y_FREQ * original.Y;
				int xOffset = (int)(X_AMP * Math.Sin(val + xSeed));
				int yOffset = (int)(Y_AMP * Math.Sin(val + ySeed));
				deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
			}
			return new GraphicsPath(deformed, path.PathTypes);
		}
	}
}