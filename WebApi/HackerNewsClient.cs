using System;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace WebApi
{
    public class HackerNewsClient
    {
        /// <summary>
        /// URL for list of Best Stories IDs
        /// </summary>
        const String url_ids_list = "https://hacker-news.firebaseio.com/v0/beststories.json";
        /// <summary>
        /// List of best stories ID is downloaded once and held by the application (since it doesn't change)
        /// </summary>
        static int[] best_stories_IDs;


        /// <summary>
        /// Loads the list of Best Stories ID in order to use it for further requests
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task InitBestStoriesIDs()
        {
            using (HttpClient cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(url_ids_list);
                // request returns a json list of IDs
                String product = await GetProductAsync(cl);
                // if request returns null --> exception will be thrown and execution stopped
                best_stories_IDs = JsonObject.Parse(product).AsArray().Select(x => (int)x).ToArray();
                if (best_stories_IDs.Length == 0)
                {
                    throw new ArgumentNullException("Server returned zero Best Stories IDs");
                }
            }
        }

        /// <summary>
        /// Asynchronously gets details of requested stories and returns in format described in "Santander - Developer Coding Test" task (JSON).
        /// </summary>
        /// <param name="count">Count of requested Best Stories</param>
        /// <returns></returns>
        public static async Task<String> GetBestStories (int count)
        {
            List<BestStoryOutput> bso = new List<BestStoryOutput>();
            // get Best Stories one by one from the top of best_stories_IDs
            // if requested count is bigger than available elements - limit to the list length
            for (int i = 0; i < count && i < best_stories_IDs.Length; i++)
            {
                using (HttpClient cl = new HttpClient())
                {
                    String uri = $"https://hacker-news.firebaseio.com/v0/item/{best_stories_IDs[i]}.json";
                    cl.BaseAddress = new Uri(uri);
                    cl.DefaultRequestHeaders.Accept.Clear();
                    cl.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    String product = await GetProductAsync(cl);
                    try
                    {
                        // parse incoming JSON with BestStory object and format it as the client requires
                        JsonNode story = JsonObject.Parse(product);
                        BestStory bs = story.Deserialize<BestStory>();
                        bso.Add(new BestStoryOutput(bs));
                    } catch (Exception ex)
                    {
                        // in case data is returned in wrong format or anything goes wrong --> ignore the record and proceed next
                        Console.WriteLine("Best Story returned from hacker-news has invalid format: " + product);
                    }
                }
            }
            // sort list by score descending and return as formatted json list
            // TODO sorting of large list may require an optimization
            JsonSerializerOptions jopt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(bso.OrderByDescending(o => o.score).ToList(), jopt);
        }

        /// <summary>
        /// Gets response from HTTP
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static async Task<String> GetProductAsync(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }
    }
}
