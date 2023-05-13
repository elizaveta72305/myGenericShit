using genericCRUD.Models;
using System.Text.Json.Serialization;

namespace genericCRUDtest.Models
{
    public class Laser: EntitiyBase
    {
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public Color LaserColors { get; set; } = new Color();
    }
}
