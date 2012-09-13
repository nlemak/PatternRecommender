using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatternRecommender.Static
{
    public class Constants
    {
        public class URL
        {
            public static string Home = "/";

            public static string Preferences = Home + "pref";

            public static string Recommend = Home + "rec";

            public static string BookmarkHome = Home + "bkmarks";

            public static string BookmarkRemove = BookmarkHome + "/" + "remove";
        }

        public class SessionItems
        {
            public static string AuthenticatedSession = "AuthenticatedSession";

            public static string Exception = "Exception";
        }

        public class OAuth
        {
            public static string RequestTokenURL = "https://www.ravelry.com/oauth/request_token";

            public static string AuthorizeURL = "https://www.ravelry.com/oauth/authorize";

            public static string AccessTokenURL = "https://www.ravelry.com/oauth/access_token";

            // TODO: verify this is what I want.  Also could make each failed auth (from bkmarks for example) send their own callback
            public static string CallbackURL = "http://localhost:8889/callback";

            public static string ConsumerKey = "BEB1FFEE27A3B94B4590";

            public static string ConsumerSecret = "iAueLjV6AaOTfgpvTkySTynLPJFw7VdaAUK5wD3A";

            public static string SignatureMethod = DevDefined.OAuth.Framework.SignatureMethod.HmacSha1;
        }

        public class Cookies
        {
            // OAuth token cookie.
            public static string OAuthTokenCookie = "PCOAT";

            // OAuth token secret cookie.
            public static string OAuthSecretCookie = "PCOATS";

            public static string IsAccessTokenCookie = "PCIATC";
        }
    }
}