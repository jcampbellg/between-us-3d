using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public Settings settings;
    public Animator animator;
    public Transform bone;
    public bool canUse = false;

    float xRotation = 0f;

	void Update()
    {
        if (canUse)
        {
            float mouseSensitivity = settings.mouseSensitivity;
            Transform cam = Camera.main.transform;
            Transform camPivot = Camera.main.transform.parent;

            float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 45f);
            Vector3 currentRotation = cam.localRotation.eulerAngles;
            currentRotation.x = xRotation;
            cam.localRotation = Quaternion.Euler(currentRotation);
            camPivot.rotation = transform.rotation;

            xRotation = Mathf.Clamp(xRotation, -30f, 30f);
            Vector3 currentBoneRotation = bone.localRotation.eulerAngles;
            currentBoneRotation.x = xRotation;
            bone.localRotation = Quaternion.Euler(currentBoneRotation);

            transform.Rotate(Vector3.up * mouseX);
            animator.SetFloat("verticalRotation", xRotation);
        }
    }
}
