using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private GameManager gameManager;
    private Camera mainCam;
    private Rigidbody2D rb;
    private bool isLeft, isRight;
    private ScoreHandler scoreManager;
    private Vector2 InputDir;
    private float horizontal, vertical;
    public float moveSpeed;
    public float rotationSpeed;
    public AudioSource audioSource;

    public JoyStick MoveJoyStick;
    public JoyStick ShootJoyStick;
    public float backgroundSpeed;
    public AudioClip[] audioClips;
    [SerializeField] private Renderer background;
    
   
   

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        scoreManager = FindObjectOfType<ScoreHandler>();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isLeft && !isRight)
        {
            RotateBackGround();
        }

        Inputs();
        CheckPosition();
        Rotation();
        BackgroundMovement();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Inputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        InputDir = new Vector2(horizontal, vertical);
    }
    private void BackgroundMovement()
    {
        if (MoveJoyStick.InputDir.x > 0)
        {
            print("1");
            background.material.mainTextureOffset += new Vector2(-backgroundSpeed * Time.deltaTime, 0);
        }
        else if (MoveJoyStick.InputDir.x < 0)
        {
            print("2");
            background.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
        }
    }
    void Movement()
    {
       
        rb.AddForce(InputDir * moveSpeed); 
        
        if(MoveJoyStick.InputDir.magnitude>0)
        {
            rb.AddForce(MoveJoyStick.InputDir * moveSpeed);
        }   
      
    }

    void RotateBackGround()
    {
        if(horizontal>0)
        {
            background.material.mainTextureOffset += new Vector2(-backgroundSpeed * Time.deltaTime,0);
        }
        else if (horizontal< 0)
        {
            background.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
        }
        else
        {
        background.material.mainTextureOffset += new Vector2(0,-backgroundSpeed * Time.deltaTime);
        }
    }

    void Rotation()
    {
        float angle = Mathf.Atan2(MoveJoyStick.InputDir.y, MoveJoyStick.InputDir.x) * Mathf.Rad2Deg+90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }


    void CheckPosition()
    {
        float screenWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float screenHight = mainCam.orthographicSize * 2;

        float rightEdge =screenWidth/2;
        float leftEdge = rightEdge*-1;
        float topEdge =screenHight/2;
        float bottomEdge = topEdge*-1;
       

        if (transform.position.x > rightEdge)
        {
           transform.position = new Vector2(leftEdge, transform.position.y);
        }

        if (transform.position.x < leftEdge)
        {
            transform.position = new Vector2(rightEdge, transform.position.y);
        }

        if (transform.position.y > topEdge)
        {
            transform.position = new Vector2(transform.position.x, bottomEdge);
        }

        if (transform.position.y < bottomEdge)
        {
            transform.position = new Vector2(transform.position.x, topEdge);
        }
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.tag== "Enemy")
        {
            gameManager.GameOver();
            obj.gameObject.GetComponent<Collider2D>().enabled = false;
            audioSource.Play();
            Destroy(obj.gameObject);
            PlayAudio(audioClips[0]);
            scoreManager.HighScoreMethod();
        }
        else if(obj.gameObject.tag == "Coin")
        {
            PlayAudio(audioClips[1]);
            obj.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(obj.gameObject);
            ScoreHandler.score++;
        }
       
    }
    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
