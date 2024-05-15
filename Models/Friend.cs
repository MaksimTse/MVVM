using Plugin.Media.Abstractions;
using SQLite;

namespace MVVM.Models
{
    [Table("Friends")]
    public class Friend
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; } 
        public byte[] Img { get; set; }
    }
}
