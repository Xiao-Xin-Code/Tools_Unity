
public class UndoRedo : IUndoRedo
{
    private Record record;
    private UndoStack<Record> recordStacks;

    public UndoRedo(int limit = 20)
    {
        recordStacks = new UndoStack<Record>(limit);
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
        record.Undo();
    }

    private void DoRedo()
    {
        Record record = recordStacks.RedoData();
        record.Redo();
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




	#endregion



	#region 工具

	private delegate void UndoRedoCallback(Record record);

	private class Record
	{
		private UndoRedoCallback m_undoCallback;
		private UndoRedoCallback m_redoCallback;

		public UndoRedoCallback UndoCallBack { get => m_undoCallback; set => m_undoCallback = value; }
		public UndoRedoCallback RedoCallBack { get => m_redoCallback; set => m_redoCallback = value; }

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

	}

	private class UndoStack<T> where T : class
	{
		private class Node
		{
			public T val;
			public Node prev;
			public Node next;

			public Node() { }

			public Node(Node prev)
			{
				this.prev = prev;
			}

			public Node(T val, Node prev, Node next)
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

		public void Push(T item)
		{
			if (m_cur == m_last)
			{
				m_head = m_head.next;
				m_last = m_last.next;
			}

			m_cur = m_cur.next;
			m_cur.val = item;
			m_curmax = m_cur;
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

		public T UndoData()
		{
			T data = m_cur.val;
			m_cur = m_cur.next;
			return data;
		}

		public T RedoData()
		{
			m_cur = m_cur.next;
			T data = m_cur.val;
			return data;
		}

	}

	#endregion
}