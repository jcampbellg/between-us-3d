using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
	public Settings settings;
	public GameObject mapCamera;
	public GameObject client;
	public RectTransform mapUI;
	public float mouseSensitivity;
	public float scrollSensitivity;
	bool isDragging = false;

	public void OnMapDrag()
	{
		float dragH = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity * 10000 / mapUI.sizeDelta.x;
		float dragV = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity * 10000 / mapUI.sizeDelta.y;

		Vector3 move = new Vector3(dragH, 0f, dragV);

		mapCamera.transform.position = mapCamera.transform.position + move;
		Cursor.lockState = CursorLockMode.Locked;
	}
	public void OnMapDragStart()
	{
		isDragging = true;
	}

	public void OnMapEndDrag()
	{
		Cursor.lockState = CursorLockMode.None;
		isDragging = false;
	}

	private void Update()
	{
		float scroll = Input.GetAxisRaw("Mouse ScrollWheel") * scrollSensitivity * 100000 * Time.deltaTime;
		scroll = Mathf.Clamp(mapUI.sizeDelta.x + scroll, 400, 1500);
		mapUI.sizeDelta = new Vector2(scroll, scroll);

		if (isDragging)
		{
			OnMapDrag();
		}

		if (Input.GetButtonDown("Action"))
		{
			//Center to player
			mapCamera.transform.position = new Vector3(client.transform.position.x, mapCamera.transform.position.y, client.transform.position.z);
		}
	}
	private void OnEnable()
	{
		mapCamera.transform.position = new Vector3(client.transform.position.x, mapCamera.transform.position.y, client.transform.position.z);
	}
}
