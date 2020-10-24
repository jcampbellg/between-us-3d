using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Door : NetworkBehaviour
{
    [SyncVar]
    public bool isOpen = false;
    public Vector3 closeVector;
    public Vector3 openVector;
	readonly float speed = 2f;

	private void Update()
	{
        if (isOpen)
		{
            if ((openVector - transform.position).magnitude > 0.01f)
            {
                transform.position = Vector3.Slerp(transform.position, openVector, speed * Time.deltaTime);
            }
            else
                transform.position = openVector;
        }
        else
		{
            if ((closeVector - transform.position).magnitude > 0.01f)
            {
                transform.position = Vector3.Slerp(transform.position, closeVector, speed * Time.deltaTime);
            }
            else
                transform.position = closeVector;
        }
    }
}
