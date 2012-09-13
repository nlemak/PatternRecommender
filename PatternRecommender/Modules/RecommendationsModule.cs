using Nancy;
using Nancy.Responses;
using PatternRecommender.Requests;
using PatternRecommender.Static;

namespace PatternRecommender.Modules
{
    public class RecommendationsModule : NancyModule
    {
        public RecommendationsModule() : base(Constants.URL.Recommend)
        {
            Before += ctx =>
            {
                AuthenticationRequest authRequest = new AuthenticationRequest(ctx);
                return authRequest.DoRequest();
            };

            Get["/"] = parameters =>
            {
                return "Hello Recommendations World!";
            };

            Post["/"] = parameters =>
            {
                // TODO: If checkboxes checked, redirect to bkmrk home with message "added x bkmrks!", otherwise, error message at the top with no other changes
                // Redirect to the Bookmarks page after saving new bookmarks.
                return new RedirectResponse(Constants.URL.BookmarkHome);
            };
        }
    }
}