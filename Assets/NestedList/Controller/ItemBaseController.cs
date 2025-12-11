
using UnityEngine;

public abstract class ItemBaseController : BaseController
{
    protected ItemBaseView _view;
    protected ItemBaseEntity _entity;

    public RectTransform RectTransform => _view ? _view.RectTransform : GetComponent<RectTransform>();

	private void Awake()
    {
        _view = GetComponent<ItemBaseView>();
        OnInit();
    }

    protected abstract void OnInit();

    public abstract void SetEntityData(object data);
}