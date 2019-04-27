using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EspressoShop.Web.Infrastructure
{
    public class HeadersHelper
    {
        public static void AddTracingHeaders(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            var incomingHeaders = new[] {"x-request-id",
                       "x-b3-traceid",
                       "x-b3-spanid",
                       "x-b3-parentspanid",
                       "x-b3-sampled",
                       "x-b3-flags",
                       "x-ot-span-context"
            };
            foreach(var header in incomingHeaders)
            {
                if(httpContextAccessor.HttpContext.Request.Headers.Keys.Contains(header))
                {
                    client.DefaultRequestHeaders.Add(header, new string[] { httpContextAccessor.HttpContext.Request.Headers[header] });
                }
            }

            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                client.DefaultRequestHeaders.Add("userName", new[] { httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name)});
                client.DefaultRequestHeaders.Add("UserEmail", new[] { httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)});
                client.DefaultRequestHeaders.Add("userRole", new[] { httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role)});
            }

        }
    }
}
