using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
    #region Components
    private Rigidbody2D rb;
    private Animator anim;
    public Text cryptoText;
    #endregion

    private const int JUMP_LAYER = 9;
    private const int COIN_LAYER = 11;

    #region Attributes
    [Range(0, 100)]
    public float speed = 2.5f;
    [Range(0, 100)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private const float REJUMP_TIME = .25f;
    public float jumpPush = 1.5f;

    public float contactDirection;
    private TrailRenderer trail;
    private int count;
    #endregion

    #region States
    private bool grounded;
    private int jumpCount;
    private bool canJump = true;
    #endregion

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trail = GetComponent<TrailRenderer>();
	}

    private void OnDisable()
    {
        count = 0;
    }

    void Update () {
        float x = Input.GetAxis("Horizontal");
            transform.position += Vector3.right * speed * x * (grounded? 1 : .7f) * Time.deltaTime;


        trail.enabled = contactDirection != 0;

        if (cryptoText != null)
            cryptoText.text = count.ToString();
	}

    private void FixedUpdate()
    {
        bool jump = Input.GetButtonDown("Jump");
        if (jump && jumpCount < 2)
        {
            if (canJump) {
                rb.velocity = (Vector2.up * jumpVelocity * ((contactDirection != 0) ? 1.25f : 1)) +
                    (Vector2.right * contactDirection * jumpPush);
                jumpCount++;

                if (jumpCount == 1)
                    anim.SetTrigger("jump");
            }

            if(jumpCount == 2)
            {
                //CameraShake.Instance.Shake();
            }

            canJump = false;
            Invoke("resetJump", REJUMP_TIME);
        }

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !jump)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == JUMP_LAYER)
        {
            contactDirection = Mathf.RoundToInt(collision.contacts[0].point.x) * -1;
            if (contactDirection > 1)
                contactDirection = 1;
            else if (contactDirection < -1)
                contactDirection = -1;
            Invoke("resetDirection", .5f);
            return;
        }

        contactDirection = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == JUMP_LAYER)
        {
            grounded = true;
            jumpCount = 0;
        }

        if(collision.gameObject.layer == COIN_LAYER)
        {
            collision.gameObject.SetActive(false);
            count++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            canJump = true;
        }
    }

    private void resetJump()
    {
        canJump = true;
    }


    private void resetDirection()
    {
        contactDirection = 0;
    }

}
