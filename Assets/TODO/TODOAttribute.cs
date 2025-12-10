using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
public class TODOAttribute : PropertyAttribute
{
	public string Description { get; private set; }

	public TODOAttribute(string description)
	{
		Description = description;
	}
}
