using EfCore.SoftDelete.Attributes;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    [SoftDelete]
    public bool IsDeleted { get; set; }
}
