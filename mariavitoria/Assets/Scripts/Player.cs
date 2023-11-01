using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    public float speed;
    public float gravity;

    private Animator anim;

    private Transform cam;

    
     Vector3 moveDirection;
    
    public float smoothRotTime;
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if(controller.isGrounded)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if(direction.magnitude > 0)
        {
             float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
             float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnSmoothVelocity, smoothRotTime);

             transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

             moveDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * speed;
             
        } 
        else 
        { 
             moveDirection = Vector3.zero;
        }
       }

      moveDirection.y -= gravity * Time.deltaTime;

      controller.Move(moveDirection * Time.deltaTime);

    }
}

