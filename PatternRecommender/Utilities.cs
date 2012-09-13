using System;
using System.Collections.Specialized;
using System.Web;
using DevDefined.OAuth.Consumer;
using Nancy;
using PatternRecommender.Static;

namespace PatternRecommender
{
    public class Utilities
    {
        /// <summary>
        /// Saves the authenticated session to the context.
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <param name="session">Authenticated session</param>
        internal static void SetAuthenticatedSession(NancyContext ctx, OAuthSession session)
        {
            // TODO: make serializable if it is not already
            ctx.Request.Session[Constants.SessionItems.AuthenticatedSession] = session;
        }

        /// <summary>
        /// Retrieves the authenticated session if available.
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <returns>Authenticated session</returns>
        internal static OAuthSession GetAuthenticatedSession(NancyContext ctx)
        {
            return ctx.Request.Session[Constants.SessionItems.AuthenticatedSession] as OAuthSession;
        }

        /// <summary>
        /// Set the exception to the context.
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <param name="e">Exception that occurred during processing of the request</param>
        internal static void SetException(NancyContext ctx, Exception e)
        {
            // Do not overwrite existing exception.  
            if (ctx.Request.Session[Constants.SessionItems.Exception] == null)
            {
                ctx.Request.Session[Constants.SessionItems.Exception] = e;
            }
        }

        /// <summary>
        /// Retrieves the exception that occurred or null if no exception has occurred.
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <returns>Exception that occurred during processing of the request</returns>
        internal static Exception GetException(NancyContext ctx)
        {
            return ctx.Request.Session[Constants.SessionItems.Exception] as Exception;
        }

        /// <summary>
        /// Retrieve specific query string parameters.
        /// </summary>
        /// <param name="ctx">Context</param>
        /// <param name="parameters">String parameter names</param>
        /// <returns>Collection of the found query string parameters.</returns>
        internal static NameValueCollection GetQueryStringParameters(NancyContext ctx, string[] parameters)
        {
            NameValueCollection foundParameters = new NameValueCollection();
            NameValueCollection tempParameters = HttpUtility.ParseQueryString(ctx.Request.Url.Query);

            foreach (string param in parameters)
            {
                if (!string.IsNullOrWhiteSpace(tempParameters[param]))
                {
                    foundParameters[param] = tempParameters[param];
                }
            }

            return foundParameters;
        }
    }
}