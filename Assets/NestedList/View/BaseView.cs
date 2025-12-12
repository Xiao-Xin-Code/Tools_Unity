using QMVC;
using UnityEngine;

public abstract class BaseView : MonoBehaviour, IView
{
	public IArchitecture GetArchitecture()
	{
		return NestedList.Interface;
	}
}