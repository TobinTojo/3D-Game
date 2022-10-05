using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerMovement : MonoBehaviour
{   [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float springForce = 8f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    [SerializeField] Animator anim;
    Rigidbody rb;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource enemsource;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip bounce;
    public static int coins;
    private bool isDash;
    [SerializeField] Text coinCount;
    [SerializeField] Transform wayPoint;
    [SerializeField] Text rightTime;
    public bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(timer.currentTime);
        rightTime.text = time.ToString(@"mm\:ss");
        if (isDash) {
              transform.position = Vector3.MoveTowards(transform.position, wayPoint.transform.position, 14f * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, wayPoint.transform.position) < 0.1f)
        {
            isDash = false;
        }    
        coinCount.text = coins.ToString();
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
            movementSpeed = 4f;
            anim.SetBool("isBoosting", false);
            if (!isDash)
                GetComponent<TrailRenderer>().enabled = false;
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
   void springJumping() {
        rb.velocity = new Vector3(rb.velocity.x, springForce, rb.velocity.z);
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
        if (other.gameObject.tag.Equals("Spring") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false)
        {
            springJumping();
        }
        if (other.gameObject.tag.Equals("speedPanel") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true)
        {
            GetComponent<TrailRenderer>().enabled = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            isDash = true;
            anim.SetBool("isBoosting", true);
            Invoke("LowerSpeed", 0.1f);
        }
        if (other.gameObject.tag.Equals("coin")) {
            coins++;
        }
   }
   void LowerSpeed() {
        movementSpeed = 6f;
   }
   void OnCollisionExit(Collision other)
   {
	    if (other.gameObject.tag.Equals ("MovingPlatform"))
	    {
		    this.transform.parent = null;
	    }
   }
}
