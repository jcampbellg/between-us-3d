using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivertedUI : MonoBehaviour
{
    public Slider[] sliders;
    public GameObject[] nextTask;
    int n;

    void Start()
    {
        n = Random.Range(0, sliders.Length);
        sliders[n].interactable = true;
    }

    void Update()
    {
        if (sliders[n].value == 1)
		{
            nextTask[n].SetActive(true);
            this.GetComponent<TaskUI>().FinishUI();
        }
    }
}
