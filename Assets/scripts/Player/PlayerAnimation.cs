using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private Animator anim;
	public int jumpCount;
	public int jumpCountMax;
	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundRadius = 0.2f;

	void Start()
	{
		anim = GetComponent<Animator>();

		


	}
	void Update()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		if (grounded)
		{
			jumpCount = 0;
		}
		if (grounded && Input.GetButton("Horizontal"))
		{
			anim.SetBool("isRunning", true);
		}
		else
		{
			anim.SetBool("isRunning", false);
		}


		if (jumpCount < jumpCountMax && Input.GetButtonDown("Jump"))
        {
			jumpCount++;	
			anim.SetTrigger("isJumping");
        }
		
		}
}
