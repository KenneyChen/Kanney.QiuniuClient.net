using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZHC.Framework.Model
{
    public enum ErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 失败
        /// </summary>
        Fail = 0,
    }

    public class ApiModel
    {
        public int ReturnValue { get; set; }

        public string Message { get; set; }

    }

    public class ApiModel<T> : ApiModel
    {
        public T DataModel { get; set; }
    }
}
