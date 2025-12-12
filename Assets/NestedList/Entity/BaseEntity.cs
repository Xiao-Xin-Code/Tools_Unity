using QMVC;

/// <summary>
/// ssss
/// </summary>
public abstract class BaseEntity : IEntity
{
	public IArchitecture GetArchitecture()
	{
		return NestedList.Interface;
	}
}