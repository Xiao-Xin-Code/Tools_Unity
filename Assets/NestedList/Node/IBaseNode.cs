public interface IBaseNode 
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ParentId { get; set; }
	public int Depth { get; set; }

	public bool? IsExpanded { get; set; }
}