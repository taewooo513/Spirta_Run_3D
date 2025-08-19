using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour  
    // <T> <- ��Ŭ������ ���׸����� ����ϰڴ� : MonoBehaviour (where T : MonoBehaviour)  <- ���׸��� �������� �� �����Ŵ� ����
    // List<T> <- ��ģ���� ���׸� Ŭ����
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject();
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null) // ���������ص� ��� ���̵� ����Ҽ��ְԲ� 
        {
            if (transform.parent == null)
                DontDestroyOnLoad(gameObject);
            instance = GetComponent<T>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
