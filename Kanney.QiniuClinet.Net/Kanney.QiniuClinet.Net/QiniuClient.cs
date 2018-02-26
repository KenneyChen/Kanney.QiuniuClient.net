using Qiniu.IO;
using Qiniu.RS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZHC.Framework.Model;

namespace Kanney.QiniuClinet.Net
{
    public class QiniuClient
    {
        static QiniuClient()
        {
            //设置账号的AK和SK
            Qiniu.Conf.Config.ACCESS_KEY = ConfigHelper.GetSettingsKey("QiniuCloud_ACCESS_KEY"); //"gfs673LM84Ko5pHxOAEF4Wd-rFnAnK-6m44DQlFn";
            Qiniu.Conf.Config.SECRET_KEY = ConfigHelper.GetSettingsKey("QiniuCloud_SECRET_KEY");// "wnLs9b6ClYzVMednKJtcnwSonH-BeTdQ4KD0GAvN";
        }

        //设置上传的空间
        private static string bucket
        {
            get { return ConfigHelper.GetSettingsKey("QiniuCloud_Bucket"); } 
        }

        /// <summary>
        /// 七牛URL上传
        /// </summary>
        /// <param name="filePath">图片绝对路径</param>
        /// <param name="fileKey">上传文件文件名（由于七牛不支持文件夹，可以在文件做手脚：比如：/aaa/xxx.jpg 这个一个合法文件名</param>
        /// <returns></returns>
        public static PutRet UploadUrl(string filePath, string fileKey/*设置上传的文件的key值*/)
        {
            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            //设置上传的文件的key值
            //String key = "EDIT2/yourdefinekey3.PNG";
            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间  [:key覆盖上传]
            PutPolicy put = new PutPolicy(bucket + ":" + fileKey, 3600);
            //设置callbackUrl以及callbackBody,七牛将文件名和文件大小回调给业务服务器
            //put.CallBackUrl = "http://7xrpbe.com1.z0.glb.clouddn.com/callback";
            //put.CallBackBody = "filename=xxxxx.png&filesize=100";

            //调用Token()方法生成上传的Token
            string upToken = put.Token();
            //上传文件的路径
            //String filePath = p + @"\abc\7.png";

            //调用PutFile()方法上传
            PutRet ret = target.PutFile(upToken, fileKey, filePath, extra);

            return ret;
        }

        /// <summary>
        /// 七牛URL上传
        /// </summary>
        /// <param name="filePath">图片绝对路径</param>
        /// <param name="fileKey">上传文件文件名（由于七牛不支持文件夹，可以在文件做手脚：比如：/aaa/xxx.jpg 这个一个合法文件名</param>
        /// <returns></returns>
        public static ApiModel<string> UploadStream(Stream stream, string fileKey/*设置上传的文件的key值*/)
        {
            //TODO:校验图片类型
            //stream.Position = 0;
            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            //设置上传的文件的key值
            //String key = "EDIT2/yourdefinekey3.PNG";
            //普通上传,只需要设置上传的空间名就可以了,第二个参数可以设定token过期时间  [:key覆盖上传]
            PutPolicy put = new PutPolicy(bucket + ":" + fileKey, 3600);
            //设置callbackUrl以及callbackBody,七牛将文件名和文件大小回调给业务服务器
            //put.CallBackUrl = "http://7xrpbe.com1.z0.glb.clouddn.com/callback";
            //put.CallBackBody = "filename=xxxxx.png&filesize=100";

            //调用Token()方法生成上传的Token
            string upToken = put.Token();
            //上传文件的路径
            //String filePath = p + @"\abc\7.png";

            //调用Put方法上传
            PutRet ret = target.Put(upToken, fileKey, stream, extra);

            return new ApiModel<string>
            {
                ReturnValue = ret.OK ? (int)ErrorCode.Success : 0,
                DataModel = ret.key,
                Message = ret.Exception != null ? ret.Exception.ToString() : ""
            };
        }
    }
}