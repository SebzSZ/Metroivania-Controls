using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")] 
	
	// Float Variables
	[SerializeField] private float moveSpeed;
	[SerializeField] [Range(0f, 0.3f)] private float smoothMove;
	private float horizontalMove;
	
	// Vector Variables
	private Vector2 input;
	private Vector2 finalInput;
	private Vector2 velocity = Vector2.zero;
	private Vector3 scale;
	
	// Flip Controller
	private bool flipped = false;
	
	[Header("Components")]
	
	private Rigidbody2D rb2D;
	private Animator animator;
	private PlayerInput playerInput;
	
	private void Start()
	{
		rb2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		playerInput = GetComponent<PlayerInput>();
	}
	
	private void Update()
	{
		ReadInput();
		Flip();
		
		animator.SetFloat("xMove", Mathf.Abs(horizontalMove));
	}
	
	private void FixedUpdate()
	{
		Move(horizontalMove);
	}
	
	private void ReadInput()
	{
		input = playerInput.actions["Move"].ReadValue<Vector2>().normalized;
		horizontalMove = input.x * moveSpeed;
	}
	
	private void Move(float move)
	{
		finalInput = new Vector2(move, rb2D.velocity.y);
		rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, finalInput, ref velocity, smoothMove);
	}
	
	private void Flip()
	{
		if ((flipped && horizontalMove > 0f) || (!flipped && horizontalMove < 0f))
		{
			flipped = !flipped;
			scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
		
	}
}
