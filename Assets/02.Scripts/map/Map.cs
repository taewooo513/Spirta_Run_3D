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
            if (GameManager.Instance.map == null)
            {
                GameManager.Instance.map = new Map[2];
                GameManager.Instance.map[0] = this;
            }
            else
            {//
                if (GameManager.Instance.map[1] == null)
                {
                    GameManager.Instance.map = new Map[2];
                    GameManager.Instance.map[0] = this;
                }
                else
                {
                    GameManager.Instance.map[1] = this;
                }
            }
        }
        void Update()
        {
            transform.Translate(Vector3.back * (Time.deltaTime * speed), Space.World);
        }
    }
}
