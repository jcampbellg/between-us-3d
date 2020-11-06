using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockUI : MonoBehaviour
{
	public GameObject[] buttons;
	[SerializeField] int atButton = 1;
	private void OnEnable()
	{
		atButton = 1;
		List<string> numbersLeft = new List<string>();

		for (int i = 0; i < buttons.Length; i++)
		{
			numbersLeft.Add(""+(i+1));
		}

		for (int i = 0; i < buttons.Length; i++)
		{
			int n = Random.Range(0, numbersLeft.Count);
			string text = numbersLeft[n];
			numbersLeft.RemoveAt(n);

			buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
			buttons[i].GetComponent<Button>().interactable = true;
		}
	}
	private void Update()
	{
		if (atButton == buttons.Length+1)
		{
			this.GetComponent<TaskUI>().FinishUI();
		}
	}
	public void OnPress(GameObject button)
	{
		if (button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == ""+atButton)
		{
			button.GetComponent<Button>().interactable = false;
			atButton += 1;
		}
		else
		{
			atButton = 1;
			foreach (GameObject but in buttons)
			{
				but.GetComponent<Button>().interactable = true;
			}
		}
	}
}
