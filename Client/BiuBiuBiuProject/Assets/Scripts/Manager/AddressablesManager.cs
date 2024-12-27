using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

//��Ѱַ��Դ ��Ϣ
public class AddressablesInfo
{
    //��¼ �첽�������
    public AsyncOperationHandle handle;
    //��¼ ���ü���
    public uint count;

    public AddressablesInfo(AsyncOperationHandle handle)
    {
        this.handle = handle;
        count += 1;
    }
}

public class AddressablesManager : SingletonManager<AddressablesManager>
{

    //��һ������ �������Ǵ洢 �첽���صķ���ֵ
    public Dictionary<string, AddressablesInfo> resDic = new Dictionary<string, AddressablesInfo>();

    private AddressablesManager() { }

    public void LoadAssetAsync<T>(string name, Action<T> callBack)
    {
        LoadAssetAsync<T>(name, (AsyncOperationHandle<T> handle) =>
        {
            if (!handle.IsDone || handle.Status == AsyncOperationStatus.Failed)
            {
                callBack(default);
            }
            callBack(handle.Result);
        });
    }

    //�첽������Դ�ķ���
    public void LoadAssetAsync<T>(string name, Action<AsyncOperationHandle<T>> callBack)
    {
        //���ڴ���ͬ�� ��ͬ������Դ�����ּ���
        //��������ͨ�����ֺ�����ƴ����Ϊ key
        string keyName = name + "_" + typeof(T).Name;
        AsyncOperationHandle<T> handle;
        //����Ѿ����ع�����Դ
        if (resDic.ContainsKey(keyName))
        {
            //��ȡ�첽���ط��صĲ�������
            handle = resDic[keyName].handle.Convert<T>();
            //Ҫʹ����Դ�� ��ô���ü���+1
            resDic[keyName].count += 1;
            //�ж� ����첽�����Ƿ����
            if(handle.IsDone)
            {
                //����ɹ� �Ͳ���Ҫ�첽�� ֱ���൱��ͬ�������� ���ί�к��� �����Ӧ�ķ���ֵ
                callBack(handle);
            }
            //��û�м������
            else
            {
                //������ʱ�� ��û���첽������� ��ô����ֻ��Ҫ ������ ���ʱ��ʲô������
                handle.Completed += (obj) => {
                    if (obj.Status == AsyncOperationStatus.Succeeded)
                        callBack(obj);
                };
            }
            return;
        }
        
        //���û�м��ع�����Դ
        //ֱ�ӽ����첽���� ���Ҽ�¼
        handle = Addressables.LoadAssetAsync<T>(name);
        handle.Completed += (obj)=> {
            if (obj.Status == AsyncOperationStatus.Succeeded)
                callBack(obj);
            else
            {
                Debug.LogWarning(keyName + "��Դ����ʧ��");
                if(resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        };
        AddressablesInfo info = new AddressablesInfo(handle);
        resDic.Add(keyName, info);
    }


    //�첽���ض����Դ ���� ����ָ����Դ
    public void LoadAssetAsync<T>(Addressables.MergeMode mode, Action<T> callBack, params string[] keys)
    {
        //1.����һ��keyName  ֮�����ڴ��뵽�ֵ���
        List<string> list = new List<string>(keys);
        string keyName = "";
        foreach (string key in list)
            keyName += key + "_";
        keyName += typeof(T).Name;
        //2.�ж��Ƿ�����Ѿ����ع������� 
        //������ʲô
        AsyncOperationHandle<IList<T>> handle;
        if (resDic.ContainsKey(keyName))
        {
            handle = resDic[keyName].handle.Convert<IList<T>>();
            //Ҫʹ����Դ�� ��ô���ü���+1
            resDic[keyName].count += 1;
            //�첽�����Ƿ����
            if (handle.IsDone)
            {
                foreach (T item in handle.Result)
                    callBack(item);
            }
            else
            {
                handle.Completed += (obj) =>
                {
                    //���سɹ��ŵ����ⲿ�����ί�к���
                    if(obj.Status == AsyncOperationStatus.Succeeded)
                    {
                        foreach (T item in handle.Result)
                            callBack(item);
                    }
                };
            }
            return;
        }
        //��������ʲô
        handle = Addressables.LoadAssetsAsync<T>(list, callBack, mode);
        handle.Completed += (obj) =>
        {
            if(obj.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError("��Դ����ʧ��" + keyName);
                if (resDic.ContainsKey(keyName))
                    resDic.Remove(keyName);
            }
        };
        AddressablesInfo info = new AddressablesInfo(handle);
        resDic.Add(keyName, info);
    }


    //�ͷ���Դ�ķ��� 
    public void Release<T>(string name)
    {
        //���ڴ���ͬ�� ��ͬ������Դ�����ּ���
        //��������ͨ�����ֺ�����ƴ����Ϊ key
        string keyName = name + "_" + typeof(T).Name;
        if(resDic.ContainsKey(keyName))
        {
            //�ͷ�ʱ ���ü���-1
            resDic[keyName].count -= 1;
            //������ü���Ϊ0  ���������ͷ�
            if(resDic[keyName].count == 0)
            {
                //ȡ������ �Ƴ���Դ ���Ҵ��ֵ������Ƴ�
                AsyncOperationHandle<T> handle = resDic[keyName].handle.Convert<T>();
                Addressables.Release(handle);
                resDic.Remove(keyName);
            }
        }
    }

    public void Release<T>(params string[] keys)
    {
        //1.����һ��keyName  ֮�����ڴ��뵽�ֵ���
        List<string> list = new List<string>(keys);
        string keyName = "";
        foreach (string key in list)
            keyName += key + "_";
        keyName += typeof(T).Name;
        
        if(resDic.ContainsKey(keyName))
        {
            resDic[keyName].count -= 1;
            if(resDic[keyName].count == 0)
            {
                //ȡ���ֵ�����Ķ���
                AsyncOperationHandle<IList<T>> handle = resDic[keyName].handle.Convert<IList<T>>();
                Addressables.Release(handle);
                resDic.Remove(keyName);
            }
            
        }
    }

    //�����Դ
    public void Clear()
    {
        foreach (var item in resDic.Values)
        {
            Addressables.Release(item.handle);
        }
        resDic.Clear();
        AssetBundle.UnloadAllAssetBundles(true);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}
