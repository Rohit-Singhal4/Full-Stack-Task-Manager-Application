namespace TodoApi.DTO.Group;

public class GroupResponse
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required bool IsDefault { get; set; }
    public DateTime ModifiedDate { get; set; }
    
}