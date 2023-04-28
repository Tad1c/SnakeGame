using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : class
{
    private Stack<T> stack;
    private Func<T> CreateFunc;
    private Action<T> ActionGet;
    private Action<T> ActionStore;
    private Action<T> ActionDestroy;

    private int maxSize;

    private int sizeOfElements;

    private int InactiveObj => stack.Count;

    public ObjectPool(Func<T> createFunc, Action<T> actionGet, Action<T> actionStore, Action<T> actionDestroy,
        int defaultSize, int maxSize)
    {
        stack = new Stack<T>(defaultSize);
        CreateFunc = createFunc;
        ActionGet = actionGet;
        ActionStore = actionStore;
        ActionDestroy = actionDestroy;
        this.maxSize = maxSize;
    }

    public T Get()
    {
        T obj;

        if (stack.Count == 0)
        {
            obj = CreateFunc();
        }
        else
        {
            obj = stack.Pop();
        }

        ActionGet?.Invoke(obj);
        return obj;
    }

    public void Store(T obj)
    {
        ActionStore?.Invoke(obj);

        if (InactiveObj < maxSize)
        {
            stack.Push(obj);
        }
        else
        {
            ActionDestroy?.Invoke(obj);
        }
    }
}
