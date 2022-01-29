using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace swmsweb
{
    public class HttpResult
    {
        private bool success;

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private Object data;


        public Object Data
        {
            get { return data; }
            set { data = value; }
        }

        public static HttpResult failResult(string message)
        {
            return new HttpResult() { message = message, success = false };
        }
        public static HttpResult failResult(string message, Object data)
        {
            return new HttpResult() { message = message, success = false, data = data };
        }
        public static HttpResult successResult(string message, Object data)
        {
            return new HttpResult() { success = true, message = message, data = data };
        }
        public static HttpResult successResult(Object data)
        {
            return new HttpResult() { success = true, data = data };
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" });
        }
    }

    public class WebApiResult {
        private bool success;

        public bool IsSuccess
        {
            get { return success; }
            set { success = value; }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private Object data;
       

        public Object Data
        {
            get { return data; }
            set { data = value; }
        }

        private string statusCode;

        public string StatusCode {
            get { return statusCode; }
            set { statusCode = value; }
        }
       

        public static WebApiResult failResult(string message)
        {
            return new WebApiResult() { message = message, success = false };
        }
        public static WebApiResult failResult(string message, Object data)
        {
            return new WebApiResult() { message = message, success = false, data = data };
        }
        public static WebApiResult successResult(string message, Object data)
        {
            return new WebApiResult() { success = true, message = message, data = data,statusCode = "200" };
        }
        public static WebApiResult successResult(Object data)
        {
            return new WebApiResult() { success = true, data = data, statusCode = "200" };
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.Converters.IsoDateTimeConverter() { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" });
        }
    }
}