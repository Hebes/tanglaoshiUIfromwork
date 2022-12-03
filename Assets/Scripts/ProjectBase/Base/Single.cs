using UnityEngine;


/// <summary>
/// ������ûɶ��
/// </summary>
public class Single { }


//C#�� ����֪ʶ��
//���ģʽ ����ģʽ��֪ʶ��
//�̳������Զ������� ����ģʽ���� ����Ҫ�����ֶ�ȥ�� ���� apiȥ����
//������ ֱ�� GetInstance������
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
                //���ö��������Ϊ�ű���
                obj.name = typeof(T).ToString();
                //���������ģʽ���� ������ ���Ƴ�
                //��Ϊ ����ģʽ���� ���� �Ǵ��������������������е�
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
            //���ö��������Ϊ�ű���
            obj.name = typeof(T).ToString();
            //���������ģʽ���� ������ ���Ƴ�
            //��Ϊ ����ģʽ���� ���� �Ǵ��������������������е�
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

//1.C#�� ���͵�֪ʶ
//2.���ģʽ�� ����ģʽ��֪ʶ
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

//C#�� ����֪ʶ��
//���ģʽ ����ģʽ��֪ʶ��
//�̳��� MonoBehaviour �� ����ģʽ���� ��Ҫ�����Լ���֤����λ����
//public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
//{
//    private static T instance;

//    public static T Instance => instance;

//    public static T GetInstance()
//    {
//        //�̳���Mono�Ľű� ���ܹ�ֱ��new
//        //ֻ��ͨ���϶��������� ���� ͨ�� �ӽű���api AddComponentȥ�ӽű�
//        //U3D�ڲ���������ʵ������
//        return instance;
//    }
//    protected virtual void Awake()
//    {
//        instance = this as T;
//    }

//}