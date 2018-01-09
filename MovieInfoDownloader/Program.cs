using MovieInfoDownloader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfoDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = MovieFileFinder.FindMovies("");
            string htmlFileName = "movies";
            HTMLPrinter.PrintToHtml(movies, htmlFileName);
            System.Diagnostics.Process.Start($"{htmlFileName}.html");
            Console.ReadLine();
        }
    }
}
