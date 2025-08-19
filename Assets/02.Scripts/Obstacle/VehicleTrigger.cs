using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterManager.Instance.player.TakeDamage(1);
            AudioManager.Instance.PlayCarCrashSound();
            Destroy(this.transform.parent.gameObject);
        }
    }
}