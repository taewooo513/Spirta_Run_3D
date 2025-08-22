using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThing : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.position.y < -10.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
