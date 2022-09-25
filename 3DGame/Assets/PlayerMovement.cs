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
    [SerializeField] AudioClip jump;
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
            transform.position = new Vector3(0f, 0.55f, 0f);
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
            source.clip = jump;
            source.Play();
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            anim.SetBool("isJumping", true);
            isJump = true;
        }
        if (Physics.CheckSphere(groundCheck.position, 0.1f, ground) == true) {
            anim.SetBool("isJumping", false);
            isJump = false;
        }
        else {
            anim.SetBool("isJumping", true);
            
        }
    }
}
