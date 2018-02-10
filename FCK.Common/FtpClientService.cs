using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace FCK.Common
{
    public class FtpClientService
    {
        #region Internal Members
        private NetworkCredential certificate;
        #endregion
        /// <summary>
        /// 构造函数，提供初始化数据的功能，打开Ftp站点
        /// </summary>
        public FtpClientService(string FtpUserName, string FtpPassword)
        {
            certificate = new NetworkCredential(FtpUserName, FtpPassword);
        }
        /// <summary>
        /// 创建FTP请求
        /// </summary>
        /// <param name="uri">ftp://myserver/upload.txt</param>
        /// <param name="method">Upload/Download</param>
        /// <returns></returns>
        private FtpWebRequest CreateFtpWebRequest(Uri ftpuri, string method)
        {
            FtpWebRequest ftpClientRequest = (FtpWebRequest)WebRequest.Create(ftpuri);
            ftpClientRequest.Proxy = null;
            ftpClientRequest.Credentials = certificate;
            ftpClientRequest.KeepAlive = true;
            ftpClientRequest.UseBinary = true;
            ftpClientRequest.UsePassive = true;
            ftpClientRequest.Method = method;
            ftpClientRequest.UsePassive = true;
            //ftpClientRequest.Timeout = -1;
            return ftpClientRequest;
        }
        /// <summary>
        /// FTP上传文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="ftpServerIP"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool UploadFile(string sourceFile, string ftpServerIP, string filePath)
        {
            try
            {
                FtpCheckDirectoryExist(filePath, ftpServerIP);
                FileInfo fileInf = new FileInfo(sourceFile);
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpServerIP + filePath + fileInf.Name));
                reqFTP.Credentials = certificate;
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.ContentLength = fileInf.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                FileStream fs = fileInf.OpenRead();
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                int allbye = (int)fileInf.Length;
                int startbye = 0;
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                    startbye += contentLen;
                }
                strm.Close();
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private int CopyDataToDestination(Stream sourceStream, Stream destinationStream, int offSet)
        {
            try
            {
                int sourceLength = (int)sourceStream.Length;
                int length = sourceLength - offSet;
                byte[] buffer = new byte[length + offSet];
                int bytesRead = sourceStream.Read(buffer, offSet, length);
                while (bytesRead != 0)
                {
                    destinationStream.Write(buffer, 0, bytesRead);
                    bytesRead = sourceStream.Read(buffer, 0, length);
                    length = length - bytesRead;
                    offSet = (bytesRead == 0) ? 0 : (sourceLength - length);//(length - bytesRead);                 
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return offSet;
            }
            finally
            {
                destinationStream.Close();
                sourceStream.Close();
            }
            return offSet;
        }
        
        //删除文件
        public bool DeleteFileName(string deletefile, string ftpServerIP)
        {
            try
            {
                FileInfo file = new FileInfo(deletefile);
                Uri uri = new Uri(ftpServerIP + file.Name);
                FtpWebRequest reqFTP = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.DeleteFile);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查目录，并自动创建目录
        /// </summary>
        /// <param name="destFilePath"></param>
        /// <param name="ftpServerIP"></param>
        public void FtpCheckDirectoryExist(string destFilePath, string ftpServerIP)
        {
            string fullDir = FtpParseDirectory(destFilePath);
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];   
                if (dir != null && dir.Length > 0)
                {
                    curDir += dir + "/";
                    MakeDir(curDir, ftpServerIP);
                }
            }
        }

        public string FtpParseDirectory(string destFilePath)
        {
            return destFilePath.Substring(0, destFilePath.LastIndexOf("/"));
        }

        //创建目录
        public bool MakeDir(string dirName, string ftpServerIP)
        {
            try
            {
                Uri uri = new Uri(ftpServerIP + dirName);
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(uri);
                reqFTP.Credentials = certificate;
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Bitmap GenThumbnail(Stream imagestr, int width, int height)
        {
            Image imageFrom = Image.FromStream(imagestr);
            // 创建画布
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            try
            {
                // 源图宽度及高度 
                int imageFromWidth = imageFrom.Width;
                int imageFromHeight = imageFrom.Height;
                // 生成的缩略图实际宽度及高度 
                int bitmapWidth = width;
                int bitmapHeight = height;
                // 生成的缩略图在上述"画布"上的位置 
                int X = 0;
                int Y = 0;
                double multiple = 0;

                //宽大于高的横图
                if (imageFromWidth > imageFromHeight)
                {
                    multiple = (double)imageFromHeight / (double)imageFromWidth;
                    bitmapWidth = Convert.ToInt32((double)height / multiple);
                    if (bitmapWidth >= width)
                    {
                        bitmapHeight = height;
                    }
                    else
                    {
                        bitmapWidth = width;
                        bitmapHeight = Convert.ToInt32((double)bitmapWidth * multiple);
                    }

                    X = (width - bitmapWidth) / 2;
                }
                //高大于宽的竖图
                else
                {
                    multiple = (double)imageFromWidth / (double)imageFromHeight;
                    bitmapWidth = width;
                    bitmapHeight = Convert.ToInt32((double)width / multiple);
                    Y = (height - bitmapHeight) / 2;
                }


                // 用白色清空 
                g.Clear(Color.White);
                // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                // 指定高质量、低速度呈现。 
                g.SmoothingMode = SmoothingMode.HighQuality;
                // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
                g.DrawImage(imageFrom, new Rectangle(X, Y, bitmapWidth, bitmapHeight), new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);

                //返回Bitmap
                return bmp;

                //返回Stream
                //byte[] byt = BitmapToBytes(bmp);
                //return BytesToStream(byt);
            }
            catch
            {
                return null;
            }
            finally
            {
                //释放资源   
                imageFrom.Dispose();
                //bmp.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// Bitmap 转 byte[]
        /// </summary>
        /// <param name="Bitmap"></param>
        /// <returns></returns>
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, ImageFormat.Jpeg);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

    }

}
