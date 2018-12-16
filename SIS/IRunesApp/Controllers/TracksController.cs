namespace IRunesApp.Controllers
{
    using Common;
    using Models;
    using SIS.HTTP.Requests.Contracts;
    using SIS.HTTP.Responses.Contracts;
    using SIS.WebServer.Results;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;

    public class TracksController : BaseController
    {
        public IHttpResponse Create(IHttpRequest request)
        {
            if (!this.IsLoggedIn)
            {
                return new RedirectResult("users/login");
            }

            return this.View();
        }

        public IHttpResponse DoCreate(IHttpRequest request)
        {
            var name = request.FormData["name"].ToString().Replace("+", "");
            var link = WebUtility.UrlDecode(request.FormData["link"].ToString());
            var price = decimal.Parse(request.FormData["price"].ToString());

            var urlTokens = request.Headers.GetHeader("Referer").Value.Split('?', StringSplitOptions.RemoveEmptyEntries);
            var albumId = urlTokens[1].Replace("albumId=", "");

            // Validate
            this.ValidateName(name);
            //this.ValidateLink(link);

            // Create
            this.CreateAndSaveTrack(name, link, price, albumId);

            this.ViewBag["albumId"] = $"?id={albumId}";

            return new RedirectResult("/tracks/create");
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            var urlTokens = request.Url.Split('?', StringSplitOptions.RemoveEmptyEntries);
            var ids = urlTokens[1].Split('&', StringSplitOptions.RemoveEmptyEntries);
            var albumId = ids[0].Replace("albumid=", "");
            var trackId = ids[1].Replace("trackid=", "");
            var track = this.Db.Tracks.FirstOrDefault(t => t.Id == trackId);
            if (track != null)
            {
                this.ViewBag["link"] = track.Link;
                this.ViewBag["name"] = track.Name;
                this.ViewBag["price"] = track.Price.ToString(CultureInfo.InvariantCulture);
                this.ViewBag["albumId"] = $"?albumId={albumId}";
            }
            else
            {
                this.ViewBag["link"] = Messages.NoTrackInfo;
                this.ViewBag["name"] = "";
                this.ViewBag["price"] = "";
                this.ViewBag["albumId"] = "";
            }

            return this.View();
        }

        private void CreateAndSaveTrack(string name, string link, decimal price, string albumId)
        {
            var track = new Track()
            {
                Name = name,
                Link = link,
                Price = price
            };

            var trackAlbum = new TrackAlbum();
            trackAlbum.AlbumId = albumId;
            track.Albums.Add(trackAlbum);

            try
            {
                this.Db.Tracks.Add(track);
                this.Db.SaveChanges();
            }
            catch (Exception exception)
            {
                this.ServerError(exception.Message);
            }
        }

        private void ValidateLink(string link)
        {
            var url = WebUtility.UrlDecode(link);
            var validUri = new Uri(url);
            if (string.IsNullOrWhiteSpace(validUri.Scheme) ||
                string.IsNullOrWhiteSpace(validUri.Host) ||
                string.IsNullOrWhiteSpace(validUri.LocalPath) ||
                !validUri.IsDefaultPort)
            {
                this.BadRequestError(Messages.InvalidUrl);
            }
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                this.BadRequestError(Messages.InvalidTrackName);
            }

            if (this.Db.Tracks.Any(t => t.Name == name))
            {
                this.BadRequestError(Messages.TrackExists);
            }
        }
    }
}