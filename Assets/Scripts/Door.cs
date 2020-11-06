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
            if ((openVector - transform.localPosition).magnitude > 0.01f)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, openVector, speed * Time.deltaTime);
            }
            else
                transform.localPosition = openVector;
        }
        else
		{
            if ((closeVector - transform.localPosition).magnitude > 0.01f)
            {
                transform.localPosition = Vector3.Slerp(transform.localPosition, closeVector, speed * Time.deltaTime);
            }
            else
                transform.localPosition = closeVector;
        }
    }
}
