
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace YouTubeAPI
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var apiKey = "write your api key";
            var playlistId = "PLSN6qXliOioz5lnckfofNcLJ3CnZJvEJO";
            var maxResults = 10;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults={maxResults}&playlistId={playlistId}&key={apiKey}");
            var json = await response.Content.ReadAsStringAsync();

            var videos = JsonConvert.DeserializeObject<YouTubeVideoResponse>(json).Items;
            foreach (var video in videos)
            {
                Console.WriteLine($"Title: {video.Snippet.Title}, VideoId: {video.Id}");
            }
        }
    }

    public class YouTubeVideoResponse
    {
        public List<YouTubeVideo> Items { get; set; }
    }

    public class YouTubeVideo
    {
        public VideoSnippet Snippet { get; set; }

        public string? Id { get; set; }
    }

    public class VideoSnippet
    {
        public string? Title { get; set; }
    }
}
