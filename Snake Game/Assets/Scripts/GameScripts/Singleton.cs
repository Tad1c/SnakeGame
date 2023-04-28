using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    InitInstance();
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        RemoveDuplicateInstance();
    }

    private static void InitInstance()
    {
        GameObject singletonObject = new GameObject
        {
            name = typeof(T).Name
        };
        
        instance = singletonObject.AddComponent<T>();
        DontDestroyOnLoad(instance);
    }
    
    private void RemoveDuplicateInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
    }

}
