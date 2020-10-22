using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController characterController;
    public Settings settings;
    public Animator animator;
    public bool canUse = false;

    void Update()
    {
        if (canUse)
        {
            float speed = settings.playerSpeed;

            if (!settings.isMenuOpen)
			{
                Vector3 move = GetInputMovement();
                characterController.Move(move * speed * Time.deltaTime);

                Transform camPivot = Camera.main.transform.parent;
                camPivot.position = transform.position;

                animator.SetFloat("speed", move.magnitude);
			}

            characterController.Move(Vector3.up * -10f * Time.deltaTime);
        }
        
    }
    private Vector3 GetInputMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float angle = Vector2.Angle(new Vector2(1, 0), new Vector2(xInput, zInput));
        float radians = angle / 180 * Mathf.PI;
        float zMaxMagnitud = Mathf.Sin(radians);
        float xMaxMagnitud = Mathf.Cos(radians);

        float xValue = Mathf.Abs(xMaxMagnitud) * xInput;
        float zValue = Mathf.Abs(zMaxMagnitud) * zInput;

        return transform.right * xValue + transform.forward * zValue;
    }
}
