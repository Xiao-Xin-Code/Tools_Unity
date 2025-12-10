using System;
using UnityEngine;

public class UndoRedo : IUndoRedo
{
    private Record record;
    private UndoStack recordStacks;


	
    public UndoRedo(int limit = 20)
    {
        recordStacks = new UndoStack(limit);
    }

    public void Undo()
    {
        if (recordStacks.CanUndo)
        {
            DoUndo();
        }
    }

	public void Redo()
	{
		if (recordStacks.CanRedo)
		{
            DoRedo();
		}
	}

    private void DoUndo()
    {
        Record record = recordStacks.UndoData();
		Debug.Log(record == null);
        record.Undo();
    }

    private void DoRedo()
    {
        Record record = recordStacks.RedoData();
        record.Redo();
    }

	public void Clear()
	{
		recordStacks.ClearInvalid();
		recordStacks = new UndoStack(20);
	}

	public void BeginRecord()
    {
        record = new Record();
	}

    public void EndRecord()
    {
        if (record == null || record.UndoCallBack == null || record.RedoCallBack == null)
        {
			
        }
        else
        {
            recordStacks.Push(record);
        }
        record = null;
    }


	#region 预制设置记录方法

	#region Transform层级记录

	public void BeginRecordTransform(Transform target, Transform parent, Action afterUndo = null)
	{
		if (record != null)
		{
			record.UndoCallBack += record =>
			{
				target.SetParent(parent);
				afterUndo?.Invoke();
			};
		}
	}

	/// <summary>
	/// 设置清空方法
	/// </summary>
	/// <param name="target"></param>
	public void RecordTransformClear(Transform target, Action afterClear = null)
	{
		if (record != null)
		{
			record.ClearCallBack += record =>
			{
				if (target != null)
				{
					GameObject.Destroy(target.gameObject);
					afterClear?.Invoke();
				}
			};
		}
	}

	public void EndRecordTransform(Transform target, Transform parent, Action afterRedo = null)
	{
		if (record != null)
		{
			record.RedoCallBack += record =>
			{
				target.SetParent(parent);
				afterRedo?.Invoke();
			};
		}
	}

	#endregion

	#region 委托记录

	public void BeginRecordAction(Action undoAction)
	{
		if (record != null)
		{
			record.UndoCallBack += record => undoAction?.Invoke();
		}
	}

	public void EndRecordAction(Action redoAction)
	{
		if (record != null)
		{
			record.RedoCallBack += record => redoAction?.Invoke();
		}
	}

    #endregion

    #endregion



    #region 工具

    private delegate void UndoRedoCallback(Record record);

	private class Record
	{
		private UndoRedoCallback m_undoCallback;
		private UndoRedoCallback m_redoCallback;
		private UndoRedoCallback m_clearCallback;

		public UndoRedoCallback UndoCallBack { get => m_undoCallback; set => m_undoCallback = value; }
		public UndoRedoCallback RedoCallBack { get => m_redoCallback; set => m_redoCallback = value; }
		public UndoRedoCallback ClearCallBack { get => m_clearCallback; set => m_clearCallback = value; }

		public void Undo()
		{
			try
			{
				m_undoCallback?.Invoke(this);
			}
			catch
			{

			}
		}

		public void Redo()
		{
			try
			{
				m_redoCallback?.Invoke(this);
			}
			catch
			{

			}
		}

		public void Clear()
		{
			try
			{
				m_clearCallback?.Invoke(this);
			}
			catch
			{

			}
		}

	}

	private class UndoStack
	{
		public class Node
		{
			public Record val;
			public Node prev;
			public Node next;

			public Node() { }

			public Node(Node prev)
			{
				this.prev = prev;
			}

			public Node(Record val, Node prev, Node next)
			{
				this.val = val;
				this.prev = prev;
				this.next = next;
			}
		}

		private Node m_head;//头节点，指向首节点，不记录数据，仅作为占位使用
		private Node m_last;//实际结尾
		private Node m_curmax;
		private Node m_cur;


		public UndoStack(int capacity)
		{
			m_head = new Node();
			Node node = m_head;
			for (int i = 0; i < capacity; i++)
			{
				Node next = new Node(node);
				node.next = next;
				node = next;
			}
			m_last = node;
			m_last.next = m_head;
			m_head.prev = m_last;

			m_cur = m_head;
			m_curmax = m_head;
		}

		public void Push(Record item)
		{
			if (m_cur == m_last)
			{
				m_head = m_head.next;
				m_last = m_last.next;
			}

			ClearInvalid();

			m_cur = m_cur.next;
			m_cur.val = item;
			m_curmax = m_cur;
		}

		public void ClearInvalid()
		{
			if (m_curmax != m_cur)
			{
				Node temp = m_cur.next;
				while (temp != m_curmax.next)
				{
					if (temp.val != null)
					{
						temp.val.Clear();
					}
					temp = temp.next;
					if (temp == m_cur.next) break;
				}
			}
		}

		public bool CanUndo
		{
			get
			{
				if (m_cur == null) return false;
				else
				{
					return m_cur != m_head;
				}
			}
		}

		public bool CanRedo
		{
			get
			{
				if (m_cur == null) return false;
				else
				{
					return m_cur != m_curmax;
				}
			}
		}

		public Record UndoData()
		{
			Record data = m_cur.val;
			m_cur = m_cur.prev;
			return data;
		}

		public Record RedoData()
		{
			m_cur = m_cur.next;
			Record data = m_cur.val;
			return data;
		}
	}

	#endregion
}

