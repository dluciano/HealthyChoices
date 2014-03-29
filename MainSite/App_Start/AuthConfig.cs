using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using MainSite.Models;

namespace MainSite
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            var facebooksocialData = new Dictionary<string, object> { { "Icon", "/Images/Facebook.png" } };

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "447433985384501",
                appSecret: "6a3e83dabcdca35bced76a8940f2be31",
                displayName: "Facebook",
                extraData: facebooksocialData);
        }
    }
}
