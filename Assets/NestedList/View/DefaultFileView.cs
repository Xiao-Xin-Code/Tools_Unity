using UnityEngine;
using UnityEngine.UI;

public class DefaultFileView : ItemBaseView
{
	[SerializeField] private Text nameText;


	public void UpdateView(DefaultFileEntity entity)
	{
		nameText.text = entity.DefaultNodeFile.Name;
	}

}