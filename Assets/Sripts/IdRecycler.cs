using System;
using System.Collections.Generic;

public class IdRecycler<T>
{
	protected T _currentMaxId;
	protected Queue<T> _recycledIds = new Queue<T>();
	protected HashSet<T> _activeIds = new HashSet<T>();
	protected Func<T, T> _addFunc;

	public IdRecycler(T initId, Func<T, T> addFunc)
	{
		if (addFunc == null) throw new ArgumentNullException($"addFunc can not Null");
		_currentMaxId = initId;
		_addFunc = addFunc;
	}

	public T GetNewId()
	{
		if(_recycledIds.TryDequeue(out T recycledId))
		{
			_activeIds.Add(recycledId);
			return recycledId;
		}
		T newId = _addFunc(_currentMaxId);
		if (_activeIds.Contains(newId))
		{
			throw new NotSupportedException($"生成了重复数据：{newId},此数据已经被使用，请检查addFunc，或更换新的addFunc");
		}
		_currentMaxId = newId;
		_activeIds.Add(_currentMaxId);
		return _currentMaxId;
	}

	public void ReleaseId(T id)
	{
		if (_activeIds.Remove(id))
		{
			_recycledIds.Enqueue(id);
		}
	}

}