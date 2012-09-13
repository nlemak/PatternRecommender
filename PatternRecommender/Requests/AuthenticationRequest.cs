using System;
using System.Collections.Specialized;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Storage.Basic;
using Nancy;
using Nancy.Cookies;
using Nancy.Responses;
using PatternRecommender.Static;

namespace PatternRecommender.Requests
{
    public class AuthenticationRequest
    {
        private NancyContext ctx;
        private bool forceReauth;

        /// <summary>
        /// Constructor for an authentication request.
        /// Forced re-authentication may occur after a request receives a 401 from Ravelry.
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <param name="forceReauth">Optional parameter to force re-authentication</param>
        public AuthenticationRequest(NancyContext ctx, bool forceReauth = false)
        {
            this.ctx = ctx;
            this.forceReauth = forceReauth;
        }

        public Response DoRequest()
        {
            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = Constants.OAuth.ConsumerKey,
                ConsumerSecret = Constants.OAuth.ConsumerSecret,
                SignatureMethod = Constants.OAuth.SignatureMethod,
            };

            OAuthSession session = new OAuthSession(
                consumerContext,
                Constants.OAuth.RequestTokenURL,
                Constants.OAuth.AuthorizeURL,
                Constants.OAuth.AccessTokenURL);

            string tokenCookie = null;
            ctx.Request.Cookies.TryGetValue(Constants.Cookies.OAuthTokenCookie, out tokenCookie);
            string secretCookie = null;
            ctx.Request.Cookies.TryGetValue(Constants.Cookies.OAuthSecretCookie, out secretCookie);

            if (!forceReauth && !string.IsNullOrWhiteSpace(tokenCookie) && !string.IsNullOrWhiteSpace(secretCookie))
            {
                AccessToken accessToken = new AccessToken();
                accessToken.Token = tokenCookie;
                accessToken.TokenSecret = secretCookie;

                session.AccessToken = accessToken;
            }
            else
            {
                NameValueCollection queryStringParameters = Utilities.GetQueryStringParameters(ctx, new string[] { "oauth_token", "oauth_verifier" });

                if (queryStringParameters.Count == 2)
                {
                    // Re-create the request token from the callback request from Ravelry post-user-authentication.
                    RequestToken requestToken = new RequestToken();
                    requestToken.Token = queryStringParameters["oauth_token"];

                    try
                    {
                        // TODO: Ravelry fails to accept the token here.  Debugging with them.
                        // Exchange the temporary request token for an access token which can be used to access user data.  Uses "GET".
                        IToken accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, queryStringParameters["oauth_verifier"]);

                        // Save token data to cookies.
                        ctx.Response.AddCookie(new NancyCookie(Constants.Cookies.OAuthTokenCookie, accessToken.Token, true, true));
                        ctx.Response.AddCookie(new NancyCookie(Constants.Cookies.OAuthSecretCookie, accessToken.TokenSecret, true, true));
                    }
                    catch (Exception e)
                    {
                        Utilities.SetException(ctx, e);
                        return null;
                    }
                }
                else
                {
                    // Retrieve a request token from Ravelry.
                    session.CallbackUri = new System.Uri(Constants.OAuth.CallbackURL);
                    IToken requestToken = session.GetRequestToken();

                    // Generate a user authorization URL for the request token.
                    string authorizationLink = session.GetUserAuthorizationUrlForToken(
                        requestToken,
                        Constants.OAuth.CallbackURL);

                    // Redirect to Ravelry.
                    return new RedirectResponse(authorizationLink);
                }
            }

            // Save the session to the context
            Utilities.SetAuthenticatedSession(ctx, session);

            return null;
        }
    }
}