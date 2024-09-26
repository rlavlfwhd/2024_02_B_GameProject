using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static readonly object _lock = new object();

    private static bool isQuitting = false;

    public static T Instance
    {
        get
        {
            if (isQuitting)
            {
                Debug.Log($"[�̱���] '{typeof(T)}' �ν��Ͻ��� ���ø����̼� ���� �߿� ���� �ǰ� �ֽ��ϴ�. ����Ʈ ��ü ������ ���� null ��ȯ");
                return null;
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject singletonObject = new GameObject($"{typeof(T)}(Singleton");
                        instance = singletonObject.AddComponent<T>();

                        DontDestroyOnLoad(singletonObject);

                        Debug.Log($"[�̱���]{typeof(T)} �� �ν��Ͻ��� DonDestroyOnLoad�� �����Ǿ����ϴ�. ");
                    }
                }
                return instance;
            }

        }
    }
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Debug.LogWarning($"[�̱���] {typeof(T)}�� �ٸ� �ν��Ͻ��� �̹� �����մϴ�. ���ߺ��� �ı��մϴ�. ");
        }
    }

    protected virtual void OnApplicationQuit()
    {
        isQuitting = true;
    }
    protected virtual void OnDestroy()
    {
        if(!isQuitting)
        {
            Debug.LogWarning($"[�̱���]{typeof(T)}�� �ν��Ͻ��� ���ø����̼� ���ᰡ �ƴ� �������� �ı�, ��������");
        }

        isQuitting=true;
    }
    private System.Collections.IEnumerator ExcuteOnNextFrame(Action action)
    {
        yield return null;
        action();
    }
}


