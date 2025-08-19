using System;
using UnityEngine;

namespace _02.Scripts.map
{
    public class MapRemove : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {

            if (other.transform.TryGetComponent(out Pattern pattern))
            {
                PatternManager.Instance.PopPattern();
                Destroy(pattern.gameObject);
            }
            else
            {
                other.transform.position = new Vector3(0f, 0f, 258f);
            }
        }
    }
}
