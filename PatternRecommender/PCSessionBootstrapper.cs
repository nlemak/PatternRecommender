using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using TinyIoC;

namespace PatternRecommender
{
    public class PCSessionBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            CookieBasedSessions.Enable(pipelines);
        }
    }
}