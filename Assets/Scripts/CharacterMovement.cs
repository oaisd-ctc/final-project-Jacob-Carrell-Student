using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController characterController;

    public float moveSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float jumpForce = 5.0f;
    public float gravityMult = 0.05f;
    public bool isFalling = false;
    private Vector3 moveDirection;
    private Vector3 forces;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.Normalize();
        moveDirection.y = -1f;
        isFalling = !Physics.Raycast(transform.position, Vector3.down, 1.1f);
        RaycastHit  hit;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = 5.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isFalling == false)
        {
            forces = Vector3.up * jumpForce;
            isFalling = true;
        }

        if (Physics.SphereCast(transform.position, 1f, Vector3.down, out hit, 1.5f))
        {
            isFalling = false;
        }

        if (isFalling)
        {
            forces += Physics.gravity * gravityMult * Time.deltaTime;
        }
        

        characterController.Move(((moveDirection * moveSpeed) + forces) * Time.deltaTime);
    }

    public void AddMoveInput(float forwardInput, float rightInput)
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = (forwardInput * forward) + (rightInput * right);
    }
}
