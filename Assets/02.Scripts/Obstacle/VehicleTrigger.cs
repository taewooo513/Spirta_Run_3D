using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayCarCrashSound();
            CharacterManager.Instance.player.TakeDamage(1);
            Destroy(this.transform.parent.gameObject);
        }
    }
}