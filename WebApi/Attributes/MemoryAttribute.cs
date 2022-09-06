using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;

namespace WebApp.Attributes
{

    public class MemoryAttribute : TypeFilterAttribute
    {
        public MemoryAttribute(string constant, bool checkForId = false) : base(typeof(MemoryFilter))
        {
            Arguments = new object[] { constant, checkForId };
        }
    }

    public class MemoryFilter : IActionFilter
    {
        private string _constant;
        private MemoryCacheService _memoryCacheService;
        private bool _checkForId;

        public MemoryFilter(MemoryCacheService memoryCacheService, string constant, bool checkForId)
        {
            _constant = constant;
            _memoryCacheService = memoryCacheService;
            _checkForId = checkForId;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            ////var req = context.HttpContext.Request.Path.ToString();
            ////var result = context.Result;
            ////_memoryCache.Set(req, result);
            ////_dictionaryService.AddElementToDictionary(_constant, req);
            _memoryCacheService.SaveHttpResponse(_constant, context, _checkForId);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _memoryCacheService.GetResponseFromCache(context);

        }
    }
}



