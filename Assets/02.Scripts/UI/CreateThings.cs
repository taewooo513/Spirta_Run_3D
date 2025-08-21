using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateThings : MonoBehaviour
{
    [SerializeField] private GameObject[] thingPrefab;
    private void Start()
    {
        InvokeRepeating("CreateThing", 0.0f, 0.2f);
    }

    private void CreateThing()
    {
        float random = Random.Range(0.2f, 1.0f);
        GameObject thing = Instantiate(thingPrefab[Random.Range(0, thingPrefab.Length)], this.transform.position, Random.rotation);
        thing.transform.localScale = new Vector3(random, random, random);
        Rigidbody rb = thing.AddComponent<Rigidbody>();
        rb.mass = 0.1f;
        rb.drag = 2.0f;
        thing.AddComponent<DestroyThing>();
    }
}
