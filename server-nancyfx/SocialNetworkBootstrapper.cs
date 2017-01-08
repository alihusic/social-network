using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using SocialNetwork2.Factory;
using SocialNetworkServerNV1.Response;
using System;

namespace SocialNetwork2
{
    /// <summary>
    /// Class created by Ali.
    /// </summary>
    public class SocialNetworkBootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

       

        /// <summary>
        /// Method used to handle unexpected exception reporting.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private dynamic customErrorHandle(dynamic context, dynamic exception)
        {
            return new ErrorResponse("FATAL SERVER ERROR: " + ((Exception)exception).Message + '\n' + ((Exception)exception).StackTrace);
        }

        /// <summary>
        /// Method used to handle client request logging.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private dynamic customRequestLogging(dynamic context, dynamic exception)
        {
            return null;
        }

        /// <summary>
        /// Method used to handle server response logging.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private dynamic customResponseLogging(dynamic context, dynamic exception)
        {
            return null;
        }

        /**
         ApplicationStartup overriden to introduce custom-made error handling
             */
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            TokenFactory.deleteAllTokens();
            pipelines.OnError += (context, exception) => customErrorHandle(context, exception);
            //pipelines.BeforeRequest += (context, exception) => customRequestLogging(context, exception);
            //pipelines.AfterRequest += (context, exception) => customResponseLogging(context, exception);
            base.ApplicationStartup(container, pipelines);
        }

        
    }
}