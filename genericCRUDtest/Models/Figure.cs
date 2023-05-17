using genericCRUDtest.Models;
using System.Text.Json.Serialization;

namespace genericCRUD.Models
{
    public class Figure
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Length { get; set; }
        public int Width { get; set; }
        public Color FigureColor { get; set; } = new Color();
        public long ColorId { get; set; }
    }
}
