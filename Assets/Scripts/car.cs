using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    //fix rotation of car
    //fix gravity of car

    //raycast

    public float moveInput2;
    public float turnInput2;


    public float moveInput;
    public float turnInput;
    private bool isCarGrounded;

    public float airDrag;
    public float groundDrag;

    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask GroundLayer;

    public Rigidbody sphereRB;

    public Animator ani;
    public bool a;

    void Start()
    {
        //detach rigidbody from car
        sphereRB.transform.parent = null;
    }

    void Update()
    {

        if (a == false)
        {
            moveInput = Input.GetAxisRaw("Vertical");
            turnInput = Input.GetAxisRaw("Horizontal");

            //set cars rotation
            float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
            transform.Rotate(0, newRotation, 0, Space.World);
        }
        else
        {
            moveInput = moveInput2;
            turnInput = turnInput2;

            float newRotation = turnInput * turnSpeed * Time.deltaTime * moveInput2;
            transform.Rotate(0, newRotation, 0, Space.World);
        }

       
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;


        //set cars position to sphere
        transform.position = sphereRB.transform.position;




        if (turnInput <= 0.1f)
        {
            ani.Play("left");

        }
        if (turnInput >= 0.1f)
        {
            ani.Play("right");

        }
        if (turnInput == 0)
        {
            ani.Play("stop");

        }

        // raycast ground check
        RaycastHit hit;
        int groundLayer = 1;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, groundLayer);

        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if (isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }
    }

    public void left()
    {
        turnInput2 = 1;
    }
    public void NOleft()
    {
        turnInput2 = 0;
    }


    public void right()
    {
        turnInput2 = -1;
    }
    public void NORright()
    {
        turnInput2 = 0;
    }


    public void move()
    {
        moveInput2 = 1;
    }
    public void NOmove()
    {
        moveInput2 = 0;
    }


    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            //move car
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            //add extra gravity
            sphereRB.AddForce(transform.up * -30);

        }
    }
}
