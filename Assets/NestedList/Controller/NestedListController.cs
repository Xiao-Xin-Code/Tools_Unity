using System.Collections;
using System.Collections.Generic;
using System.IO;
using QMVC;
using UnityEngine;

public class NestedListController : BaseController
{
	NestedListView _view;
	NestedListEntity _entity;


	private string rootPath;


	PoolSystem _poolSystem;





    private void Awake()
    {


		_poolSystem = this.GetSystem<PoolSystem>();


	}



    private void Refresh(NestedListEntity entity)
	{
		float curHeight = 0;
		float overHeight = _view.Scroll.content.anchoredPosition.y;
		float displayHeight = _view.Scroll.content.anchoredPosition.y + _view.Scroll.GetComponent<RectTransform>().rect.height;
		float maxWidth = 0;

		foreach (var item in _entity.GetCategories)
		{
			if (item.IsExpanded != null)
			{


			}
			else
			{
				//获取父物体的IsExpanded，继而判断是否显示


			}

			if (item.ParentId == -1)
			{
				//说明自己就是根部//可以显示
			}
			else
			{
				//item.Depth
			}



			float targetHeight = curHeight;// + 使用的预制件的大小
			float targetWidth = item.Depth * _view.IndentPerLevel;//+ 使用的预制件的大小

			maxWidth = maxWidth > targetHeight ? maxWidth : targetHeight;

			if (targetHeight > overHeight && curHeight < displayHeight)
			{
				//显示

			}


		}



	}


	public void RootPath()
	{
		DirectoryInfo info = new DirectoryInfo(rootPath);
		IEnumerable<DirectoryInfo> infos = info.EnumerateDirectories();

		foreach (var item in infos)
		{
			//item.FullName;
		}

	}
	
}