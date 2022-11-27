using System.Text.RegularExpressions;
using UtmBuilder.Exceptions;

namespace UtmBuilder
{
    public class Utm
    {
        private const string UrlRegexPattern =
            @"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";

        /// <summary>
        /// Genereate a new UTM
        /// </summary>
        /// <param name="url">Website URL</param>
        /// <param name="source">The referrer (e.g, google, newsletter)</param>
        /// <param name="medium">Marketing medium (e.g, cpc, banner, email)</param>
        /// <param name="name">Product, promo code, or slogan (e.g., spring_sale) One of campaign</param>
        /// <exception cref="InvalidUtmException"></exception>
        public Utm(
            string url,
            string source,
            string medium,
            string name)
        {
            Url = url;
            Campaign = new Campaign(source:source, medium:medium, name:name);

            if (!Regex.IsMatch(Url, UrlRegexPattern))
                throw new InvalidUtmException("Invalid URL");
        }

        /// <summary>
        /// Genereate a new UTM
        /// </summary>
        /// <param name="url">Website URL</param>
        /// <param name="source">The referrer (e.g, google, newsletter)</param>
        /// <param name="medium">Marketing medium (e.g, cpc, banner, email)</param>
        /// <param name="name">Product, promo code, or slogan (e.g., spring_sale) One of campaign</param>
        /// <param name="id">The ads campaign id.</param>
        /// <param name="term">Identify the paid keywords</param>
        /// <param name="content">Use to differentiate ads</param>
        /// <exception cref="InvalidUtmException"></exception>
        public Utm(
            string url,
            string source,
            string medium,
            string name,
            string? id = null,
            string? term = null,
            string? content = null)
        {
            Url = url;
            Campaign = new Campaign(source:source, medium:medium, name:name, id:id, term:term, content:content);

            if (!Regex.IsMatch(Url, UrlRegexPattern))
                throw new InvalidUtmException("Invalid URL");
        }

        public string Url { get; }
        public Campaign Campaign { get; }

        public override string ToString()
            => $"{Url}?utm_source={Campaign.Source}&utm_medium={Campaign.Medium}&utm_campaign={Campaign.Name}&utm_id={Campaign.Id}&utm_term={Campaign.Term}&utm_content={Campaign.Content}";
    }
}
