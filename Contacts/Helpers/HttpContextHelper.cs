using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Helpers
{
  public static class HttpContextHelper
  {
    private static IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Configures the helper for a specified Http context accessor.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public static HttpContext Current => _httpContextAccessor.HttpContext;

    public static bool UserHasClaims => Current.User.Claims.Count() > 0;

  }
}
