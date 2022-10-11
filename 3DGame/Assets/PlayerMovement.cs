using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
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
    public float jumpButtonGracePeriod;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    public bool isJump = true;
    Vector3 movement;
    public static bool isRailGrinding = false;
    float horizontalInput;
    float verticalInput;
    public int rail = 0;
    public static bool rail1 = false;
    public static bool rail2 = false;
    [SerializeField] GameObject shield;
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
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (isDash) {
              transform.position = Vector3.MoveTowards(transform.position, wayPoint.transform.position, 14f * Time.deltaTime);
        }
        else
        {
             if (movementSpeed == 4f)
                GetComponent<TrailRenderer>().enabled = false;
        }
        if (Vector3.Distance(transform.position, wayPoint.transform.position) < 0.1f)
        {
            isDash = false;
        }    
        coinCount.text = coins.ToString();
        if (transform.position.y < -15f) {
            transform.position = new Vector3(0f, 0.55f, -1.7f);
        }
        if (!isRailGrinding)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        }
        else 
        {
             movement = new Vector3(0f, 0, 0f).normalized;
        }
        
        if (isRailGrinding)
            rb.AddForce(Vector3.forward * 60);
        if (movement != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            if (!isRailGrinding)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
            else
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod && !isJump)
            {
                Jumping();
                source.clip = jump;
                source.Play();
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        if (rb.velocity.y < -0.1 && isJump)
        {
            isJump = false;
        }
        if (Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true) {
            anim.SetBool("isJumping", false);
            lastGroundedTime = Time.time;
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
    if (other.gameObject.tag.Equals("rail") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true)
    {
        isRailGrinding = true;
        char rails = other.gameObject.name[other.gameObject.name.Length - 1];
        rail = Convert.ToInt32(rails);
        if (rail == 49)
            rail1 = true;
        else if (rail == 50)
            rail2 = true;
        anim.SetBool("isRail", true);
    }
   }

   void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals ("EnemyHead") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false) {
            enemsource.clip = bounce;
            enemsource.Play();
            Destroy(other.gameObject);
            Destroy(other.transform.parent.gameObject);
            Jumping();
        }
        if (other.gameObject.tag.Equals ("Item") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false) {
            enemsource.clip = bounce;
            enemsource.Play();
            Destroy(other.gameObject);
            shield.SetActive(true);
            HealthManager.health = 2;
            Destroy(other.transform.parent.gameObject);
            Jumping();
        }
        if (other.gameObject.tag.Equals("Spring") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false)
        {
            springJumping();
        }
        if (other.gameObject.tag.Equals ("FlyEnemy") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false) {
            enemsource.clip = bounce;
            enemsource.Play();
            Destroy(other.gameObject);
            Jumping();
        }
        if (other.gameObject.tag.Equals("speedPanel") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true)
        {
            GetComponent<TrailRenderer>().enabled = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            isDash = true;
            wayPoint = other.gameObject.GetComponent<dashPanel>().wayPoint;
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
        if (other.gameObject.tag.Equals("rail"))
        {
            if (rail == 49)
                rail1 = false;
            else if (rail == 50)
                rail2 = false;
            if (rail1 == false && rail2 == false)
            {
                isRailGrinding = false;
                anim.SetBool("isRail", false);
                rail = 0;
            }
                
        }
   }
}
