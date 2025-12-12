
using UnityEngine;
using UnityEngine.UI;

public class DefaultFolderView : ItemBaseView
{

	[SerializeField] private Text nameText;


    public void UpdateView(DefaultFolderEntity entity)
    {
        nameText.text = entity.DefaultNodeFolder.Name;
    }

}