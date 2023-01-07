using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Globalization;
using System.Dynamic;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace OnlineServices.Entity
{
    public static class BaseSettings
    {
        public static string RandomText(int length, bool isNumber = true)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";
            string characters = "";

            if (isNumber)
                characters = numbers;
            else
                characters = alphabets + numbers;

            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
        public static string NormilizeMobileNo(string phone)
        {
            var newphone = phone;
            if (phone.Substring(0, 4).Equals("0098"))
            {
                newphone = phone.Substring(4);
            }
            else if (phone.Substring(0, 3).Equals("+98"))
            {
                newphone = phone.Substring(3);
            }

            if (newphone.Length == 10 && newphone.Substring(0, 1) != "0" && newphone.Substring(0, 1) == "9")
            {
                newphone = "0" + newphone;
            }

            return newphone;
        }
        public static string ConvertDigitsToLatin(string s)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    //Persian digits
                    case '\u06f0':
                        sb.Append('0');
                        break;
                    case '\u06f1':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                        sb.Append('9');
                        break;

                    //Arabic digits    
                    case '\u0660':
                        sb.Append('0');
                        break;
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u0669':
                        sb.Append('9');
                        break;
                    default:
                        sb.Append(s[i]);
                        break;
                }
            }
            return sb.ToString();
        }
        public static bool IsValidMobileNumber(string input)
        {
            const string pattern = @"^(0|\+98)?([ ]|,|-|[()]){0,2}9[0|1|2|3|4]([ ]|,|-|[()]){0,3}(?:[0-9]([ ]|,|-|[()]){0,2}){8}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(input);
        }
        public static DateTime? ParseDate(string strDate)
        {
            try
            {
                if (string.IsNullOrEmpty(strDate) || string.Equals(strDate, "____/__/__"))
                    return null;
                strDate = strDate.Replace("/", "");
                int year = int.Parse(strDate.Substring(0, 4));
                int month = int.Parse(strDate.Substring(4, 2));
                if (month > 12)
                {
                    month = 12;
                }
                else if (month < 1)
                {
                    month = 1;
                }
                int day = int.Parse(strDate.Substring(6, 2));
                if (day > 0x1f)
                {
                    day = (month >= 6) ? 30 : 0x1f;
                }
                else if (day < 1)
                {
                    day = 1;
                }
                PersianCalendar calendar = new PersianCalendar();
                return new DateTime?(calendar.ToDateTime(year, month, day, 0, 0, 0, 0));
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
        public static string Gregorian2HijriSlashedWithNull(DateTime? GDate)
        {
            if (GDate.HasValue)
            {
                try
                {
                    return Gregorian2HijriSlashed(GDate.Value);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return "";
        }
        public static string Gregorian2HijriSlashed(DateTime GDate)
        {
            PersianCalendar calendar = new PersianCalendar();
            string[] textArray1 = new string[] { (calendar.GetDayOfMonth(GDate).ToString().Length == 1) ? ("0" + calendar.GetDayOfMonth(GDate).ToString()) : calendar.GetDayOfMonth(GDate).ToString(), "/", (calendar.GetMonth(GDate).ToString().Length == 1) ? ("0" + calendar.GetMonth(GDate).ToString()) : calendar.GetMonth(GDate).ToString(), "/", calendar.GetYear(GDate).ToString() };
            return ReverseDate(string.Concat(textArray1));
        }
        public static string Gregorian2HijriSlashedWithTime(DateTime? GDate)
        {
            if (GDate.HasValue)
            {
                try
                {
                    string str = GDate.Value.Hour.ToString();
                    string str2 = GDate.Value.Minute.ToString();
                    string[] textArray1 = new string[] { Gregorian2HijriSlashed(GDate.Value), " ", (str.Length == 1) ? ("0" + str) : str, ":", (str2.Length == 1) ? ("0" + str2) : str2 };
                    return string.Concat(textArray1);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return "";
        }
        public static string ReverseDate(string str)
        {
            if ((str == null) || (str.Trim() == string.Empty))
            {
                return string.Empty;
            }
            char[] separator = new char[] { '/' };
            string[] array = str.Split(separator);
            Array.Reverse(array);
            string[] textArray1 = new string[] { array[0], "/", array[1], "/", array[2] };
            return string.Concat(textArray1);
        }

        public static int FileTypeCode(string fileType)
        {
            if (fileType == "mp4" || fileType == "mov"
                || fileType == "wmv" || fileType == "flv"
                || fileType == "avi" || fileType == "avchd"
                || fileType == "webm" || fileType == "mkv")
                return Convert.ToInt32(FileTypeEnum.Video);
            else if (fileType == "jpg" || fileType == "jpeg"
                || fileType == "png" || fileType == "gif"
                || fileType == "svg" || fileType == "tif"
                || fileType == "tiff" || fileType == "bmp"
                || fileType == "eps" || fileType == "raw"
                || fileType == "cr2" || fileType == "nef"
                || fileType == "orf" || fileType == "sr2")
                return Convert.ToInt32(FileTypeEnum.Picture);
            else
                return Convert.ToInt32(FileTypeEnum.Voice);
        }
        public static string GetFileExtension(string base64String)
        {
            var type = base64String.Split(';')[0];
            var type2 = type.Split('/')[1];
            return type2;
        }
        public static Image ByteArrayToImage(byte[] bArray)
        {
            if (bArray == null)
                return null;

            System.Drawing.Image newImage;

            try
            {
                using (MemoryStream ms = new MemoryStream(bArray, 0, bArray.Length))
                {
                    ms.Write(bArray, 0, bArray.Length);
                    newImage = System.Drawing.Image.FromStream(ms, true);
                }
            }
            catch (Exception ex)
            {
                newImage = null;

                //Log an error here
            }

            return newImage;
        }
        private static MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }
        public static Image HandleImageUpload(byte[] binaryImage, int width, int height)
        {
            return RezizeImage(Image.FromStream(BytearrayToStream(binaryImage)), 800, 800);
        }
        private static Image RezizeImage(Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                Bitmap cpy = new Bitmap(nnx, nny, PixelFormat.Format32bppArgb);
                using (Graphics gr = Graphics.FromImage(cpy))
                {
                    gr.Clear(Color.Transparent);

                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    gr.DrawImage(img,
                        new Rectangle(0, 0, nnx, nny),
                        new Rectangle(0, 0, img.Width, img.Height),
                        GraphicsUnit.Pixel);
                }
                return cpy;
            }

        }
    }
}