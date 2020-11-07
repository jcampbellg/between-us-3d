using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DownloadUI : MonoBehaviour
{
    float waitFor;
    public float waiting = 0;
	public Button button;
	public bool isDownloading = false;
	public bool isUpload = false;
	public GameObject uploadTask;

	private void Start()
	{
		waitFor = Random.Range(8.0f, 12.0f);
	}

	private void Update()
	{
		if (isDownloading)
		{
			waiting += Time.deltaTime;
		}

		if (waiting > waitFor)
		{
			if (!isUpload)
			{
				uploadTask.SetActive(true);
			}
			this.GetComponent<TaskUI>().FinishUI();
		}
	}

	public void ClickButton()
	{
		isDownloading = true;
		button.interactable = false;
		button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = isUpload ? "Uploading..." : "Downloading...";
	}

	private void OnEnable()
	{
		waiting = 0;
		isDownloading = false;
		button.interactable = true;
		button.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = isUpload ? "Upload Data" : "Download Data";
	}
}
