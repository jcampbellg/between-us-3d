using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompasUI : MonoBehaviour
{
    public RectTransform compas;
    public TextMeshProUGUI locationName;

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(0f, 0f, this.gameObject.transform.rotation.eulerAngles.y + 180);
        compas.rotation = Quaternion.Euler(rot);
    }
}
