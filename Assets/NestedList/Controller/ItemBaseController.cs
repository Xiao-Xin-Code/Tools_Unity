
public abstract class ItemBaseController : BaseController
{
    protected ItemBaseView _view;
    protected ItemBaseEntity _entity;



    private void Awake()
    {
        _view = GetComponent<ItemBaseView>();
        OnInit();
    }

    protected abstract void OnInit();

    public abstract void SetEntityData(object data);
}