namespace genericCRUD.Models
{
    public class EntitiyBase
    {
        public long Id { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
