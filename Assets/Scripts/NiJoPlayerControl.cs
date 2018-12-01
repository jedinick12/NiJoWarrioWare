using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NiJoPlayerControl : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody2D rb2d;
    private int count;

    public Text endText;
    public GameObject droplet;

    private float timer;
    private int wholetime;

    private bool timerText;
    private AudioSource Waterdroplet;



    void Start()
    {
        timer = 0.0f;
        timerText = true;
        rb2d = GetComponent<Rigidbody2D>();
        Waterdroplet = GetComponent<AudioSource>();
        count = 0;
        winText.text = "Catch The Water";
        SetCountText();
    }


    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);
        rb2d.AddForce(movement * speed);
        timer = timer + Time.deltaTime;
        if (timer > 2.0f && timerText)
        {
            winText.text = "";
            timerText = false;
        }
        timer = timer + Time.deltaTime;
        if (timer >= 10 && count <= 10)
        {
            endText.text = "You Lose! :(";
            StartCoroutine(ByeAfterDelay(2));

        }

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            //GameLoader.AddScore(count);
            Instantiate(droplet, transform.position, Quaternion.identity);
            Waterdroplet.Play();

        }
    }

    void SetCountText()
    {
        countText.text = "POINTS!!: " + count.ToString();
        if (count >= 10 && timer <= 10)
        {
            endText.text = "You win";
            StartCoroutine(ByeAfterDelay(2));
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        //GameLoader.gameOn = false;
    }
}
