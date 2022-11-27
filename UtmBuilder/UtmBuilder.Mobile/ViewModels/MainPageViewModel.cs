using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;
using UtmBuilder.Mobile.Exceptions;

namespace UtmBuilder.Mobile.ViewModels
{
    public sealed partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string websiteUrl;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string id;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string source;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string medium;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string term;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Url))]
        private string content;

        public string Url
        {
            get
            {
                try
                {
                    var utm = new Utm(url:websiteUrl, id:id, source:source, medium:medium, name:name, term:term, content:content);
                    return utm.ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        [RelayCommand]
        private async Task CopyToClipboard()
        {
            await Clipboard.SetTextAsync(Url);
            await Application.Current.MainPage
                .DisplayAlert(
                    "Copied!!!",
                    "URL copied to clipboard",
                    "OK");
        }
    }
    //Modelos transferidos por conta de erro de compatibilidade de versões
    public class Campaign
    {
        /// <summary>
        /// Generate a new campaign for a URL
        /// </summary>
        /// <param name="source">The referrer (e.g, google, newsletter)</param>
        /// <param name="medium">Marketing medium (e.g, cpc, banner, email)</param>
        /// <param name="name">Product, promo code, or slogan (e.g., spring_sale) One of campaign</param>
        /// <param name="id">The ads campaign id.</param>
        /// <param name="term">Identify the paid keywords</param>
        /// <param name="content">Use to differentiate ads</param>
        public Campaign(
            string source,
            string medium,
            string name,
            string? id = null,
            string? term = null,
            string? content = null)
        {
            Id = id;
            Source = source;
            Medium = medium;
            Name = name;
            Term = term;
            Content = content;

            InvalidUtmException.ThrowIfNull(source, "UTM Source is invalid");
            InvalidUtmException.ThrowIfNull(medium, "UTM Medium is invalid");
            InvalidUtmException.ThrowIfNull(name, "UTM Name is invalid");
        }

        /// <summary>
        /// The ads campaign id.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// The referrer (e.g, google, newsletter)
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Marketing medium (e.g, cpc, banner, email)
        /// </summary>
        public string Medium { get; }

        /// <summary>
        /// Product, promo code, or slogan (e.g., spring_sale) One of campaign
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Identify the paid keywords
        /// </summary>
        public string? Term { get; }

        /// <summary>
        /// Use to differentiate ads
        /// </summary>
        public string? Content { get; }
    }

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
