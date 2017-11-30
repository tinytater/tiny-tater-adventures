using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour 
{
	private Rigidbody2D myRigidBody;
	private Animator myAnimator;

	[SerializeField]
	private float movementSpeed;

	private bool facingRight;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatIsGround;

	private bool isGrounded;
	private bool jump;

	[SerializeField]
	private bool airControl;

	[SerializeField]
	private float jumpForce;

    [SerializeField]
    public AudioClip soundFile;

    [SerializeField]
    public AudioClip deathSound;
    public bool dead = false;

    [SerializeField]
    public int pears;

    public AudioSource myAS;

    private string[] cheatCode;
    private int index;



    // Use this for initialization.
    void Start()
	{
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        facingRight = true;
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
        myAS = allMyAudioSources[1];
        cheatCode = new string[] { "up", "up", "down","down","left","right","b","a" };
        index = 0;
    }

	void Update()
	{
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[index]))
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }


        if (index == cheatCode.Length)
        {
            SceneManager.LoadScene("Credits");
        }
        HandleInput();
        var myPosX = transform.position.x;
        var myPosY = transform.position.y;

        if (Mathf.Approximately(myPosX, 36.1f) && Mathf.Approximately(myPosY, -4.872916f)) 
        {
            if (GameManager.Instance.CollectedPears == pears)
            {
                SceneManager.LoadScene("Good Ending");
            }
            else
            {
                SceneManager.LoadScene("Bad Ending");
            }
        }

        if (myPosX <= -15.57)
        {
            transform.position = new Vector2(-15.57f, transform.position.y);
        }
        else if (myPosX >= 36.1)
        {
            transform.position = new Vector2(36.1f, transform.position.y);
        }

        if (myPosY <= -20.0 && !dead)
        {
            dead = true;

            Death();
        }
    }
	
	// Update is called once per frame.
	void FixedUpdate() 
	{
		float horizontal = Input.GetAxis("Horizontal");

		isGrounded = IsGrounded();

		HandleMovement(horizontal);
		Flip(horizontal);
		ResetValues();
		
	}

	private void HandleMovement (float horizontal)
	{
		if (isGrounded || airControl)
		{
			myRigidBody.velocity = new Vector2 (horizontal * movementSpeed, myRigidBody.velocity.y);
		}
		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));  // changes speed based on left/right input to transition between idle/run 


		if (isGrounded && jump) 
		{
			isGrounded = false;
			myRigidBody.AddForce (new Vector2 (0, jumpForce));
		}
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			jump = true;
		}
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
	}

	private void Flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
		{
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
		}
		
	}

	private void ResetValues()
	{
		jump = false;
	}

	private bool IsGrounded()
	{
		if (myRigidBody.velocity.y <= 0)
		{
			foreach (Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);

				for (int i = 0; i < colliders.Length; i++) 
				{
					if (colliders [i].gameObject != gameObject) // if current collider isn't player 
					{ 
						return true; // we are colliding
					}
				}
			}
		}
		return false;
	}

    public void Death()
    {
        myAnimator.Play("Death");
        movementSpeed = 0;
        myAS.Stop();
        transform.
        GetComponent<AudioSource>().PlayOneShot(deathSound, 1.0f);
        StartCoroutine(Wait(1.5f));
        
    }


    IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        transform.position = new Vector2(-15.24f, .05f);
        dead = false;
        yield return 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pear")
        {
            
            GameManager.Instance.CollectedPears++;
            GetComponent<AudioSource>().PlayOneShot(soundFile, 0.5f);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        }
    }
}

