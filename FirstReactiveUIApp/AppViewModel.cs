using ReactiveUI;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using System.Globalization;
using System.Linq;

namespace FirstReactiveUIApp
{
    public class AppViewModel:ReactiveObject
    {
        private string _SearchTerm;

        public string SearchTerm
        {
            get { return _SearchTerm; }
            set { this.RaiseAndSetIfChanged(ref _SearchTerm, value); }
        }

        public ReactiveCommand<string,List<FlickrPhoto>> ExecuteSearch { get; protected set; }

        ObservableAsPropertyHelper<List<FlickrPhoto>> _SearchResults;
        public List<FlickrPhoto> SearchResults => _SearchResults.Value;

        ObservableAsPropertyHelper<Visibility> _SpinnerVisibility;
        public Visibility SpinnerVisibility => _SpinnerVisibility.Value;

        public AppViewModel()
        {
            ExecuteSearch = ReactiveCommand.CreateFromTask<string, List<FlickrPhoto>>(
                searchTerm => GetSearchResultsFromFlickr(searchTerm)
                );

            this.WhenAnyValue(x => x.SearchTerm)
                .Throttle(TimeSpan.FromMilliseconds(800), RxApp.MainThreadScheduler)
                .Select(x => x?.Trim())
                .DistinctUntilChanged()
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .InvokeCommand(ExecuteSearch);

            _SpinnerVisibility = ExecuteSearch.IsExecuting
                .Select(x => x ? Visibility.Visible : Visibility.Collapsed)
                .ToProperty(this, x => x.SpinnerVisibility, Visibility.Hidden);

            ExecuteSearch.ThrownExceptions.Subscribe(ExecuteSearch => { });

            _SearchResults = ExecuteSearch.ToProperty(this, x => x.SearchResults, new List<FlickrPhoto>());
        }

        private async Task<List<FlickrPhoto>> GetSearchResultsFromFlickr(string searchTerm)
        {
            var doc = await Task.Run(() => XDocument.Load(String.Format(CultureInfo.InvariantCulture,
                       "http://api.flickr.com/services/feeds/photos_public.gne?tags={0}&format=rss_200",
                       HttpUtility.UrlEncode(searchTerm))));

            if (doc.Root == null)
                return null;

            var titles = doc.Root.Descendants("{http://search.yahoo.com/mrss/}title")
                .Select(x => x.Value);

            var tagRegex = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var descriptions = doc.Root.Descendants("{http://search.yahoo.com/mrss/}description")
                .Select(x => tagRegex.Replace(HttpUtility.HtmlDecode(x.Value), ""));

            var items = titles.Zip(descriptions,
                (t, d) => new FlickrPhoto { Title = t, Description = d }).ToArray();

            var urls = doc.Root.Descendants("{http://search.yahoo.com/mrss/}thumbnail")
                .Select(x => x.Attributes("url").First().Value);

            var ret = items.Zip(urls, (item, url) => { item.Url = url; return item; }).ToList();
            return ret;
        }
    }
}