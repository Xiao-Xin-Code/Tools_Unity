
using UnityEngine;
using UnityEngine.UI;



public class Test : MonoBehaviour
{
    public Transform parent;
    public Transform prefab;
    public Button addBtn;

    private UndoRedo undo;

    // Start is called before the first frame update
    void Start()
    {
        undo = new UndoRedo();
        addBtn.onClick.AddListener(AddPressed);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                undo.Undo();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                undo.Redo();
            }
        }
    }

    private void AddPressed()
    {
        Transform target = GameObject.Instantiate(prefab, parent);
        undo.BeginRecord();
        undo.BeginRecordTransform(target, null);
        undo.RecordTransformClear(target);
        undo.EndRecordTransform(target, parent);
        undo.EndRecord();
    }
}
