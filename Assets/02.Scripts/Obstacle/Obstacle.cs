using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public Vector3 Direction;

    void Update()
    {
        if (speed != 0)
            Movement();
    }

    private void Movement()
    {
        transform.position += Direction * (speed + GameManager.Instance.map.speed) * Time.deltaTime;
    }
}
