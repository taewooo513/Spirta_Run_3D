using System;
using UnityEngine;

namespace _02.Scripts.map
{
    public class MapRemove : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
            Debug.Log("충돌 감지!");
            other.transform.position = new Vector3(0f, 0f, 258f);
        }
    }
}
