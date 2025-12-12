public class DefaultFolderController : ItemBaseController
{
	DefaultFolderEntity m_Entity;
	DefaultFolderView m_View;


	protected override void OnInit()
	{
		_entity = new DefaultFolderEntity();
		m_Entity = _entity as DefaultFolderEntity;
		m_View = _view as DefaultFolderView;


	}

	public override void SetEntityData(object data)
	{
		m_Entity.DefaultNodeFolder = data as DefaultNodeFolder;
		m_View.UpdateView(m_Entity);
	}
}