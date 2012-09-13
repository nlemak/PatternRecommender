using System;
using Nancy;
using Nancy.Responses;
using PatternRecommender.Requests;
using PatternRecommender.Static;

namespace PatternRecommender.Modules
{
    public class BaseModule : NancyModule
    {
        public BaseModule() : base()
        {
            Before += ctx =>
            {
                AuthenticationRequest authRequest = new AuthenticationRequest(ctx);
                return authRequest.DoRequest();
            };

            Get[Constants.URL.Home] = _ =>
            {
                //Exception e = Utilities.GetException(Context);
                //if (e != null)
                //{
                //    return View["Error", e];
                //}

                return View["Home"];
            };

            Get["/callback"] = _ =>
            {
                //Exception e = Utilities.GetException(Context);
                //if (e != null)
                //{
                //    return View["Error", e];
                //}

                //return new RedirectResponse(Constants.URL.Home);
                return View["Home"];
            };
        }
    }
}