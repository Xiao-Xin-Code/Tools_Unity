using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ItemBaseView : BaseController
{
	[SerializeField] private RectTransform rectTransform;
	public RectTransform RectTransform => rectTransform;
	
}