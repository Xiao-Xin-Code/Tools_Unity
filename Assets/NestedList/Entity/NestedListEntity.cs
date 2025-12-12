using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class NestedListEntity : BaseEntity
{
	Dictionary<int, IBaseNode> categoriesMap = new Dictionary<int, IBaseNode>();

	public List<IBaseNode> GetCategories => categoriesMap.Values.ToList();

	public void AddCategory(int id, IBaseNode category)
	{
		Debug.Log("添加ID：" + id + "ParentID：" + category.ParentId);
		categoriesMap.Add(id, category);
	}


	public IBaseNode GetCateGory(int id)
	{
		if (categoriesMap.ContainsKey(id))
		{
			return categoriesMap[id];
		}

		throw new NullReferenceException($"正在尝试获取指定ID的节点数据 ID:{id}，不存在");
	}

	public T GetCateGory<T>(int id) where T : IBaseNode
	{
		if (categoriesMap.ContainsKey(id))
		{
			Debug.Log(categoriesMap[id].GetType());
			if(categoriesMap[id] is T)
			{
				return (T)categoriesMap[id];
			}
		}

		throw new NullReferenceException($"正在尝试获取指定类型的节点数据 Type:{typeof(T)},ID:{id}，不存在");
	}

}

