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
    using System.Text;

    public class AlbumsController : BaseController
    {
        public IHttpResponse All(IHttpRequest request)
        {
            if (!this.IsLoggedIn)
            {
                return new RedirectResult("users/login");
            }

            var allAlbums = new StringBuilder();
            var albums = this.Db.Albums;
            if (albums.Any())
            {
                foreach (var album in albums)
                {
                    allAlbums.AppendLine($@"<h4><a href = ""/albums/details?id={album.Id}"">{album.Name}</a></h4>");
                }

                this.ViewBag["albums"] = allAlbums.ToString();
            }
            else
            {
                this.ViewBag["albums"] = Messages.NoAlbums;
            }

            return this.View();
        }

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
            var cover = WebUtility.UrlDecode(request.FormData["cover"].ToString());

            // Validate
            this.ValidateName(name);

            // Create
            this.CreateAndSaveAlbum(name, cover);

            return new RedirectResult("/albums/create");
        }

        public IHttpResponse Details(IHttpRequest request)
        {
            var urlTokens = request.Url.Split('?', StringSplitOptions.RemoveEmptyEntries);
            var albumId = urlTokens[1].Replace("id=", "");
            var album = this.Db.Albums.FirstOrDefault(a => a.Id == albumId);
            if (album != null)
            {
                this.ViewBag["cover"] = album.Cover;
                this.ViewBag["name"] = album.Name;
                this.ViewBag["price"] = album.Price.ToString(CultureInfo.InvariantCulture);
                this.ViewBag["albumId"] = $"?albumId={albumId}";

                var tracks = this.Db.Albums.FirstOrDefault(a => a.Id == albumId).Tracks.Select(ta => ta.Track);
                var trackList = new StringBuilder();
                var counter = 1;
                foreach (var track in tracks)
                {
                    trackList.AppendLine($@"<h4><a href = ""/tracks/details?albumId={album.Id}&trackId={track.Id}"">{counter++}. {track.Name}</a></h4>");
                }

                this.ViewBag["tracks"] = trackList.ToString();
            }
            else
            {
                this.ViewBag["image"] = Messages.NoTracks;
                this.ViewBag["name"] = "";
                this.ViewBag["price"] = "";
                this.ViewBag["tracks"] = "";
                this.ViewBag["albumId"] = "";
            }

            return this.View();
        }

        private void CreateAndSaveAlbum(string name, string cover)
        {
            var album = new Album()
            {
                Name = name,
                Cover = cover
            };

            try
            {
                this.Db.Albums.Add(album);
                this.Db.SaveChanges();
            }
            catch (Exception exception)
            {
                this.ServerError(exception.Message);
            }
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                this.BadRequestError(Messages.InvalidAlbumName);
            }

            if (this.Db.Albums.Any(a => a.Name == name))
            {
                this.BadRequestError(Messages.AlbumExists);
            }
        }
    }
}