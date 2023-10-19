using Newtonsoft.Json;

namespace Utopia.Api;

public class GetPlayerStatus
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("player_status")]
    public PlayerStatus PlayerStatus { get; set; }
}
public class PlayerStatus
    {
        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("login_time")]
        public double LoginTime { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }
    }

    public class Status
    {
        [JsonProperty("action")]
        public int Action { get; set; }

        [JsonProperty("info_text")]
        public string InfoText { get; set; }

        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("mods")]
        public int Mods { get; set; }

        [JsonProperty("beatmap")]
        public Beatmap Beatmap { get; set; }
    }

    public class Beatmap
    {
        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("set_id")]
        public int SetId { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("last_update")]
        public string LastUpdate { get; set; }

        [JsonProperty("total_length")]
        public int TotalLength { get; set; }

        [JsonProperty("max_combo")]
        public int MaxCombo { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("plays")]
        public int Plays { get; set; }

        [JsonProperty("passes")]
        public int Passes { get; set; }

        [JsonProperty("mode")]
        public int Mode { get; set; }

        [JsonProperty("bpm")]
        public string Bpm { get; set; }

        [JsonProperty("cs")]
        public float Cs { get; set; }

        [JsonProperty("od")]
        public float Od { get; set; }

        [JsonProperty("ar")]
        public float Ar { get; set; }

        [JsonProperty("hp")]
        public float Hp { get; set; }

        [JsonProperty("diff")]
        public float Diff { get; set; }
    }