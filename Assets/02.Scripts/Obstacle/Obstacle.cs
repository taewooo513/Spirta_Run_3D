using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public Vector3 Direction;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Debug.Log(GameManager.Instance.map[0].speed);
        transform.position += Direction * (speed - GameManager.Instance.map[0].speed) * Time.deltaTime;
    }
}
