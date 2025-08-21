using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Item : MonoBehaviour
{
    public UnityEvent useItemAction; //아이템 사용시 호출하는 함수

    public float maxTime; // 버프지속시간
    public float val; // 수치 ex). 이속증가 얼마나되는지등

    public void GetItem()
    {
        Debug.Log("teaw");
        useItemAction?.Invoke();
        Debug.Log("teaw");
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<CapsuleCollider>().isTrigger = true;
    }
}