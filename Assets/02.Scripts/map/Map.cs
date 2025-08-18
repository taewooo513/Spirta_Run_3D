using UnityEngine;

namespace _02.Scripts.map
{
    public class Map : MonoBehaviour
    {
        public float speed;

        void Update()
        {
            transform.Translate(Vector3.back * (Time.deltaTime * speed), Space.World);
        }
    }
}
