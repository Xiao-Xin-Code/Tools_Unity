using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QMVC;
using UnityEngine;

public class NestedListController : BaseController
{
	NestedListView _view;
	NestedListEntity _entity;

	[SerializeField]
	private string rootPath;


	PoolSystem _poolSystem;
	ItemBaseFactory _factory;




    private void Awake()
    {
		_poolSystem = this.GetSystem<PoolSystem>();
		_factory = this.GetUtility<ItemBaseFactory>();
		_entity = new NestedListEntity();
		_view = GetComponent<NestedListView>();

		StartCoroutine(GetNode());

		_view.RegisterScrollValueChanged(RefreshOnScroll);

		
	}



    private void Refresh(NestedListEntity entity)
	{
		_poolSystem.ReturnAllPool();

		float curHeight = 0;
		float overHeight = _view.Scroll.content.anchoredPosition.y;
		float displayHeight = _view.Scroll.content.anchoredPosition.y + _view.Scroll.GetComponent<RectTransform>().rect.height;
		float maxWidth = 0;

		foreach (var item in _entity.GetCategories)
		{
			if (item.ParentId >= 0) 
			{
				//存在父节点,父节点必须是BaseFolder
				BaseFolder node = _entity.GetCateGory<BaseFolder>(item.ParentId);

				if (node != null) 
				{
					if (node.IsExpanded)
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
							var categoryItem = _poolSystem.GetPool(item.GetType(), prefab).Get();
							categoryItem.transform.SetParent(_view.Scroll.content);

							//设置数据
							categoryItem.SetEntityData(item);

							float xPos = item.Depth * _view.IndentPerLevel;
							float yPos = -curHeight;
							categoryItem.RectTransform.anchoredPosition = new Vector2(xPos, yPos);
						}
						curHeight = targetHeight;
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
					var categoryItem = _poolSystem.GetPool(item.GetType(),prefab).Get();
					categoryItem.transform.SetParent(_view.Scroll.content);

					//设置数据
					categoryItem.SetEntityData(item);

					float xPos = item.Depth * _view.IndentPerLevel;
					float yPos = -curHeight;
					categoryItem.RectTransform.anchoredPosition = new Vector2(xPos, yPos);
				}
				curHeight = targetHeight;
			}
		}

		_view.Scroll.content.sizeDelta = new Vector2(maxWidth, curHeight);
	}


	private void RefreshOnScroll(Vector2 v)
	{
		Refresh(_entity);
	}



	IEnumerator GetNode()
	{
		int id = 0;
		Stack<(string FullName, string Name, int Id, int ParentId, int Depth)> stack = new Stack<(string path, string Name, int Id, int ParentId, int Depth)>();
		DirectoryInfo info = new DirectoryInfo(rootPath);
		stack.Push((info.FullName, info.Name, 0, -1, 0));

		while (stack.Count > 0)
		{
			var stackInfo = stack.Pop();

			if (File.Exists(stackInfo.FullName))
			{
				_entity.AddCategory(stackInfo.Id, new DefaultNodeFile
				{
					Id = stackInfo.Id,
					ParentId = stackInfo.ParentId,
					Name = stackInfo.Name,
					Depth = stackInfo.Depth
				});
			}
			else if (Directory.Exists(stackInfo.FullName))
			{
				
				_entity.AddCategory(stackInfo.Id, new DefaultNodeFolder
				{
					Id = stackInfo.Id,
					ParentId = stackInfo.ParentId,
					Name = stackInfo.Name,
					Depth = stackInfo.Depth,
					IsExpanded = true
				});
			}

			yield return null;

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

		yield return null;

		Refresh(_entity);
	}
	
}