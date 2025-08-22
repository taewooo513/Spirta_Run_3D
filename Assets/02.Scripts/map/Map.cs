using UnityEngine;

namespace _02.Scripts.map
{
    public class Map : MonoBehaviour
    {
        public float speed;
        private void Awake()
        {

        }

        private void Start()
        {
            if (GameManager.Instance.map[0] == null)
                GameManager.Instance.map[0] = this;
            else
                GameManager.Instance.map[1] = this;
        }
        void Update()
        {
            transform.Translate(Vector3.back * (Time.deltaTime * speed), Space.World);
        }
    }
}
