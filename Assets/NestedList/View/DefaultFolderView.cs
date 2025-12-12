using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefaultFolderView : ItemBaseView,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image icon;
	[SerializeField] private Text nameText;


    #region Event

    private event Action<PointerEventData> onPointerClickEvent;
    private event Action<PointerEventData> onPointerDownEvent;
    private event Action<PointerEventData> onPointerUpEvent;

	#endregion



	public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClickEvent?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDownEvent?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUpEvent?.Invoke(eventData);
    }

    public void UpdateView(DefaultFolderEntity entity)
    {
        nameText.text = entity.DefaultNodeFolder.Name;
    }




    public void RegisterPointerClick(Action<PointerEventData> action)
    {
        onPointerClickEvent += action;
    }

	public void RegisterPointerDown(Action<PointerEventData> action)
	{
        onPointerDownEvent += action;
	}

	public void RegisterPointerUp(Action<PointerEventData> action)
	{
        onPointerUpEvent += action;
	}

}