using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 15f;
    public float jumpForce = 15f;
    public float rotationSpeed = 15f;
    public bool grounded = false;
    public LayerMask groundLayer;
    private Animator anim;
    private Vector3 moveDir;
    private Vector3 rotDir;
    private Rigidbody rb;
    private bool bAllowJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update ()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(0, 0, z).normalized;
        rotDir = new Vector3(0, x, 0).normalized;

        print(grounded);
 
	}

    private void FixedUpdate()
    {
        anim.SetBool("OnGround", grounded);
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotDir * rotationSpeed * Time.deltaTime));

        RaycastHit hit;

        if(Physics.Raycast(transform.position + (transform.up * 0.1f),-transform.up, out hit, 0.2f, groundLayer))
        {
            grounded = true;
            anim.SetFloat("Jump", 0);
            rb.drag = 5;
            bAllowJump = true;
        }
        else
        {
            grounded = false;
            bAllowJump = false;
            
        }
 
        if(Input.GetButton("Jump") && bAllowJump)
        {
            rb.AddForce(transform.TransformDirection(Vector3.up) * jumpForce, ForceMode.Impulse);
            anim.SetFloat("Jump", 5); 
            rb.drag = 0;

        }

        if(moveDir != Vector3.zero)
        {
            anim.SetFloat("Forward", 1);
           
        }
        else
        {
            anim.SetFloat("Forward", 0);
        }

        Debug.DrawRay(transform.position, -transform.up,Color.red);
    }

    public Vector3 GetMoveDir()
    {
        return moveDir;
    }

}
