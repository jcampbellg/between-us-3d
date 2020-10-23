using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameUI : MonoBehaviour
{
    public Settings settings;
    public int placement;
    TextMeshProUGUI text;
    public GameObject logo;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        if (settings.playersList.Count > placement)
		{
            GameObject player = settings.playersList[placement];
            text.text = player.GetComponent<ClientController>().playerName;
            logo.GetComponent<Image>().sprite = player.GetComponent<SkinRenderer>().skin.logo;
        }
    }
}
