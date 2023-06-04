using Microsoft.AspNetCore.Http;

namespace AppNoOne.TagHelper
{
    public class SessionUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetSession(string key, string value)
        {
            _httpContextAccessor.HttpContext.Session.SetString(key, value);

        }

        //Get Session syntax

        public string GetSession(string key)
        {
            return _httpContextAccessor.HttpContext.Session.GetString(key);

        }
    }
}
