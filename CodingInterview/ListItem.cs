namespace CodingInterview
{
    public class ListItem
    {
        public ListItem(int id, string name, int? parentId)
        {
            Id = id;
            Name = name;
            ParnetId = parentId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParnetId { get; set; }
    }
}