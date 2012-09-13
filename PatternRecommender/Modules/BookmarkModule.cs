using Nancy;
using PatternRecommender.Requests;
using PatternRecommender.Static;

namespace PatternRecommender.Modules
{
    public class BookmarkModule : NancyModule
    {
        public BookmarkModule() : base(Constants.URL.BookmarkHome)
        {
            Before += ctx =>
            {
                AuthenticationRequest authRequest = new AuthenticationRequest(ctx);
                return authRequest.DoRequest();
            };

            Get["/"] = parameters =>
            {
                return "Hello Bookmark World!";
            };

            Post["/"] = parameters =>
            {
                return "Updated with checkmarked bookmarks removed!";
            };
        }
    }
}