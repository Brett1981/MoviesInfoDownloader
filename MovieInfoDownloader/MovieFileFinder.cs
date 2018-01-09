using MovieInfoDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieInfoDownloader
{
    public static class MovieFileFinder
    {


        public static List<Movie> FindMovies(string relativePath)
        {
            List<string> videoFilesPaths = GetVideosFilePaths(relativePath, SearchOption.AllDirectories);
            for (int i = videoFilesPaths.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(videoFilesPaths[i]);
                if (IsTvSeries(videoFilesPaths[i]))
                    videoFilesPaths.RemoveAt(i);
            }

            List<Movie> movieList = new List<Movie>();
            foreach (var videoFilePath in videoFilesPaths)
            {
                string movieName = ExtractTitleFromFileName(Path.GetFileNameWithoutExtension(videoFilePath));
                Movie movie = OMDbApi.FindMovie(movieName);

                if (String.IsNullOrEmpty(movie.Title)) continue;
                movie.Path = videoFilePath;

                movieList.Add(movie);
            }

            return movieList;
        }

        private static string ExtractTitleFromFileName(string filename)
        {
            filename = filename.Replace('.', ' ');
            string datePattern = "[1-2][0-9][0-9][0-9]";
            Regex r = new Regex(datePattern, RegexOptions.IgnoreCase);
            Match m = r.Match(filename);
            if (m.Success)
            {
                int indeks = m.Index;
                filename = filename.ToString().Substring(0, indeks);
            }

            return filename;
        }

        private static bool IsTvSeries(string filename)
        {
            string pattern = "S[0-9][0-9]E[0-9][0-9]";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = r.Match(filename);

            if (m.Success || filename.ToLower().Contains("sample")) return true;
            else return false;
        }

        private static List<string> GetVideosFilePaths(string relativePath, SearchOption searchOption)
        {
            return Directory
            .EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath), "*.*", searchOption)
            .Where(file =>
            file.ToLower().EndsWith("mp4") ||
            file.ToLower().EndsWith("avi") ||
            file.ToLower().EndsWith("mkv"))
            .ToList();
        }
    }
}
