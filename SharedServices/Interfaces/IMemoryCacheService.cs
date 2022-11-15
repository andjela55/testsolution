using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using SharedServices.Interfaces;
using System.Net;

namespace SharedServices.Interfaces
{
    public interface IMemoryCacheService
    {
        public void SaveHttpResponse(string constant, ActionExecutedContext context, bool checkForId);
        public void GetResponseFromCache(ActionExecutingContext context, string constant, bool checkForId);
        public void RemoveItem(string constant, string id = "");
    }
}
