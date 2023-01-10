using Microsoft.AspNetCore.Http;

namespace OnlineServices.Api.Helpers
{
    public class SettingMethod
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SettingMethod(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool IsLocal()
        {
            var url = _httpContextAccessor.HttpContext?.Request.Host.Value;
            return url == null || url.Contains("localhost");
        }
    }
}
