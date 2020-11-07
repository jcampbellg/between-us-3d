using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivertedUI : MonoBehaviour
{
    public Slider[] sliders;
    public GameObject[] nextTask;
    public Image icon;
    int n;
    float time = 0f;
    readonly float timeToWait = 2f;

    void Start()
    {
        n = Random.Range(0, sliders.Length);
        sliders[n].interactable = true;
    }

    void Update()
    {
        if (sliders[n].value == 1)
        {
            this.GetComponent<TaskUI>().canClose = false;
            time += Time.deltaTime;
            icon.color = new Color(243f/255f, 159f/255f, 73f/255f);

            if (time > timeToWait)
            {
                nextTask[n].SetActive(true);
                this.GetComponent<TaskUI>().FinishUI();
            }
        }
        else
		{
            icon.color = new Color(1f, 1f, 1f);
            this.GetComponent<TaskUI>().canClose = true;
            time = 0f;
        }
    }
}
