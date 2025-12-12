using QMVC;
using UnityEngine.EventSystems;

public class DefaultFolderController : ItemBaseController
{
	DefaultFolderEntity m_Entity;
	DefaultFolderView m_View;


	protected override void OnInit()
	{
		_entity = new DefaultFolderEntity();
		m_Entity = _entity as DefaultFolderEntity;
		m_View = _view as DefaultFolderView;

		m_View.RegisterPointerClick(ExpandOrCollapseCallBack);
	}

	public override void SetEntityData(object data)
	{
		m_Entity.DefaultNodeFolder = data as DefaultNodeFolder;
		//通过FullName获取图标信息，更新sprite

		m_View.UpdateView(m_Entity);
	}

	
	public void ExpandOrCollapse()
	{
		m_Entity.DefaultNodeFolder.IsExpanded = !m_Entity.DefaultNodeFolder.IsExpanded;
		//Refresh
		this.SendCommand<RefreshNestedListCommand>();
	}

	private void ExpandOrCollapseCallBack(PointerEventData evt)
	{
		if(evt.button == PointerEventData.InputButton.Left)
		{
			if(evt.clickCount == 2)
			{
				ExpandOrCollapse();
			}
		}
	}

}