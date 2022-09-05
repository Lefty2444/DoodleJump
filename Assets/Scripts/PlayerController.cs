using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 5;
    public float sideForce = .5f;
    public float squishAnimationTime = .2f;
    public float unsquishAnimationTime = .5f;
    public Vector2 bounds = new Vector2(-3, 3);
    public Vector2 squishScale;
    public Transform gameOverScreen;
    public int score = 0;
    public TMP_Text scoreText;

    private bool colliding = false;
    private Rigidbody2D rigidBody;
    private Vector2 scale;
    

    private void Awake()
    {
        scale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigidBody.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal") * sideForce);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, bounds.x, bounds.y), transform.position.y);

        if (colliding && rigidBody.velocity.y <= 0)
        {
            Jump();
        }
    }


    void Jump()
    {
        rigidBody.velocity += Vector2.up * jumpVelocity;
        StopAllCoroutines();
        StartCoroutine(Squish());
        IncrementScore();
    }

    void IncrementScore ()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!colliding) {
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }

    IEnumerator Squish()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / squishAnimationTime)
        {
            transform.localScale = Vector2.Lerp(scale, squishScale, t);
            yield return null;
        }
        for (float t = 0; t < 1; t += Time.deltaTime / unsquishAnimationTime)
        {
            transform.localScale = Vector2.Lerp(squishScale, scale, t);
            yield return null;
        }
    }

    private void OnBecameInvisible()
    {
        Time.timeScale = 0;
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.GetComponent<GameOverScreen>().ShowScore(score);
    }
}
