using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Base.Server.IService;
using Base.Server.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Base.Server.Start.Controllers
{
    //    api/file/list
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IFileService _fileService;
        // 读取数据库中的文件列表 
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        [Route("list")]// 需要加鉴权？不需要。因为这个逻辑是在登录操作之前
        public IActionResult GetFileList(string? keyword)
        {
            // 关于Result<>包装方式   下课后自己尝试

            Result<List<UpgradeFileModel>> result = new Result<List<UpgradeFileModel>>();
            try
            {
                //bool state = false;
                //if (string.IsNullOrEmpty(keyword))
                //    state = true;

                var files = _fileService.Query<UpgradeFileModel>(f =>
                    f.FileId > 0 && f.state == 1 && (string.IsNullOrEmpty(keyword) || f.FileName.Contains(keyword)));

                // EFCore


                // 从数据库获取相关的文件信息   文件名-文件MD5
                result.Data = files.ToList();
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        // 下载文件方法，      http://localhost:5000/api/file/download/Halcon算子手册(中文复印版).pdf
        [HttpGet("download/{filename}")]
        public IActionResult Download([FromRoute] string filename)
        {
            //var filePath = Path.Combine(Path.Combine(_configuration.Read("FileFolder"), "upgrade"), filename); //文件所在路径
            //'D:\Zhaoxi.EDU\Jovan\01-VIP\[2021]WPF上位机工业互联\20220128WPF上位机Course049SmartParking(6)\src\Base.Server\Base.Server\\bin
            //\net5.0-windowsWebFiles\UpgradeFiles\Halcon算子手册(中文复印版).pdf'.”


            //D:\Zhaoxi.EDU\Jovan\Vip_src\Base.Server\Base.Server\bin\Debug\net5.0\WebFiles\UpgradeFiles
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string filePath = Path.Combine(root, @"WebFiles\UpgradeFiles", filename);

            ResFileStream fs = new ResFileStream(filePath, FileMode.Open, FileAccess.Read);

            var type = new MediaTypeHeaderValue("application/oct-stream").MediaType;

            //enableRangeProcessing 是否启动断点续传
            return File(fs, contentType: type, filename, enableRangeProcessing: true);
        }

        //Url编码
        // http://localhost:5000/api/file/upload    以Header方式传进来   md5   file_path
        [HttpPost]
        [Route("upload")]
        [Authorize]// Token
        public IActionResult Upload([FromForm] IFormCollection formCollection, [FromHeader] string md5, [FromHeader] string file_path)
        {
            Result<long> result = new Result<long>();
            try
            {
                FormFileCollection filelist = (FormFileCollection)formCollection.Files;
                if (filelist.Count > 0)
                {
                    // 文件名称
                    string fileName = filelist[0].FileName;
                    // 文件的MD5值
                    string fileMD5 = md5;// formCollection["md5"].ToString();
                    // 更新地址
                    string filePath = file_path;// formCollection["file_path"].ToString();

                    var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    string path = "UpgradeFiles";

                    string targetPath = Path.Combine(root, $"WebFiles\\{path}");
                    DirectoryInfo di = new DirectoryInfo(targetPath);
                    if (!di.Exists) di.Create();


                    using (FileStream fs = System.IO.File.Create(Path.Combine(targetPath, fileName)))
                    {
                        // 复制文件
                        filelist[0].CopyToAsync(fs).GetAwaiter().GetResult();
                        // 清空缓冲区数据
                        fs.Flush();
                    }

                    {
                        // 更新或新增到数据库
                        //UpgradeFile upgradeFile = new UpgradeFile
                        //{
                        //    FileName = fileName,
                        //    FileMd5 = fileMD5,
                        //    FilePath=
                        //    UploadTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                        //};
                        ////_fileUpgradeService.Insert<UpgradeFile>(upgradeFile);
                        //_fileUpgradeService.AddOrUpdate(upgradeFile);
                    }
                    // 保存图片文件（用户图像）
                }
                result.Data = filelist.Sum(f => f.Length);
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }
            return Ok(result);
        }

        // 静态资源访问   上传的文件可以直接请求
        // mp3
        // http://localhost:5000/api/file/img/1001.png
        [HttpGet("img/{img}")]
        public IActionResult GetImage([FromRoute(Name = "img")] string imgPath)
        {
            var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string rootPath = Path.Combine(root, @"WebFiles\Images");
            //获取图片的返回类型
            var contentTypDict = new Dictionary<string, string> {
                {"jpg","image/jpeg"},
                {"jpeg","image/jpeg"},
                {"jpe","image/jpeg"},
                {"png","image/png"},
                {"gif","image/gif"},
                {"ico","image/x-ico"},
                {"tif","image/tiff"},
                {"tiff","image/tiff"},
                {"fax","image/fax"},
                {"wbmp","image/nd.wap.wbmp"},
                {"rp","imagend.rn-realpix"}
            };
            var contentTypeStr = "image/jpeg";

            var imgTypeSplit = imgPath.Split('.');
            var imgType = imgTypeSplit[imgTypeSplit.Length - 1].ToLower();
            //未知的图片类型
            if (contentTypDict.ContainsKey(imgType))
            {
                contentTypeStr = contentTypDict[imgType];
            }

            string filePath = Path.Combine(rootPath, imgPath);
            //图片不存在
            if (!new FileInfo(filePath).Exists)
            {
                return NoContent();
            }
            //返回原图
            using (var sw = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, contentTypeStr);
            }
        }

        [HttpPost]
        [Route("u_image")]//上头像
        [Authorize]// Token   
        public IActionResult UploadAvatar([FromForm] IFormCollection formCollection, [FromHeader] string username)
        {
            Result<long> result = UploadImage((FormFileCollection)formCollection.Files, username, "Images");
            //Result<long> result = new Result<long>();
            //try
            //{
            //    FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            //    if (filelist.Count > 0)
            //    {
            //        // 文件名称
            //        string fileName = username;

            //        var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //        string path = "Images";

            //        string targetPath = Path.Combine(root, $"WebFiles\\{path}");
            //        DirectoryInfo di = new DirectoryInfo(targetPath);
            //        if (!di.Exists) di.Create();


            //        using (FileStream fs = System.IO.File.Create(Path.Combine(targetPath, fileName)))
            //        {
            //            // 复制文件
            //            filelist[0].CopyToAsync(fs).GetAwaiter().GetResult();
            //            // 清空缓冲区数据
            //            fs.Flush();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.State = 500;
            //    result.ExceptionMessage = ex.Message;
            //}

            return Ok(result);
        }

        [HttpPost]
        [Route("u_snap")]//上传抓拍
        [Authorize]// Token   
        public IActionResult UploadSnapPhoto([FromForm] IFormCollection formCollection, [FromHeader] string filename)
        {
            // Base64编码   -  解码  gergergergre
            filename = Encoding.UTF8.GetString(Convert.FromBase64String(filename));
            Result<long> result = UploadImage((FormFileCollection)formCollection.Files, filename, "SnapPhotos");
            //Result<long> result = new Result<long>();
            //try
            //{
            //    FormFileCollection filelist = (FormFileCollection)formCollection.Files;
            //    if (filelist.Count > 0)
            //    {
            //        // 文件名称
            //        string fileName = filename;

            //        var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            //        string path = "SnapPhotos";

            //        string targetPath = Path.Combine(root, $"WebFiles\\{path}");
            //        DirectoryInfo di = new DirectoryInfo(targetPath);
            //        if (!di.Exists) di.Create();


            //        using (FileStream fs = System.IO.File.Create(Path.Combine(targetPath, fileName)))
            //        {
            //            // 复制文件
            //            filelist[0].CopyToAsync(fs).GetAwaiter().GetResult();
            //            // 清空缓冲区数据
            //            fs.Flush();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.State = 500;
            //    result.ExceptionMessage = ex.Message;
            //}

            return Ok(result);
        }

        private Result<long> UploadImage(FormFileCollection fileList, string filename, string path)
        {
            Result<long> result = new Result<long>();
            try
            {
                FormFileCollection fl = fileList;
                if (fl.Count > 0)
                {
                    // 文件名称
                    string fileName = filename;// 带扩展名

                    var root = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    //string path = "Images";

                    string targetPath = Path.Combine(root, $"WebFiles\\{path}");
                    DirectoryInfo di = new DirectoryInfo(targetPath);
                    if (!di.Exists) di.Create();


                    using (FileStream fs = System.IO.File.Create(Path.Combine(targetPath, fileName)))
                    {
                        // 复制文件
                        fl[0].CopyToAsync(fs).GetAwaiter().GetResult();
                        // 清空缓冲区数据
                        fs.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                result.State = 500;
                result.ExceptionMessage = ex.Message;
            }

            return result;
        }
    }

    internal class ResFileStream : FileStream
    {
        public ResFileStream(string path, FileMode mode, FileAccess access) : base(path, mode, access) { }

        /// <param name="array"></param>
        /// <param name="offset">偏移量</param>
        /// <param name="count">读取的最大字节数</param>
        /// <returns></returns>
        public override int Read(byte[] array, int offset, int count)
        {
            // 此处可以限制下载速度
            //count = 256;
            //Thread.Sleep(10);
            return base.Read(array, offset, count);
        }
    }
}
