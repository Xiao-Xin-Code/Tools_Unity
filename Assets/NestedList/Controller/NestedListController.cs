using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QMVC;
using Unity.VisualScripting;
using UnityEngine;

public class NestedListController : BaseController
{
	NestedListView _view;
	NestedListEntity _entity;


	private string rootPath;


	PoolSystem _poolSystem;
	ItemBaseFactory _factory;




    private void Awake()
    {
		_poolSystem = this.GetSystem<PoolSystem>();

		_entity = new NestedListEntity();

		Refresh(_entity);
	}



    private void Refresh(NestedListEntity entity)
	{
		float curHeight = 0;
		float overHeight = _view.Scroll.content.anchoredPosition.y;
		float displayHeight = _view.Scroll.content.anchoredPosition.y + _view.Scroll.GetComponent<RectTransform>().rect.height;
		float maxWidth = 0;

		foreach (var item in _entity.GetCategories)
		{
			if (item.ParentId >= 0) 
			{
				//存在父节点
				IBaseNode node = _entity.GetCateGory(item.ParentId);

				if (node != null) 
				{
					if (node.IsExpanded == null) 
					{
						throw new Exception("父节点不是容器，此种情况是错误的");
					}
					else
					{
						if (node.IsExpanded.Value)
						{
							var prefab = _factory.GetPrefab(item.GetType());
							float targetHeight = curHeight + prefab.RectTransform.rect.height;
							float targetWidth = item.Depth * _view.IndentPerLevel + prefab.RectTransform.rect.width;

							if (maxWidth < targetWidth)
							{
								maxWidth = targetWidth;
							}


							if (targetHeight > overHeight && curHeight < displayHeight)
							{
								var categoryItem = _poolSystem.GetPool(item.GetType()).Get();
								categoryItem.transform.SetParent(_view.Scroll.content);

								//设置数据

								float xPos = item.Depth * _view.IndentPerLevel;
								float yPos = -curHeight;
								categoryItem.RectTransform.anchoredPosition = new Vector2(xPos, yPos);
							}

							curHeight = targetHeight;
						}
					}
				}
				else
				{
					throw new NullReferenceException($"父节点不存在：{item.ParentId}，结构不正确");
				}
			}
			else
			{
				var prefab = _factory.GetPrefab(item.GetType());
				float targetHeight = curHeight + prefab.RectTransform.rect.height;
				float targetWidth = item.Depth * _view.IndentPerLevel + prefab.RectTransform.rect.width;

				if (maxWidth < targetWidth)
				{
					maxWidth = targetWidth;
				}

				if (targetHeight > overHeight && curHeight < displayHeight)
				{
					var categoryItem = _poolSystem.GetPool(item.GetType()).Get();
					categoryItem.transform.SetParent(_view.Scroll.content);

					//设置数据


					float xPos = item.Depth * _view.IndentPerLevel;
					float yPos = -curHeight;
					categoryItem.RectTransform.anchoredPosition = new Vector2(xPos, yPos);
				}

				curHeight = targetHeight;

			}
		}

		_view.Scroll.content.sizeDelta = new Vector2(maxWidth, curHeight);
	}




	public void GetNode()
	{
		List<string> paths = new List<string>();
		int id = 0;
		Stack<(string FullName, string Name, int Id, int ParentId, int Depth)> stack = new Stack<(string path, string Name, int Id, int ParentId, int Depth)>();
		DirectoryInfo info = new DirectoryInfo(rootPath);
		stack.Push((info.FullName, info.Name, 0, -1, 0));

		while (stack.Count > 0)
		{
			var stackInfo = stack.Pop();
			paths.Add($"[{stackInfo.Id},{stackInfo.ParentId},{stackInfo.Depth}]：{stackInfo.FullName}");

			if (Directory.Exists(stackInfo.FullName))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(stackInfo.FullName);

				IEnumerable<FileInfo> fileInfos = directoryInfo.EnumerateFiles();
				foreach (var item in fileInfos.Reverse())
				{
					id = id + 1;
					stack.Push((item.FullName, item.Name, id, stackInfo.Id, stackInfo.Depth + 1));
				}

				IEnumerable<DirectoryInfo> folderInfos = directoryInfo.EnumerateDirectories();
				foreach (var item in folderInfos.Reverse())
				{
					id = id + 1;
					stack.Push((item.FullName, item.Name, id, stackInfo.Id, stackInfo.Depth + 1));
				}
			}
		}
	}
	
}