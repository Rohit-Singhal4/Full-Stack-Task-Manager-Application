using System.Collections;

namespace TodoApi.Infrastructure.Models;

public class Group
{ 
        public long Id { get; set; }
        public required string Name { get; set; }
        public required bool IsDefault { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<TodoItem> Items { get; set; }
}