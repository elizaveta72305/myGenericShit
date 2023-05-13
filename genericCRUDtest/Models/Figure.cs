using genericCRUDtest.Models;
using System.Text.Json.Serialization;

namespace genericCRUD.Models
{
    public class Figure: EntitiyBase
    {
        public string Name { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Width { get; set; }

        //[JsonIgnore]
        public Color? FigureColor { get; set; }

        //public int ColorId { get; set; }
    }
}
