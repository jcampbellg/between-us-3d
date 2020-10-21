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
		List<string> numbersLeft = new List<string>(10);
		numbersLeft.Add("1");
		numbersLeft.Add("2");
		numbersLeft.Add("3");
		numbersLeft.Add("4");
		numbersLeft.Add("5");
		numbersLeft.Add("6");
		numbersLeft.Add("7");
		numbersLeft.Add("8");
		numbersLeft.Add("9");
		numbersLeft.Add("10");

		for (int i = 0; i < 10; i++)
		{
			int n = Random.Range(0, numbersLeft.Count);
			string text = numbersLeft[n];
			numbersLeft.RemoveAt(n);

			buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
			buttons[i].GetComponent<Button>().interactable = true;
		}
	}
	public void OnPress(GameObject button)
	{
		if (button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == ""+atButton)
		{
			button.GetComponent<Button>().interactable = false;
			atButton += 1;
		}
	}
}
