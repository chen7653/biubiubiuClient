using System;
using System.Reflection;
using UnityEngine;


/// <summary>
/// �Զ�����ʽ�� �̳�Mono�ĵ���ģʽ����
/// �����ֶ����� ���趯̬��� ��������г�������������
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonMono<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                //��̬���� ��̬����
                //�ڳ����ϴ���������
                GameObject obj = new GameObject();
                //�õ�T�ű������� Ϊ������� �����ٱ༭���п�����ȷ�Ŀ�����
                //����ģʽ�ű�����������GameObject
                obj.name = typeof(T).ToString();
                //��̬���ض�Ӧ�� ����ģʽ�ű�
                instance = obj.AddComponent<T>();
                //������ʱ���Ƴ����� ��֤����������Ϸ���������ж�����
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

}

/// <summary>
/// ����ģʽ���� ��ҪĿ���Ǳ����������� ��������ʵ�ֵ���ģʽ����
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonManager<T> where T : class//,new()
{
    private static T instance;

    //�жϵ���ģʽ���� �Ƿ�Ϊnull
    protected bool InstanceisNull => instance == null;

    //���ڼ����Ķ���
    protected static readonly object lockObj = new object();

    //���Եķ�ʽ
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        //instance = new T();
                        //���÷���õ��޲�˽�еĹ��캯�� �����ڶ����ʵ����
                        Type type = typeof(T);
                        ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                    null,
                                                                    Type.EmptyTypes,
                                                                    null);
                        if (info != null)
                            instance = info.Invoke(null) as T;
                        else
                            Debug.LogError("û�еõ���Ӧ���޲ι��캯��");

                        //instance = Activator.CreateInstance(typeof(T), true) as T;
                    }
                }
            }
            return instance;
        }
    }
}