using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Services
{



    public class MemoryCacheService
    {
        public IMemoryCache _memoryCache;
        private Dictionary<string, string> pathMap = new Dictionary<string, string>();

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void SaveHttpResponse(string constant, ActionExecutedContext context, bool checkForId)
        {
            if (!(new HttpResponseMessage((HttpStatusCode)context.HttpContext.Response.StatusCode).IsSuccessStatusCode))
            {
                return;
            }
            var path = context.HttpContext.Request.Path.ToString();
            var result = context.Result;
            _memoryCache.Set(path, result);

            if (checkForId)
            {
                var id = RoutingHttpContextExtensions.GetRouteValue(context.HttpContext, "id");
                constant = String.Format(constant, id);

            }
            pathMap[constant] = path;

        }
        public void GetResponseFromCache(ActionExecutingContext context)
        {
            var requestPath = context.HttpContext.Request.Path.ToString();

            if (!_memoryCache.TryGetValue(requestPath, out ObjectResult responseFromCache))
            {
                return;
            }
            context.Result = responseFromCache;
            return;
        }

        public void RemoveItem(string key, string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                key = string.Format(key, id);
            }
            _memoryCache.Remove(key);
        }

    }
}
