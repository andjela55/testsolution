using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using SharedServices.Interfaces;
using System.Net;

namespace Services
{



    public class MemoryCacheService : IMemoryCacheService
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
            //var path = context.HttpContext.Request.Path.ToString();
            var result = context.Result;
            //_memoryCache.Set(path, result);

            if (checkForId)
            {
                var id = RoutingHttpContextExtensions.GetRouteValue(context.HttpContext, "id");
                constant = String.Format(constant, id);

            }
            //pathMap[constant] = path;
            _memoryCache.Set(constant, result);

        }
        public void GetResponseFromCache(ActionExecutingContext context,string constant,bool checkForId)
        {
            //var requestPath = context.HttpContext.Request.Path.ToString();
            if (checkForId)
            {
                var id = RoutingHttpContextExtensions.GetRouteValue(context.HttpContext, "id");
                constant = String.Format(constant, id);

            }

            if (!_memoryCache.TryGetValue(constant, out ObjectResult responseFromCache))
            {
                return;
            }
            context.Result = responseFromCache;
            return;
        }

        public void RemoveItem(string constant, string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                constant = string.Format(constant, id);
            }
            //var path = pathMap[key];
            _memoryCache.Remove(constant);
        }

    }
}
