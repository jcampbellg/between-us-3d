using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    PlayerSettings playerSettings;
    public Transform bone;
    public bool canUse = false;

    float xRotation = 0f;
    private void Start()
    {
        playerSettings = GameObject.FindGameObjectWithTag("GameState").GetComponent<PlayerSettings>();
        xRotation = gameObject.transform.rotation.eulerAngles.x;
    }
    public void RefreshXRotation()
	{
        xRotation = gameObject.transform.rotation.eulerAngles.x;
    }
    void Update()
    {
        if (canUse && !playerSettings.isMenuOpen)
        {
            float mouseSensitivity = playerSettings.mouseSensitivity;
            Transform cam = Camera.main.transform;
            Transform camPivot = Camera.main.transform.parent;

            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 30f);
            Vector3 currentRotation = cam.localRotation.eulerAngles;
            currentRotation.x = xRotation;
            cam.localRotation = Quaternion.Euler(currentRotation);
            camPivot.rotation = transform.rotation;

            Vector3 currentBoneRotation = bone.localRotation.eulerAngles;
            currentBoneRotation.x = xRotation;
            bone.localRotation = Quaternion.Euler(currentBoneRotation);

            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
