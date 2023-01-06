using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedServices.Interfaces;

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
        private IMemoryCacheService _memoryCacheService;
        private bool _checkForId;

        public MemoryFilter(IMemoryCacheService memoryCacheService, string constant, bool checkForId)
        {
            _constant = constant;
            _memoryCacheService = memoryCacheService;
            _checkForId = checkForId;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

            _memoryCacheService.SaveHttpResponse(_constant, context, _checkForId);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _memoryCacheService.GetResponseFromCache(context, _constant, _checkForId);

        }
    }
}



