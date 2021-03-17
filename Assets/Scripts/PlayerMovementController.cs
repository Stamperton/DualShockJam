using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    Transform camTransform;
    CharacterController charController;

    //MouseParameters
    [SerializeField] bool invertMouse = false;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float verticalRotationLimit = 80f;

    //Movement Parameters
    [SerializeField] bool airControl = true;
    [SerializeField] float playerWalkingSpeed = 5f;
    [SerializeField] float playerRunningSpeed = 7.5f;
    [SerializeField] float jumpStrength = 20f;

    //Movement Variables
    float forwardsMovement;
    float sidewaysMovement;
    float verticalVelocity;

    float verticalRotation = 0;

    private void Start()
    {
        HideCursor(true);

        camTransform = GetComponentInChildren<Camera>().transform;
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        MouseLook();
    }

    #region Look and Move Functions

    void MouseLook()
    {
        float mouseInputX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseInputY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        mouseInputY = Mathf.Clamp(mouseInputY, -verticalRotationLimit, verticalRotationLimit);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInputX, transform.rotation.eulerAngles.z);
        if (invertMouse)
            camTransform.localRotation = Quaternion.Euler(camTransform.localRotation.eulerAngles.x + mouseInputY, camTransform.localRotation.eulerAngles.y, camTransform.localRotation.eulerAngles.z);
        else
            camTransform.localRotation = Quaternion.Euler(camTransform.localRotation.eulerAngles.x - mouseInputY, camTransform.localRotation.eulerAngles.y, camTransform.localRotation.eulerAngles.z);

        //camTransform.localRotation = Quaternion.Euler(camTransform.localRotation.eulerAngles + , 0);
    }

    void Movement()
    {
        if (charController.isGrounded || airControl)
        {
            forwardsMovement = Input.GetAxis("Vertical") * playerWalkingSpeed;
            sidewaysMovement = Input.GetAxis("Horizontal") * playerWalkingSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardsMovement = Input.GetAxis("Vertical") * playerRunningSpeed;
                sidewaysMovement = Input.GetAxis("Horizontal") * playerRunningSpeed;
            }
        }

        verticalVelocity += (Physics.gravity.y * 1.5f) * Time.deltaTime;

        if (Input.GetButton("Jump") && charController.isGrounded)
        {
            verticalVelocity = jumpStrength;
        }

        Vector3 playerMovement = new Vector3(sidewaysMovement, verticalVelocity, forwardsMovement);


        charController.Move(transform.rotation * playerMovement * Time.deltaTime);
    }

    #endregion

    #region Utility Functions
    void HideCursor(bool _state)
    {
        Debug.Log("Mouse Hidden");
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

    }
    #endregion

}
