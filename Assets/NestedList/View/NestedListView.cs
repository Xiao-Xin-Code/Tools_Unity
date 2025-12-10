using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NestedListView : BaseView
{
	[SerializeField] private ScrollRect scroll;
	[SerializeField] private float indentPerLevel = 20f;


	public ScrollRect Scroll { get => scroll; private set => scroll = value; }
	public float IndentPerLevel { get => indentPerLevel; private set => indentPerLevel = value; }


	#region Register
	public void RegisterScrollValueChanged(UnityAction<Vector2> action)
	{
		scroll.onValueChanged.AddListener(action);
	}
	#endregion

	#region UnRegister
	public void UnRegisterScrollValueChanged(UnityAction<Vector2> action)
	{
		scroll.onValueChanged.RemoveListener(action);
	}
	#endregion
}