using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    Rigidbody rigidbody;
    float time = 0f;
    readonly float stopTime = 1f;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > stopTime)
		{
            rigidbody.isKinematic = true;
		}
    }
}
