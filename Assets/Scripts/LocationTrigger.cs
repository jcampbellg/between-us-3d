using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    public string locationName;

    private void OnTriggerEnter(Collider other)
	{
		ClientController client = other.gameObject.GetComponent<ClientController>();
		if (client && client.isLocalPlayer)
		{
			other.gameObject.GetComponent<CompasUI>().locationName.text = locationName;
		}
	}
}
