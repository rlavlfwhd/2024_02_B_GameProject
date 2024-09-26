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
                Debug.Log($"[싱글톤] '{typeof(T)}' 인스턴스가 애플리케이션 종료 중에 접근 되고 있습니다. 고스ㅜ트 객체 방지를 위해 null 반환");
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

                        Debug.Log($"[싱글톤]{typeof(T)} 의 인스턴스가 DonDestroyOnLoad로 생성되었습니다. ");
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
            Debug.LogWarning($"[싱글톤] {typeof(T)}의 다른 인스턴스가 이미 존재합니다. 이중복을 파괴합니다. ");
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
            Debug.LogWarning($"[싱글톤]{typeof(T)}의 인스턴스가 애플리케이션 종료가 아닌 시점에서 파괴, 문제가됨");
        }

        isQuitting=true;
    }
    private System.Collections.IEnumerator ExcuteOnNextFrame(Action action)
    {
        yield return null;
        action();
    }
}


