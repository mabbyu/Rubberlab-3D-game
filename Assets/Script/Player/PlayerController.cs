using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed = 12f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool airJump;

    public AudioClip getHit;

    public GameObject spawnPoint;

    //health
    //public GameObject healthLogo;

    public float currentHealth;
    public float maxHealth;

    private bool isDead;

    //footstep
    bool isWalking;

    public float FootstepDelayTime;

    float wt;

    public AudioSource audioFootstep;

    public GameObject walkAnim;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //double jump
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            airJump = true;
        }
        else
        {
            if (airJump)
            {
                
                if (Input.GetButtonDown("Jump"))
                {
                    airJump = false;
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            GameController.instance.LoseGame();
        }

        //footstep
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) 
        {
            isWalking = true;
            wt -= Time.deltaTime;
            walkAnim.GetComponent<Animation>().CrossFade("walk");
        }
        else
        {
            isWalking = false;
            audioFootstep.Stop();
            walkAnim.GetComponent<Animation>().CrossFade("idle");
        }

        if(wt <= 0)
        {
            wt = FootstepDelayTime;
            if(isGrounded)
            audioFootstep.Play();
        }
    }

    public void ApplyDamage(float damage)
    {
        //currentHealth -= damage;
        GameController.instance.currScore -= damage;

        GetComponent<AudioSource>().PlayOneShot(getHit);
    }

    public void ApplyDamageGuru(float damage)
    {
        currentHealth -= damage;
        //GameController.instance.currScore -= damage;

        GetComponent<AudioSource>().PlayOneShot(getHit);
        transform.position = spawnPoint.transform.position;
        transform.rotation = spawnPoint.transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("damage"))
        {
            gameObject.SendMessage("ApplyDamageGuru", 10, SendMessageOptions.DontRequireReceiver);
        }
    }
}