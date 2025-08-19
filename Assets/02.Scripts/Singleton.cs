using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour  
    // <T> <- 이클래스를 제네릭으로 사용하겠다 : MonoBehaviour (where T : MonoBehaviour)  <- 제네릭을 모노비헤비어 만 받을거다 제한
    // List<T> <- 이친구가 제네릭 클래스
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
        if (instance == null) // 생성을안해도 어느 씬이든 사용할수있게끔 
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
