using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2.Controller
{
    /// <summary>
    /// Class used as common controller for all classes.
    /// Class created by Ermin & Ali.
    /// </summary>
    public static class UtilityController
    {
        /// <summary>
        /// Method used to confirm existence URL exits
        /// </summary>
        /// <param name="URL">string. URL.</param>
        /// <returns>
        /// Returns true if URL is valid, else returns false.</returns>
        public static bool checkURL(string URL)
        {
            Uri uriResult;
            return (Uri.TryCreate(URL, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }
}