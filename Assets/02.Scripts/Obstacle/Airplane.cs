using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : Obstacle
{
    public GameObject spawnObject;
    public AnimationCurve moveCurve;
    public float upSpeed;
    private float startYPos;
    private float curTime;
    public float timeRate;
    public LayerMask layerMask;
    void Start()
    {
        curTime = 0;
        startYPos = transform.position.y;
    }

    void Update()
    {
        curTime += Time.deltaTime * timeRate;
        Movement();
        SpawnObstacle();
    }

    private void Movement()
    {
        float val = moveCurve.Evaluate(curTime);
        Vector3 pos = transform.position;
        pos.y += val * upSpeed * Time.deltaTime;
        pos.x += Direction.x * speed * Time.deltaTime;
        pos.z += Direction.z * speed * Time.deltaTime;
        transform.position = pos;

        Vector3 rot = transform.localEulerAngles;

        float deg = Mathf.Rad2Deg * val;
        rot.x = -deg;
        transform.localEulerAngles = rot;
    }

    private void SpawnObstacle()
    {
        Ray[] ray = new Ray[3] { new Ray(transform.position,Vector3.down),
        new Ray(new Vector3(transform.position.x ,transform.position.y,transform.position.z+ 2),Vector3.down),
        new Ray(new Vector3(transform.position.x ,transform.position.y,transform.position.z- 2),Vector3.down)};

        for (int i = 0; i < ray.Length; i++)
        {
            Debug.DrawLine(ray[i].origin, ray[i].direction + ray[i].origin);
            if (Physics.Raycast(ray[i], out var hitInfo, 11, layerMask))
            {
                Vector3 spawnPos = hitInfo.transform.position; // 여기서 XZ 값
                spawnPos.y = transform.position.y; // 여기서 Y 값
                hitInfo.transform.gameObject.SetActive(false);
                Instantiate(spawnObject, spawnPos, Quaternion.identity);
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }
}
