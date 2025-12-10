using QMVC;
using UnityEngine;

public abstract class BaseView : MonoBehaviour, IView
{
	[TODO("获取IArchitecture")]
	public IArchitecture GetArchitecture()
	{
		throw new System.NotImplementedException();
	}
}