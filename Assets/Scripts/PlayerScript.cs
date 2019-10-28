using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text winText;
    public Text livesText;

    private int scoreValue = 0;
    private int level = 1;
    private int lives = 3;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        SetLivesText();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    void Update()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue == 4)
            {
                if(level == 1)
                {
                    scoreValue = 0;
                    score.text = scoreValue.ToString();
                    transform.position = new Vector2(42.84f, 2f);
                    level = 2;
                    lives = 3;
                    SetLivesText();
                }
                else
                {
                    winText.text = "You Win! Game created by Chris Tillis.";
                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                }
            }
        }

        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You Lose!";
            this.gameObject.SetActive(false);
        }
    }
}