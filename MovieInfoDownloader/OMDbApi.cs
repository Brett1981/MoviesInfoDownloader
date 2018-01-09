using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using MovieInfoDownloader.Models;

namespace MovieInfoDownloader
{
    static public class OMDbApi
    {
        //Please dont overuse!
        private static readonly string key = "d53aa5f7";

        static private string DownloadMovieData(string movieTitle)
        {
            WebClient wc = new WebClient();
            return wc.DownloadString($"http://www.omdbapi.com/?t={movieTitle.Replace(' ','+')}&plot=full&apikey={key}");
        }

        static public Movie FindMovie(string movieTitle)
        {
            JsonSerializer json = new JsonSerializer();

            return JsonConvert.DeserializeObject<Movie>(DownloadMovieData(movieTitle));
        }


    }
}
