using System.Text.Json;
using JoystickJury.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis;

namespace JoystickJury.Helpers
{
    public static class IGDB
    {
        private static readonly HttpClient HttpClient = new();

        /// <summary>
        /// Authenticate as a Twitch Developer (get an auth token for IGDB). <br/>
        /// (See https://api-docs.igdb.com/#getting-started. Store an auth secret in the code and I'll kill you)
        /// </summary>
        /// <param name="id">Your app's Client ID</param>
        /// <param name="secret">A Client Secret associated with your app</param>
        /// <returns>A Twitch Auth Token</returns>
        public static async Task<string?> TwitchAuth(string id, string secret)
        {
            var authQuery = new Dictionary<string, string?>
            {
                ["client_id"] = id,
                ["client_secret"] = secret,
                ["grant_type"] = "client_credentials"
            };
            var response = await HttpClient.PostAsync(
                QueryHelpers.AddQueryString("https://id.twitch.tv/oauth2/token", authQuery),
                new StringContent(""));
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<TwitchAuthResponse>(json);
            return authResponse!.access_token;
        }

        /// <summary>
        /// Get the info for 1 game.
        /// </summary>
        /// <param name="gameId">IGDB ID of the game</param>
        /// <param name="clientId">Your client ID</param>
        /// <param name="authToken">An auth token (use TwitchAuth)</param>
        /// <returns>A GameResponse</returns>
        public static async Task<GameResponse?> FetchGame(int gameId, string clientId, string authToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.igdb.com/v4/games/"),
                Headers = { { "Client-ID", clientId }, { "Authorization", $"Bearer {authToken}" } },
                Content = new StringContent(
                    // Uses the expander on cover, genres, and platforms https://api-docs.igdb.com/#expander
                    $"fields name,cover.image_id,genres.name,first_release_date,platforms.name,updated_at; where id = {gameId};")
            };

            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // the request will always return an array...
            var games = JsonSerializer.Deserialize<List<GameResponse>>(json);
            return games![0];
        }

        /// <summary>
        /// [Currently unused] Get all of the names of the given IGDB IDs, comma-separated.
        /// </summary>
        /// <param name="ids">Array of IDs, likely from a GameResponse</param>
        /// <param name="type">What the IDs are for - the endpoint to use (genres, platforms, etc.)</param>
        /// <param name="clientId">Your client ID</param>
        /// <param name="authToken">An auth token (use TwitchAuth)</param>
        /// <returns></returns>
        public static async Task<string> FetchNames(int[] ids, string type, string clientId, string authToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://api.igdb.com/v4/{type}/"),
                Headers = { { "Client-ID", clientId }, { "Authorization", $"Bearer {authToken}" } },
                Content = new StringContent(
                    $"fields name; where id = ({string.Join(",", ids)});")
            };

            var response = await HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            // the request will always return an array...
            var names = JsonSerializer.Deserialize<List<Name>>(json);
            return string.Join(", ", names!.Select(x => x.name));
        }

        /// <param name="imageId">IGDB image hash (take from a Game)</param>
        /// <param name="size">"big" or "small"</param>
        /// <returns>Complete image URL usable in &lt;img&gt;</returns>
        public static string BuildCoverUrl(string imageId, string size) => 
            $"https://images.igdb.com/igdb/image/upload/t_cover_{size}/{imageId}.jpg";
        
    }

    public class TwitchAuthResponse
    {
        public required string access_token { get; set; }
        public int expires_in { get; set; }
        public required string token_type { get; set; }
    }

    public class GameResponse
    {
        public int id { get; set; }
        public Cover cover { get; set; }
        public int first_release_date { get; set; }
        public Name[] genres { get; set; }
        public string name { get; set; }
        public Name[] platforms { get; set; }
        public int updated_at { get; set; }
    }

    public class Cover
    {
        public int id { get; set; }
        public string image_id { get; set; }
    }

    public class Name
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
