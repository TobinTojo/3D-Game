using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] Animator anim;
    Rigidbody rb;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource enemsource;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip bounce;

    public bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15f) {
            transform.position = new Vector3(0f, 0.55f, -1.7f);
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (movement != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
            anim.SetBool("isRunning", true);
        }
        else {
            anim.SetBool("isRunning", false);
        }
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && Physics.CheckSphere(groundCheck.position, .1f, ground) == true) {
           Jumping();
           source.clip = jump;
           source.Play();
        }
        if (Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true) {
            anim.SetBool("isJumping", false);
            isJump = false;
        }
        else {
            anim.SetBool("isJumping", true);
            
        }
        
    }
   void Jumping() {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        anim.SetBool("isJumping", true);
        isJump = true;
   }
   void OnCollisionEnter(Collision other) {
    if (other.gameObject.tag.Equals ("MovingPlatform") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true)
	{
		this.transform.parent = other.transform;
	}
   }

   void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals ("EnemyHead") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false) {
            enemsource.clip = bounce;
            enemsource.Play();
            Destroy(other.transform.parent.gameObject);
            Jumping();
        }
   }
   void OnCollisionExit(Collision other)
   {
	    if (other.gameObject.tag.Equals ("MovingPlatform"))
	    {
		    this.transform.parent = null;
	    }
   }
}
