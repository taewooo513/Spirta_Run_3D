using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Item : MonoBehaviour
{
    public UnityEvent useItemAction; //������ ���� ȣ���ϴ� �Լ�

    public float maxTime; // �������ӽð�
    public float val; // ��ġ ex). �̼����� �󸶳��Ǵ�����

    public void GetItem()
    {
        useItemAction?.Invoke();
        Destroy(gameObject);
    }
}
