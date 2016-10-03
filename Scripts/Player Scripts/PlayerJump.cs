using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerJump : MonoBehaviour {

	[SerializeField]
	private AudioClip jumpClip;
	private float jumpForce = 12f, forwardForce = 0f;

	private Rigidbody2D myBody;

	private bool canJump;

	private Button jumpButton;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		jumpButton = GameObject.Find ("Jump Button").GetComponent<Button>();
		jumpButton.onClick.AddListener (() => Jump ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (myBody.velocity.y) == 0) {
			canJump = true;
		}
	}

	public void Jump()
	{
		//Don't jump twice.
		if (canJump) {
			canJump = false;
			//AudioSource.PlayClipAtPoint (jumpClip, transform.position);
			if (transform.position.x < 0) {
				forwardForce = 0.5f;
				//He isn't too far forward.
			} else {
				forwardForce = 0f;
			}
			//Add these to player's velocity.
			myBody.velocity = new Vector2 (forwardForce, jumpForce);
		}
		//If he is pushed left, we need to have him able to go forward.

	}

}
