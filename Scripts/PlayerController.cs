using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public Transform enemyLocation;
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed=10;
    public float upSpeed=2;
    //private int score = 0;
    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool countScoreState = false;
    private SpriteRenderer MarioSprite;
    private Animator marioAnimator;
    //public Text scoreText;
    private AudioSource marioAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        MarioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("a") && faceRightState){
            faceRightState= false;
            MarioSprite.flipX = true;
        }
        
        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            MarioSprite.flipX=false;
        }

        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }*/

    }

    void FixedUpdate()
{
    marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

	float moveHorizontal = Input.GetAxis("Horizontal");
    if (Mathf.Abs(moveHorizontal) > 0){
        Vector2 movement = new Vector2(moveHorizontal, 0);
        if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
    }
    if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
        // stop
        marioBody.velocity = Vector2.zero;
    }

    if (Input.GetKeyDown("space") && onGroundState){
        marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        onGroundState = false;

        marioAnimator.SetBool("onGround", onGroundState);

        countScoreState = true;
    }
    if (Input.GetKeyDown("a") && faceRightState){
        faceRightState= false;
        MarioSprite.flipX = true;
        if (Mathf.Abs(marioBody.velocity.x) > 1.0){
            marioAnimator.SetTrigger("onSkid");
        }
    }
        
    if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            MarioSprite.flipX=false;
        if (Mathf.Abs(marioBody.velocity.x) > 1.0){
            marioAnimator.SetTrigger("onSkid");
        }
        }
}

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Shrooms")) 
        {
            onGroundState = true;
            marioAnimator.SetBool("onGround", onGroundState);
            /*countScoreState = false;
            scoreText.text = "Score: " + score.ToString();*/
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        /*if (other.gameObject.CompareTag("Enemy")){
            Time.timeScale = 0.0f;
        }*/
    }
    
    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
