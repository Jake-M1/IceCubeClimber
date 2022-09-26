using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    public float moveRange = 4f;
    public float speed = 2f;
    private float startX;
    private float currX;
    private float leftBoundaryX;
    private float rightBoundaryX;
    private Transform e_Transform;
    private Rigidbody2D e_Rigidbody2D;
    private bool e_FacingRight = true;
    private Vector3 e_Velocity = Vector3.zero;
    private float e_MovementSmoothing = .05f;
    private float xVelocity;

    [SerializeField] private LayerMask e_WhatIsGround;
	[SerializeField] private Transform e_GroundCheck;
	const float groundRad = .2f;
	private bool e_Grounded;

    private bool inBackground;
    public Animator enemy_animator;
    private SpriteRenderer e_SpriteRenderer;
    private BoxCollider2D e_BoxCollider;
    public bool startBG;

    void Start()
    {
        e_SpriteRenderer = GetComponent<SpriteRenderer>();
        e_BoxCollider = GetComponent<BoxCollider2D>();
        e_Transform = GetComponent<Transform>();
        e_Rigidbody2D = GetComponent<Rigidbody2D>();
        startX = e_Transform.position.x;
        leftBoundaryX = startX - moveRange;
        rightBoundaryX = startX + moveRange;
        xVelocity = speed;
        transform.Rotate(0f, 180f, 0f);
        
        enemy_animator.SetBool("Background", inBackground);
        if (startBG == true)
        {
            FlipGroundEnemyBackground();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Flip Background"))
        {
            FlipGroundEnemyBackground();
        }

        EnemyGroundCheck();
        currX = e_Transform.position.x;
        if (e_Grounded && !inBackground)
        {
            EnemyGroundMove();
        }
        if (inBackground)
        {
            EnemyGroundMoveBackground();
        }
    }

    private void EnemyGroundMove()
    {
        Vector3 targetVelocity = new Vector2(xVelocity, e_Rigidbody2D.velocity.y);
		e_Rigidbody2D.velocity = Vector3.SmoothDamp(e_Rigidbody2D.velocity, targetVelocity, ref e_Velocity, e_MovementSmoothing);

		if (currX > rightBoundaryX && e_FacingRight)
		{
			FlipGroundEnemy();
		}
		else if (currX < leftBoundaryX && !e_FacingRight)
		{
			FlipGroundEnemy();
		}
    }

    private void FlipGroundEnemy()
	{
		e_FacingRight = !e_FacingRight;
		transform.Rotate(0f, 180f, 0f);
        xVelocity = -xVelocity;
	}


    private void EnemyGroundCheck()
    {
		e_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(e_GroundCheck.position, groundRad, e_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				e_Grounded = true;
			}
		}
    }


    private void FlipGroundEnemyBackground()
    {
        if (inBackground == false)
        {
            e_SpriteRenderer.sortingLayerName = "Background";
            e_BoxCollider.enabled = false;
            enemy_animator.SetBool("Background", true);
            e_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            inBackground = true;
        }
        else
        {
            e_SpriteRenderer.sortingLayerName = "Foreground";
            e_BoxCollider.enabled = true;
            enemy_animator.SetBool("Background", false);
            e_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            e_Rigidbody2D.AddForce(new Vector2(0, -0.01f));

            inBackground = false;
        }
    }


    private void EnemyGroundMoveBackground()
    {
        if (!e_Grounded)
        {
            e_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            e_Rigidbody2D.AddForce(new Vector2(0, -0.01f));
        }
        else
        {
            //e_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            
            e_Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            EnemyGroundMove();
        }
    }
}
