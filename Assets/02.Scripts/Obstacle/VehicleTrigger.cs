using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �� ���� �÷��̾ Ʈ���ſ� ������ ���� �ൿ�� �������ּ���!
            Destroy(this.transform.parent.gameObject);
        }
    }
}