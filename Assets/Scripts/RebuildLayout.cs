using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebuildLayout : MonoBehaviour
{
	public RectTransform rect;
	private void OnEnable()
	{
		StartCoroutine("UpdateLayout");
	}

	IEnumerator UpdateLayout()
	{
		yield return new WaitForSeconds(Time.deltaTime);
		LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
	}
}