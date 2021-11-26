using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float rotationSpeed = 2.0f;
    float camRotationSpeed = 1.5f;
    float camRotation = 0.0f;
    GameObject cam;
    public float maxSpeed;
    public float normalSpeed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float rotation = 0.0f;
    Rigidbody myRigidbody;

    bool isOnGround;
    public GameObject groundChecker;
    public LayerMask groundLayer;
    public float jumpForce = 300.0f;

    public float maxSprint = 5.0f;
    float sprintTimer;

    public float jumpSpeed = 8.0f;

    Animator myAnim;





    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponentInChildren<Animator>();
        
        Cursor.lockState = CursorLockMode.Locked;

        sprintTimer = maxSpeed;

        //camera controller
        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {

        //Jumping
        isOnGround = Physics.CheckSphere(groundChecker.transform.position, 0.1f, groundLayer);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }
        //sprinting
        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0.0f)
        {
            maxSpeed = sprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        }else
        {
            maxSpeed = normalSpeed;
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                sprintTimer = sprintTimer + Time.deltaTime;
            }
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, maxSprint);


        Vector3 newVelocity = (transform.forward * Input.GetAxis("Vertical") * maxSpeed) + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);
        
        myAnim.SetFloat("speed", newVelocity.magnitude);

        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);
        
        
        //quaternation represents rotation
        //euler angles assign rotation values
        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        //camera controll
        camRotation = camRotation + Input.GetAxis("Mouse Y") * camRotationSpeed;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
  
    }
}