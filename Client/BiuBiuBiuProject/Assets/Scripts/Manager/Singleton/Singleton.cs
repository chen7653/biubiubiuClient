using System;
using System.Reflection;
using UnityEngine;


/// <summary>
/// 自动挂载式的 继承Mono的单例模式基类
/// 无需手动挂载 无需动态添加 无需关心切场景带来的问题
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
                //动态创建 动态挂载
                //在场景上创建空物体
                GameObject obj = new GameObject();
                //得到T脚本的类名 为对象改名 这样再编辑器中可以明确的看到该
                //单例模式脚本对象依附的GameObject
                obj.name = typeof(T).ToString();
                //动态挂载对应的 单例模式脚本
                instance = obj.AddComponent<T>();
                //过场景时不移除对象 保证它在整个游戏生命周期中都存在
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

}

/// <summary>
/// 单例模式基类 主要目的是避免代码的冗余 方便我们实现单例模式的类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonManager<T> where T : class//,new()
{
    private static T instance;

    //判断单例模式对象 是否为null
    protected bool InstanceisNull => instance == null;

    //用于加锁的对象
    protected static readonly object lockObj = new object();

    //属性的方式
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
                        //利用反射得到无参私有的构造函数 来用于对象的实例化
                        Type type = typeof(T);
                        ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                                    null,
                                                                    Type.EmptyTypes,
                                                                    null);
                        if (info != null)
                            instance = info.Invoke(null) as T;
                        else
                            Debug.LogError("没有得到对应的无参构造函数");

                        //instance = Activator.CreateInstance(typeof(T), true) as T;
                    }
                }
            }
            return instance;
        }
    }
}