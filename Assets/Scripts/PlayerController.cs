using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 120f;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = 0.05f;
	[SerializeField] private bool m_AirControl = true;
	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;

	[SerializeField] private Transform m_GroundCheck2;
	[SerializeField] private Transform m_GroundCheck3;
	[SerializeField] private Transform m_GroundCheck4;

	const float k_GroundedRadius = 0.3f;
	private bool m_Grounded;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
		{
			OnLandEvent = new UnityEvent();
		}	
	}


	private void FixedUpdate()
	{
		m_Grounded = false;

		if (CheckGroundCollider(m_GroundCheck, k_GroundedRadius, m_WhatIsGround) == true)
		{
			m_Grounded = true;
		}
		if (CheckGroundCollider(m_GroundCheck2, k_GroundedRadius, m_WhatIsGround) == true)
		{
			m_Grounded = true;
		}
		if (CheckGroundCollider(m_GroundCheck3, k_GroundedRadius, m_WhatIsGround) == true)
		{
			m_Grounded = true;
		}
		if (CheckGroundCollider(m_GroundCheck4, k_GroundedRadius, m_WhatIsGround) == true)
		{
			m_Grounded = true;
		}
	}


	public void Move(float move, bool jump)
	{
		if (m_Grounded || m_AirControl)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}
		if (jump)
		{
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f, 0f);
	}


	public bool isGrounded()
	{
		return m_Grounded;
	}


	private bool CheckGroundCollider(Transform checkTransform, float radius, LayerMask groundMask)
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(checkTransform.position, radius, groundMask);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				return true;
			}
		}

		return false;
	}


	public void PlayerKnockback(int strength, bool up)
	{
		if (up == true)
		{
			m_Rigidbody2D.AddForce(new Vector2(strength * 5, Mathf.Abs(strength)));
		}
		else
		{
			m_Rigidbody2D.AddForce(new Vector2(strength * 5, 0));
		}
		
	}


	public void PlayerKnockbackUp(int strength)
	{
		m_Rigidbody2D.velocity = new Vector2(0, 0);
		m_Rigidbody2D.AddForce(new Vector2(0, Mathf.Abs(strength)));
	}
}
