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
    [SerializeField] Animator modern;
    [SerializeField] Animator classic;
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
    public static float ydirection;
    [SerializeField] GameObject shield;
    public float xaxis;
    public AudioSource modernSonic;
    public AudioClip ModernJump;
    public AudioClip ModernWoo;
    public AudioClip ModernFeelingGood;
    public AudioClip coinDeath;
    int counter = 0;
    public AudioClip ModernDeath;
    public AudioClip attack;
    public HealthManager HM;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (SaveCharacter.Character == 1)
            anim = modern;
        else
            anim = classic;
        transform.position = new Vector3(0f, 0.55f, -1.7f);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan time = TimeSpan.FromSeconds(timer.currentTime);
        rightTime.text = time.ToString(@"mm\:ss");
        if (PlayerMovement.isRailGrinding == true)
        {
             transform.position = new Vector3(xaxis, transform.position.y,transform.position.z);
        }
        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (movementSpeed == 6f)
            anim.SetBool("isBoosting", true);
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
            rb.isKinematic = false;
        }    
        coinCount.text = coins.ToString();
        if (transform.position.y < -10f) {
            counter++;
            if (counter == 1)
            {
                modernSonic.clip = ModernDeath;
                modernSonic.Play();
                source.clip = coinDeath;
                source.Play();
                Invoke("SonicDeath", 1f);
            }
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
        if (isDash) {
            transform.eulerAngles = new Vector3(0f, ydirection, 0f);
        }
        if (isRailGrinding)
            rb.AddForce(Vector3.forward * 60);
        if (!isDash)
        {
            if (movementSpeed == 4f){
                GetComponent<TrailRenderer>().enabled = false;
                anim.SetBool("isBoosting", false);
            }
        }
        if (movement != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
            if (!isRailGrinding)
                if (isDash)
                    transform.eulerAngles = new Vector3(0f, ydirection, 0f);
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);
                }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            anim.SetBool("isRunning", true);
        }
        else {
            if (Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true)
                movementSpeed = 4f;
            if (isDash) {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod && !isJump && !isDash)
            {
                Jumping();
                if (SaveCharacter.Character == 2)
                {
                    source.clip = jump;
                    source.Play();
                }
                else  {
                    modernSonic.clip = ModernJump;
                    modernSonic.Play();
                }
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
        xaxis = other.gameObject.GetComponent<rail>().xaxis;
        if (SaveCharacter.Character == 1)  {
                modernSonic.clip = ModernFeelingGood;
                modernSonic.Play();
        }
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
            if (SaveCharacter.Character == 1)  {
                modernSonic.clip = attack;
                modernSonic.Play();
            }
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
            if (SaveCharacter.Character == 1)  {
                modernSonic.clip = attack;
                modernSonic.Play();
            }
            Jumping();
        }
        if (other.gameObject.tag.Equals("speedPanel") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true)
        {
            GetComponent<TrailRenderer>().enabled = true;
            other.gameObject.GetComponent<AudioSource>().Play();
            isDash = true;
            ydirection = other.gameObject.GetComponent<dashPanel>().ydirection;
            wayPoint = other.gameObject.GetComponent<dashPanel>().wayPoint;
            anim.SetBool("isBoosting", true);
            rb.isKinematic = true;
            Invoke("LowerSpeed", 0.1f);
            if (SaveCharacter.Character == 1)  {
                modernSonic.clip = ModernWoo;
                modernSonic.Play();
            }
        }
        if (other.gameObject.tag.Equals("DashRing") && Physics.CheckSphere(groundCheck.position, 0.1f, ground) == false)
        {
            ydirection = other.gameObject.GetComponent<dashPanel>().ydirection;
            wayPoint = other.gameObject.GetComponent<dashPanel>().wayPoint;
            isDash = true;
            rb.isKinematic = true;
        }
        if (other.gameObject.tag.Equals("coin")) {
            coins++;
        }
        if (other.gameObject.tag.Equals("Orange")) {
            coins = HealthManager.myCoin;
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
            isRailGrinding = false;
            anim.SetBool("isRail", false);      
        }
   }
   void SonicDeath()
   {
        coins = 0;
        counter = 0;
        HM.ReloadLevel();
   }
}
