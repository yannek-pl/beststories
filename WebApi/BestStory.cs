namespace WebApi
{
    /// <summary>
    /// Input object returned by Hacker-news API
    /// </summary>
    public class BestStory
    {
        public String title { get; set; }

        public String url { get; set; }

        public String type { get; set; }

        public int descendants { get; set; }

        public List<int> kids { get; set; }

        public String postedBy { get; set; }

        public uint time { get; set; }

        public int score { get; set; }

        public int commentCount { get; set; }
    }

    /// <summary>
    /// Output object created from BestStory
    /// </summary>
    public class BestStoryOutput
    {
        public String title { get; set; }

        public String uri { get; set; }

        public String postedBy { get; set; }

        public String time { get; set; }

        public int score { get; set; }

        public int commentCount { get; set; }

        public BestStoryOutput(BestStory bs)
        {
            this.title = bs.title;
            this.uri = bs.url;
            this.postedBy = bs.postedBy;
            this.time = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(bs.time).ToString("yyyy-MM-ddTHH:mm:ss") + "+00:00";
            this.score = bs.score;
            this.commentCount = bs.commentCount;
        }
    }
}