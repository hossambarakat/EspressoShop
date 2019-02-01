using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace EspressoShop.Web.Infrastructure
{
    public class HeadersHelper
    {
        public static void AddTracingHeaders(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            var incoming_headers = new string[] {"x-request-id",
                       "x-b3-traceid",
                       "x-b3-spanid",
                       "x-b3-parentspanid",
                       "x-b3-sampled",
                       "x-b3-flags",
                       "x-ot-span-context"
            };
            foreach(var header in incoming_headers)
            {
                if(httpContextAccessor.HttpContext.Request.Headers.Keys.Contains(header))
                {
                    client.DefaultRequestHeaders.Add(header, new string[] { httpContextAccessor.HttpContext.Request.Headers[header] });
                }
            }
        }
    }
}
