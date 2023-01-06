using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace WebApi.Attributes
{
    public class ETagFilter : Attribute, IActionFilter
    {
        private readonly int[] _statusCodes;

        public ETagFilter(params int[] statusCodes)
        {
            _statusCodes = statusCodes;
            if (statusCodes.Length == 0) _statusCodes = new[] { 200 };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == "GET")
            {
                if (_statusCodes.Contains(context.HttpContext.Response.StatusCode))
                {
                    var content = JsonConvert.SerializeObject(context.Result);

                    var etag = GenerateETag(context.HttpContext.Request.Path.ToString(), content);

                    if (context.HttpContext.Request.Headers.Keys.Contains("If-None-Match") && context.HttpContext.Request.Headers["If-None-Match"].ToString() == etag)
                    {
                        context.Result = new StatusCodeResult(304);
                    }
                    context.HttpContext.Response.Headers.Add("ETag", new[] { etag });
                }
            }
        }
        private string GenerateETag(string request, string body)
        {
            var finalValue = String.Concat(request, body);
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(finalValue)));
        }
    }
}
