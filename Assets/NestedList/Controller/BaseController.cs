using QMVC;
using UnityEngine;

public abstract class BaseController : MonoBehaviour, IController
{
	public IArchitecture GetArchitecture()
    {
		return NestedList.Interface;
	}
}