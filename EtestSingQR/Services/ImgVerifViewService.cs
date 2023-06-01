using SkiaSharp;

namespace EtestSingQR.Services
{
    public class ImgVerifViewService : IImgVerifService
    {
        /// <summary>
        /// 產生圖片
        /// </summary>
        /// <returns>圖片base64碼</returns>
        public ImgVerifDate GetImgVerif()
        {
            ImgVerifDate MyImgVerifDate = new ImgVerifDate();
            MyImgVerifDate.CheckCode = GenerateCheckCode();
            byte[]? ReImgdata = null;

            //圖片大小簡易寫法
            SKBitmap bmp = new SKBitmap(95, 35);
            SKCanvas canvas = new SKCanvas(bmp);

            try
            {
                //生成隨機生成器
                Random random = new Random();

                //清空圖片背景色
                canvas.Clear(SkiaSharp.SKColors.White);

                //畫圖片的背景噪音線
                for (int i = 0; i < 11; i++)
                {
                    int x1 = random.Next(bmp.Width);
                    int x2 = random.Next(bmp.Width);
                    int y1 = random.Next(bmp.Height);
                    int y2 = random.Next(bmp.Height);

                    canvas.DrawLine(new SKPoint(x1, y1), new SKPoint(x2, y2), new SKPaint() { Color = SKColor.FromHsv(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)) });
                }
                for (int i = 0; i < 7; i++)
                {
                    int x1 = random.Next(bmp.Width);
                    int x2 = random.Next(bmp.Width);
                    int y1 = random.Next(bmp.Height);
                    int y2 = random.Next(bmp.Height);

                    canvas.DrawLine(new SKPoint(x1, y1), new SKPoint(x2, y2), new SKPaint() { Color = SKColors.DeepPink });
                }

                SKPaint textPaint = new SKPaint()
                {
                    StrokeWidth = 5,
                    Typeface = SKTypeface.FromFamilyName("Segoe UI",SKFontStyle.Bold),
                    IsAntialias = true,
                    TextAlign = SKTextAlign.Left,
                    Shader = SKShader.CreateLinearGradient(new SKPoint(0, 0), new SKPoint(bmp.Width, bmp.Height), new SKColor[] { SKColor.FromHsv(52, 152, 240), SKColor.FromHsv(243, 13, 243) }, SKShaderTileMode.Clamp),
                    TextSize = 28
                };
                SKRect Sksize = new SKRect();
                textPaint.MeasureText(MyImgVerifDate.CheckCode.Substring(0,2), ref Sksize);//計算文字寬度以及高度
                canvas.DrawText(MyImgVerifDate.CheckCode.Substring(0, 2), (bmp.Width - Sksize.Width)/6, ((bmp.Height - Sksize.Height)/3)- Sksize.Top, textPaint);
                canvas.DrawText(MyImgVerifDate.CheckCode.Substring(2), (bmp.Width - Sksize.Width) / 4 + Sksize.Width, (bmp.Height - Sksize.Height) - Sksize.Top-5, textPaint);

                //畫圖片的前景噪音點
                for (int i = 0; i < 350; i++)
                {
                    int x = random.Next(bmp.Width);
                    int y = random.Next(bmp.Height);

                    canvas.DrawPoint(new SKPoint(x,y), SKColor.FromHsv(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));
                }

                //畫圖片的邊框線
                //canvas.DrawRect(0, 0, bmp.Width - 1, bmp.Height - 1, textPaint);

                using (SKImage img = SKImage.FromBitmap(bmp))
                {
                    using (SKData ms = img.Encode())
                    {
                        var dsfds= img.Encode(SKEncodedImageFormat.Gif, 80);
                         ReImgdata = ms.ToArray();

                    }
                }
            }
            finally
            {
                canvas.Dispose();
                bmp.Dispose();
            }

            //C# 6.0的string.Format新寫法
            MyImgVerifDate.ImgBase64 =$"data:image/gif;base64, {Convert.ToBase64String(ReImgdata)}";

            return MyImgVerifDate;
        }

        /// <summary>
        /// 亂數產生驗證碼
        /// </summary>
        /// <returns>驗證碼</returns>
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = string.Empty;

            Random random = new Random();

            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            checkCode = checkCode.Replace("l", "Q").Replace("1", "E").Replace("0", "9").Replace("O", "5");
            return checkCode;
        }
    }
}
