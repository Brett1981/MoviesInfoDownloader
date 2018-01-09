using MovieInfoDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace MovieInfoDownloader
{
    public static class HTMLPrinter
    {
        public static void PrintToHtml(Movie movie,string path)
        {
            PrintToHtml(new Movie[] { movie }, path);
        }

        public static void PrintToHtml(IEnumerable<Movie> movies, string path)
        {
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            htmlWriter.Write("<head>");
            htmlWriter.Write("<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css' integrity='sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb' crossorigin='anonymous'>");
            htmlWriter.Write("</head>");

            foreach (var movie in movies)
            {
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "jumbotron");
                htmlWriter.RenderBeginTag("div");

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "media");
                htmlWriter.RenderBeginTag("div");

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Src, movie.Poster);
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Alt, "Movie poster missing");
                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "align-self-center mr-3");
                htmlWriter.RenderBeginTag("img");
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "media-body");
                htmlWriter.RenderBeginTag("div");

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "mt-0");
                htmlWriter.RenderBeginTag("h5");
                htmlWriter.InnerWriter.Write(movie.Title);
                htmlWriter.RenderEndTag();

                htmlWriter.AddAttribute(HtmlTextWriterAttribute.Class, "mb-0");
                htmlWriter.RenderBeginTag("p");


                htmlWriter.RenderBeginTag("p");
                htmlWriter.Write(movie.Year);
                htmlWriter.RenderEndTag();

                htmlWriter.RenderBeginTag("p");
                htmlWriter.Write(movie.Path);
                htmlWriter.RenderEndTag();

                htmlWriter.RenderBeginTag("p");
                htmlWriter.Write("IMDB Rating: ");
                htmlWriter.Write(movie.imdbRating);
                htmlWriter.RenderEndTag();

                htmlWriter.RenderBeginTag("p");
                htmlWriter.Write(movie.Plot);
                htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

                htmlWriter.RenderEndTag();
                htmlWriter.RenderEndTag();

                htmlWriter.RenderEndTag();
            }

            stringWriter.ToString();

            using (StreamWriter sw = new StreamWriter(path+".html",false))
            {
                sw.Write(stringWriter);
            }

        }
    }
}
