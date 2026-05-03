using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;
    
    /// <summary>
    /// Getter para obtener la instancia
    /// </summary>
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindOrCrateInstance();
            }
            return _instance;
        }
    }

    /// <summary>
    /// Encuentra o crea alguna estancia que ya estuviese en la escena
    /// </summary>
    /// <returns></returns>
    
    private static T FindOrCrateInstance()
    {

        var instanceInScene = FindAnyObjectByType<T>();

        if (instanceInScene != null)
        {
            return instanceInScene;
        }

        var name = typeof(T).Name + " Singleton";

        var containerGameObject = new GameObject(name);
        var singletonComponent = containerGameObject.AddComponent<T>();

        return singletonComponent;
    }

    public static void Remove()
    {
        Remove(0);
    }
    public static void Remove(float timer)
    {
        if (_instance != null)
        {
            Destroy(_instance, timer);
            _instance = null;
        }
    }


    public static void RemoveGameObject()
    {
        RemoveGameObject(0);
    }

    public static void RemoveGameObject(float timer)
    {
        if (_instance != null)
        {
            Destroy(_instance.gameObject, timer);
            _instance = null;
        }
    }

    public static T Create()
    {
        return instance;
    }
}