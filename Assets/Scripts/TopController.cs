using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopController : MonoBehaviour
{
    public Camera mainCamera;

    // Horizontal movement
    Vector2 vel;
    float smoothAmount;

    // Vertical movement
    float jumpHeight = 0.05f;
    float timeToJumpApex = 0.5f;
    float gravity;
    float jumpVelocity;

    float moveAmountY;
    bool grounded;
    bool doubleJumpEnabled;
    bool doubleJumped;

    bool yeeted;

    void Start()
    {
        // Horizontal stuff
        smoothAmount = ((GameManager.instance.repairTime * 0.6f) * 6) / 100f;

        // Vertical stuff
        timeToJumpApex = Mathf.Clamp(0.75f - ((GameManager.instance.repairTime * 2) / 100f), 0.25f, 0.75f);
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        // Check if we should enable double jump
        if (GameManager.instance.repairTime <= 15)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            doubleJumpEnabled = true;
        }
    }

    void Update()
    {
        if (!yeeted)
        {
            // Horizontal movement
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.SmoothDamp(transform.position, new Vector2(mousePos.x, transform.position.y), ref vel, smoothAmount);

            if (transform.position.x < GameManager.instance.screenLeftEdge) transform.position = new Vector2(GameManager.instance.screenLeftEdge, transform.position.y);
            if (transform.position.x > GameManager.instance.screenRightEdge) transform.position = new Vector2(GameManager.instance.screenRightEdge, transform.position.y);

            // Vertical movement
            if (Input.GetMouseButtonDown(0))
            {
                if (grounded)
                {
                    grounded = false;
                    moveAmountY = jumpVelocity;
                }
                else if (doubleJumpEnabled && !doubleJumped)
                {
                    doubleJumped = true;
                    moveAmountY = jumpVelocity;
                }
            }

            moveAmountY += gravity * Time.deltaTime;

            transform.Translate(new Vector2(0, moveAmountY));

            if (transform.position.y < -3.1f)
            {
                transform.position = new Vector2(transform.position.x, -3.1f);
                moveAmountY = 0;
                grounded = true;
                doubleJumped = false;
            }

            // Top spin time
            GameManager.instance.topSpinTime += Time.deltaTime;
        }
    }

    public void Yeet()
    {
        if (!yeeted)
        {
            yeeted = true;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = false;
            rb.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(4f, 6f)), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-50, 50));
            LeanTween.delayedCall(gameObject, 3f, () =>
            {
                FadeOutSceneChange.instance.FadeOut("ResultsScene", 1f);
            });
        }
    }
}
