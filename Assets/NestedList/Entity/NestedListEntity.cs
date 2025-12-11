using System.Collections.Generic;
using System.Linq;

public class NestedListEntity : BaseEntity
{
	Dictionary<int, IBaseNode> categoriesMap = new Dictionary<int, IBaseNode>();

	public List<IBaseNode> GetCategories => categoriesMap.Values.ToList();

	public void AddCategory(int id, IBaseNode category)
	{
		categoriesMap.Add(id, category);
	}


	public IBaseNode GetCateGory(int id)
	{
		return categoriesMap[id];
	}

}

