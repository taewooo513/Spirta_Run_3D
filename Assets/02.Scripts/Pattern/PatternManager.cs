using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatternManager : Singleton<PatternManager>
{
    [SerializeField]
    private GameObject[] patterns;

    public Queue<GameObject> patternQueue;
    public int addStartCount;
    public float spawnDist;

    public void Start()
    {
        patternQueue = new Queue<GameObject>();

        for (int i = 0; i < addStartCount; i++) // º¯¼ö·Î 10
        {
            AddPattern();
        }
    }

    public void AddPattern()
    {
        int randPattern = Random.Range(0, patterns.Length);

        if (patternQueue.Count <= 0)
        {
            patternQueue.Enqueue(Instantiate(patterns[randPattern], Vector3.zero, Quaternion.identity));
        }
        else
        {
            Pattern pattern = patternQueue.Last().transform.GetComponentInChildren<Pattern>();
            Vector3 spawnPos = pattern.transform.position;
            spawnPos.z += spawnDist;
            var obj = Instantiate(patterns[randPattern], spawnPos, Quaternion.identity);
            patternQueue.Enqueue(obj);
        }
    }

    public void PopPattern()
    {
        patternQueue.Dequeue();
        AddPattern();
    }

    public void ClearPatter()
    {
        patternQueue.Clear();
    }
}
