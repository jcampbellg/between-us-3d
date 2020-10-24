using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ImpostorsUI : MonoBehaviour
{
    public Settings settings;
    List<GameObject> impostorsList = new List<GameObject>();
    

    void Start()
    {
        impostorsList = new List<GameObject>(settings.impostorsList);
    }

    void Update()
    {
        if (impostorsList != settings.impostorsList)
		{
            RefreshUI();
        }
    }

    void RefreshUI()
	{
        impostorsList = new List<GameObject>(settings.impostorsList);

		for (int i = 1; i < 5; i++)
		{
            GameObject impostorName = this.transform.GetChild(i).gameObject;

            if (i-1 < impostorsList.Count && impostorsList[i-1] != null)
			{
                impostorName.SetActive(true);
                TextMeshProUGUI text = impostorName.GetComponent<TextMeshProUGUI>();
                Image logo = impostorName.transform.GetChild(0).GetComponent<Image>();

                text.text = impostorsList[i - 1].GetComponent<ClientController>().playerName;
                logo.sprite = impostorsList[i - 1].GetComponent<SkinRenderer>().skin.logo;
            }
            else
			{
                impostorName.SetActive(false);
            }
        }
    }
}
