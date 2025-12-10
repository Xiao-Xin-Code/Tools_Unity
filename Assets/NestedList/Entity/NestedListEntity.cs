using System.Collections.Generic;
using System.Linq;

public class NestedListEntity : BaseEntity
{
	Dictionary<int, BaseNode> categoriesMap = new Dictionary<int, BaseNode>();

	public List<BaseNode> GetCategories => categoriesMap.Values.ToList();

	public void AddCategory(int id, BaseNode category)
	{
		categoriesMap.Add(id, category);
	}


}

public interface IUniversal
{
	int Id { get; set; }
	string Name { get; set; }
	int ParentId { get; set; }
	int Depth { get; set; }
}

public abstract class BaseNode : IUniversal
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int ParentId { get; set; }
	public int Depth { get; set; }

	public bool? IsExpanded { get; set; }
}