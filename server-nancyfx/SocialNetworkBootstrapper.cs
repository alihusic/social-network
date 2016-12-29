using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using SocialNetwork2.Factory;
using System;

namespace SocialNetwork2
{
    public class SocialNetworkBootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

       

        // dynamic function customErrorHandle used to handle custom exceptions and behavior
        private dynamic customErrorHandle(dynamic context, dynamic exception)
        {
            return ((Exception)exception).Message;
        }
        /**
         ApplicationStartup overriden to introduce custom-made error handling
             */
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            TokenFactory.deleteAllTokens();
            pipelines.OnError += (context, exception) => customErrorHandle(context, exception);
            base.ApplicationStartup(container, pipelines);
        }
    }
}