using Microsoft.AspNetCore.Http.Features;

namespace ACDC2022.Utilities
{
    public class HttpClientUtilities
    {
		public static string GetClientIPAddress(HttpContext context)
		{
            string ip;

            if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
			{
				ip = context.Request.Headers["X-Forwarded-For"];
			}
			else
			{
				ip = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
			}

			return ip;
		}
	}
}
