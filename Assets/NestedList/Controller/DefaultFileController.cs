
public class DefaultFileController : ItemBaseController
{
	DefaultFileEntity m_Entity;
	DefaultFileView m_View;

	protected override void OnInit()
    {
		_entity = new DefaultFileEntity();
		m_Entity = _entity as DefaultFileEntity;
		m_View = _view as DefaultFileView;
	}

	public override void SetEntityData(object data)
	{
		m_Entity.DefaultNodeFile = data as DefaultNodeFile;
		m_View.UpdateView(m_Entity);
	}
}