using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeadBody : NetworkBehaviour
{
    public Rigidbody rb;
    float time = 0f;
    readonly float stopTime = 1f;
    [SyncVar]
    public string playerName = "";

    void Update()
    {
        time += Time.deltaTime;
        if (time > stopTime)
		{
            rb.isKinematic = true;
		}
    }
}
