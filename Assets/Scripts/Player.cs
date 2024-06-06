
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;
    [SerializeField] private float jumpForce = 100f;
    private Animator anim;
    private Rigidbody rigidBody;
    private bool jump = false;
    private SphereCollider sphereCollider;
    private AudioSource audioSource;

    void Awake()
    {

        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);

    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.PlayerStartedGame();
                anim.Play("Jump");
                audioSource.PlayOneShot(sfxJump);
                rigidBody.useGravity = true;
                jump = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            jump = false;
            rigidBody.velocity = new Vector3(0, 0, 0);
            rigidBody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            rigidBody.AddForce(new Vector3(-100, 20, 0), ForceMode.Impulse);
            rigidBody.detectCollisions = false;
            audioSource.PlayOneShot(sfxDeath);
            rigidBody.useGravity = true;
            GameManager.instance.PlayerCollided();
            
        }
    }
}

