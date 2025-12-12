public interface IBaseNode 
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ParentId { get; set; }
	public int Depth { get; set; }
}

public abstract class BaseFolder : IBaseNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public int Depth { get; set; }

	public bool IsExpanded { get; set; }
}

public abstract class BaseFile : IBaseNode
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentId { get; set; }
    public int Depth { get; set; }
}