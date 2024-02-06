using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBotTemplateNet8.Config
{
    internal class BotConfig
    {
        public string DiscordBotToken { get; set; }
        public string DiscordBotPrefix { get; set; }

        public async Task ReadJSONAsync()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");

            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    var json = await sr.ReadToEndAsync();
                    var obj = JsonConvert.DeserializeObject<JSONStruct>(json);

                    DiscordBotToken = obj.Token;
                    DiscordBotPrefix = obj.Prefix;
                }
            }
            else
            {
                throw new FileNotFoundException("Configuration file not found.", filePath);
            }
        }
    }

    internal sealed class JSONStruct
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}
