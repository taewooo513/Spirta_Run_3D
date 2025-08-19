using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 이 곳에 플레이어가 트리거에 들어왔을 때의 행동을 정의해주세요!
            Destroy(this.transform.parent.gameObject);
        }
    }
}