using QMVC;
using UnityEngine;

public abstract class BaseController : MonoBehaviour, IController
{
	[TODO("获取IArchitecture")]
	public IArchitecture GetArchitecture()
    {
        throw new System.NotImplementedException();
    }
}