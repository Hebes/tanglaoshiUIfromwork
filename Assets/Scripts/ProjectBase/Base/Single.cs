using UnityEngine;


/// <summary>
/// 以下类没啥用
/// </summary>
public class Single { }


//C#中 泛型知识点
//设计模式 单例模式的知识点
//继承这种自动创建的 单例模式基类 不需要我们手动去拖 或者 api去加了
//想用他 直接 GetInstance就行了
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                //设置对象的名字为脚本名
                obj.name = typeof(T).ToString();
                //让这个单例模式对象 过场景 不移除
                //因为 单例模式对象 往往 是存在整个程序生命周期中的
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }

    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject();
            //设置对象的名字为脚本名
            obj.name = typeof(T).ToString();
            //让这个单例模式对象 过场景 不移除
            //因为 单例模式对象 往往 是存在整个程序生命周期中的
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}

//1.C#中 泛型的知识
//2.设计模式中 单例模式的知识
public class BaseManager<T> where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }

    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}

//C#中 泛型知识点
//设计模式 单例模式的知识点
//继承了 MonoBehaviour 的 单例模式对象 需要我们自己保证它的位移性
//public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
//{
//    private static T instance;

//    public static T Instance => instance;

//    public static T GetInstance()
//    {
//        //继承了Mono的脚本 不能够直接new
//        //只能通过拖动到对象上 或者 通过 加脚本的api AddComponent去加脚本
//        //U3D内部帮助我们实例化它
//        return instance;
//    }
//    protected virtual void Awake()
//    {
//        instance = this as T;
//    }

//}