using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OakIdeas.AppHarbor.Api.Extensions
{
    public static class UrlStringExtensions
    {
        public static string AddSlug(this string url, string slug)
        {
            return url.Replace("{slug}", slug);
        }

        public static string AddId(this string url, string id)
        {
            return url.Replace("{id}", id);
        }

        public static string AddBuildId(this string url, string buildId)
        {
            return url.Replace("{buildId}", buildId);
        }
        
        public static Uri ToUri(this string url)
        {
            Uri uri = new Uri(url);
            return uri;
        }
    }
}
