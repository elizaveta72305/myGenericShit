using genericCRUD.Models;
using System.Text.Json.Serialization;

namespace genericCRUDtest.Models
{
    public class Color: EntitiyBase
    {
        public string Name { get; set; } = string.Empty;
        public string RGB { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Figure> Figures { get; set; } = new List<Figure>();
    }
}
