using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiresUI : MonoBehaviour
{
	public GameObject[] wires;
	public GameObject[] endings;
	public List<Color> colors;
	public List<Color> colorsLeft;

	[SerializeField] GameObject wireDragging = null;
	private void OnEnable()
	{
		SetWiresColors();
		SetEndingsColors();
	}

	void SetEndingsColors()
	{
		colorsLeft = new List<Color>(colors);

		for (int i = 0; i < 5; i++)
		{
			int n = Random.Range(0, colorsLeft.Count);
			Color color = colorsLeft[n];
			colorsLeft.RemoveAt(n);

			endings[i].GetComponent<Image>().color = color;
		}
	}
	void SetWiresColors()
	{
		colorsLeft = new List<Color>(colors);

		for (int i = 0; i < 5; i++)
		{
			int n = Random.Range(0, colorsLeft.Count);
			Color color = colorsLeft[n];
			colorsLeft.RemoveAt(n);

			wires[i].GetComponent<Image>().color = color;
		}
	}

	public void OnWireDrag(GameObject wire)
	{
		RectTransform rect = wire.GetComponent<RectTransform>();
		Vector3 distance = rect.InverseTransformPoint(Input.mousePosition);

		float stretch = distance.magnitude-1;
		float angle = Vector2.Angle(Vector2.right, distance);
		if (distance.y < 0)
		{
			angle = -angle;
		}
		
		rect.sizeDelta = new Vector2(stretch, 10f);
		Vector3 euler = rect.rotation.eulerAngles;
		rect.rotation = Quaternion.Euler(new Vector3(0f, 0f, euler.z + angle));

		wireDragging = wire;
	}

	public void OnWireDragEnd()
	{
		if (wireDragging)
		{
			RectTransform rect = wireDragging.GetComponent<RectTransform>();
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10);
			rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 10);
			rect.SetPositionAndRotation(rect.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			wireDragging = null;
		}
	}

	public void OnWireDrop(GameObject end)
	{
		if (wireDragging)
		{
			RectTransform rect = wireDragging.GetComponent<RectTransform>();
			if (end.GetComponent<Image>().color == wireDragging.GetComponent<Image>().color)
			{
				Vector3 distance = rect.InverseTransformPoint(end.GetComponent<RectTransform>().position);

				float stretch = distance.magnitude;
				float angle = Vector2.Angle(Vector2.right, distance);
				if (distance.y < 0)
				{
					angle = -angle;
				}

				rect.sizeDelta = new Vector2(stretch, 10f);
				Vector3 euler = rect.rotation.eulerAngles;
				rect.rotation = Quaternion.Euler(new Vector3(0f, 0f, euler.z + angle));
			}
			else
			{
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10);
				rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 10);
				rect.SetPositionAndRotation(rect.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
			}

			wireDragging = null;
		}
	}
}
