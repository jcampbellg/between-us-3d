using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerSettings playerSettings;
    GameSettings gameSettings;
    public Animator animator;
    public bool canUse = false;
    public bool hasGravity = true;

    private void Start()
    {
        GameObject gameStateObject = GameObject.FindGameObjectWithTag("GameState");
        playerSettings = gameStateObject.GetComponent<PlayerSettings>();
        gameSettings = gameStateObject.GetComponent<GameSettings>();
    }
    void Update()
    {
        if (canUse)
        {
            float speed = gameSettings.playerSpeed;
            Vector3 move = Vector3.zero;

            if (!playerSettings.isMenuOpen)
			{
                move = GetInputMovement();
                characterController.Move(move * speed * Time.deltaTime);
			}

            animator.SetFloat("speed", move.magnitude);

            if (hasGravity)
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
