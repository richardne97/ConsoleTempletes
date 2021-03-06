﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using Newtonsoft.Json.Linq;

namespace JwtWebApiSelfHost.Utility
{
    /// <summary>
    /// Generate HttpResponseMessages in JSON format
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// Generate response message while the return value is a single, primitive value
        /// 回應數值型態
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static HttpResponseMessage ResponseSinglePrimitiveValue(HttpStatusCode code, ValueType value, string details)
        {
            ResultMessage result = new ResultMessage()
            {
                Result = value,
                Details = details
            };
            return new HttpResponseMessage(code)
            {
                Content = new JsonContent(JsonConvert.SerializeObject(result, Formatting.Indented), Encoding.UTF8)
            };
        }

        /// <summary>
        /// 無資料
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static HttpResponseMessage NoData(string details)
        {
            ResultMessage result = new ResultMessage()
            {
                Result = "NoData",
                Details = details
            };

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new JsonContent(JsonConvert.SerializeObject(result, Formatting.Indented), Encoding.UTF8)
            };
        }

        /// <summary>
        /// 回應參數不合法
        /// </summary>
        public static HttpResponseMessage InvalidParameters(string details)
        {
            ResultMessage result = new ResultMessage()
            {
                Result = "Invalid Parameters",
                Details = details
            };

            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new JsonContent(JsonConvert.SerializeObject(result, Formatting.Indented), Encoding.UTF8)
            };
        }

        /// <summary>
        /// 將物件序列化為JsonContent
        /// </summary>
        /// <param name="obj">欲序列化的物件</param>
        /// <returns></returns>
        public static HttpResponseMessage ResponseJsonSerializedObject(object obj)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new JsonContent(JsonConvert.SerializeObject(obj))
            };
        }

        /// <summary>
        /// 回傳檔案或影像
        /// </summary>
        /// <param name="bytes">內容</param>
        /// <returns></returns>
        public static HttpResponseMessage ResponseByteArray(byte[] bytes)
        {
            var message = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes)
            };
            message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return message;
        }
    }

    /// <summary>
    /// Generate Response content in JSON format
    /// </summary>
    public class JsonContent : StringContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">the content to be encoded in JSON format. The encoding is UTF8</param>
        public JsonContent(string content) : this(content, Encoding.UTF8)
        {   }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">the content to be encoded in JSON format.</param>
        /// <param name="encoding">The encoding type</param>
        public JsonContent(string content, Encoding encoding) : base(content, encoding, "application/json")
        {   }
    }
}
