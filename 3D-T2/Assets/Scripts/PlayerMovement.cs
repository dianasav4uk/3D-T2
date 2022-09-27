using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Transform cameraPosition;
    public float mouseSemitivity;
    public bool invertX;
    public bool invertY;
    [SerializeField] float rotationSpeed = 90f;
    public float jumpHeight = 25f;
    private float gravity = -50f;


    private CharacterController characterController;

    private Vector3 movementVector;
    //private Vector2 rotationVector;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //movementVector.x = Input.GetAxis("Horizontal")*movementSpeed*Time.deltaTime;
        //movementVector.z = Input.GetAxis("Vertical")*movementSpeed*Time.deltaTime;
        Vector3 movementVertical = Input.GetAxis("Vertical") * transform.forward;
        Vector3 movementHorizontal = Input.GetAxis("Horizontal") * transform.right;

        movementVector = movementHorizontal + movementVertical;
        movementVector.Normalize();

        movementVector.y += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            if (!characterController.isGrounded)
            {
                return;
            }
            if (characterController.isGrounded)
            {
                movementVector.y = jumpHeight;
            }
        }

        movementVector = movementVector * movementSpeed * Time.deltaTime;        

        characterController.Move(movementVector);

        Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSemitivity;

        if (invertX)
        {
            mouseVector.x = -mouseVector.x;
        }
        if (invertY)
        {
            mouseVector.y = -mouseVector.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseVector.x, transform.rotation.eulerAngles.z);

        cameraPosition.rotation = Quaternion.Euler(cameraPosition.rotation.eulerAngles + new Vector3(mouseVector.y * -1, 0f, 0f));

        
    }
}
