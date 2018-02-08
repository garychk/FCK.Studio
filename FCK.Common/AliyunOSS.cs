using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FCK.Common
{
    public class AliyunOSS
    {
        /// <summary>
        /// OSS的访问地址。http://oss-cn-shanghai.aliyuncs.com
        /// </summary>
        public string endpoint { get; set; }
        /// <summary>
        /// OSS的访问ID
        /// </summary>
        private static string accessKeyId = "r6Se9XR3KekuMn7E";
        /// <summary>
        /// OSS的访问密钥
        /// </summary>
        private static string accessKeySecret = "NjHxEuBbUX8l3kxgJT0II904szrWl8";

        private static OssClient ossClient = null;

        /// <summary>
        /// 在OSS中创建一个新的存储空间。
        /// </summary>
        /// <param name="bucketName">要创建的存储空间的名称</param>
        public string CreateBucket(string bucketName)
        {
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var bucket = ossClient.CreateBucket(bucketName);
                return bucket.Name;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 上传指定的文件到指定的OSS的存储空间
        /// </summary>
        /// <param name="bucketName">指定的存储空间名称</param>
        /// <param name="key">文件的在OSS上保存的名称</param>
        /// <param name="fileToUpload">指定上传文件的本地路径</param>
        public string PutObject(string bucketName, string key, string fileToUpload)
        {
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var result = ossClient.PutObject(bucketName, key, fileToUpload);

                return result.ETag;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public PutObjectResult PutObject(string bucketName, string key, Stream content)
        {
            PutObjectResult result = null;
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                result = ossClient.PutObject(bucketName, key, content);

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            return result;
        }

        public PutObjectResult PutObject(string bucketName, string key, Stream content, ObjectMetadata meta)
        {
            PutObjectResult result = null;
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                result = ossClient.PutObject(bucketName, key, content, meta);

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            return result;
        }

        public string GetFileUri(string bucketName, string key)
        {
            string fileurl = "";
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                AccessControlList accs = ossClient.GetBucketAcl(bucketName);
                if (!accs.Grants.Any())
                {
                    fileurl = ossClient.GeneratePresignedUri(bucketName, key, DateTime.Now.AddMinutes(5)).AbsoluteUri;
                }
            }
            catch (Exception ex) { Log(ex.Message); }
            return fileurl;
        }

        /// <summary>
        /// 列出指定存储空间的文件列表
        /// </summary>
        /// <param name="bucketName">存储空间的名称</param>
        public List<string> ListObjects(string bucketName)
        {
            List<string> lists = new List<string>();
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var listObjectsRequest = new ListObjectsRequest(bucketName);
                var result = ossClient.ListObjects(listObjectsRequest);

                foreach (var summary in result.ObjectSummaries)
                {
                    lists.Add(summary.Key);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            return lists;
        }

        public List<string> ListObjects(string bucketName, string prefix)
        {
            List<string> lists = new List<string>();
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var result = ossClient.ListObjects(bucketName, prefix);

                foreach (var summary in result.ObjectSummaries)
                {
                    lists.Add(summary.Key);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            return lists;
        }

        /// <summary>
        /// 从指定的OSS存储空间中获取指定的文件
        /// </summary>
        /// <param name="bucketName">要获取的文件所在的存储空间名称</param>
        /// <param name="key">要获取的文件在OSS上的名称</param>
        /// <param name="fileToDownload">本地存储下载文件的目录<param>
        public void GetObject(string bucketName, string key, string fileToDownload)
        {
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                var obj = ossClient.GetObject(bucketName, key);

                //将从OSS读取到的文件写到本地
                using (var requestStream = obj.Content)
                {
                    byte[] buf = new byte[1024];
                    using (FileStream fs = File.Open(fileToDownload, FileMode.OpenOrCreate))
                    {
                        var len = 0;
                        while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                        {
                            fs.Write(buf, 0, len);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get object failed, {0}", ex.Message);
            }
        }

        public string GetObject(string bucketName, string key)
        {
            ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
            var obj = ossClient.GetObject(bucketName, key);
            return obj.Key;
        }

        /// <summary>
        /// 删除指定的文件
        /// </summary>   
        /// <param name="bucketName">文件所在存储空间的名称</param>
        /// <param name="key">待删除的文件名称</param>
        public bool DeleteObject(string bucketName, string key)
        {
            try
            {
                ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
                ossClient.DeleteObject(bucketName, key);
                return true;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        public void Log(string loginfo)
        {
            try
            {
                string txtPath = "D:\\Errorlogs.txt";
                if (!File.Exists(txtPath))
                {
                    CreateFile(txtPath);
                }
                StreamWriter sw = new StreamWriter(txtPath, true, System.Text.Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString());
                sw.WriteLine(loginfo);
                sw.Close();
            }
            catch { }
        }

        public bool CreateFile(string filePath)
        {
            try
            {
                filePath = "D:\\Errorlogs.txt";
                if (!File.Exists(filePath))
                    File.Create(filePath);
                return true;
            }
            catch { return false; }
        }
    }
}
