using UnityEngine;
using System.Collections;

public class Walking : MonoBehaviour {
	public float movementSpeed = 1f;
	public float rotationSpeed = 1f;
	private GameObject camera;
	private Animator animator;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		GameObject player = GameObject.FindGameObjectWithTag("player");
		animator = player.transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private bool getStandingState(){
		return true;
	}

	void FixedUpdate()
	{
		float moveVertical = Input.GetAxis("Vertical");
		float moveHorizontal = Input.GetAxis("Horizontal");
		if (!camera.GetComponent<Standup>().seatedState())
		{
			Move(moveHorizontal, moveVertical);
		}
		
		
	}
	
	private void Move(float horizontal, float vertical)
	{
			if (horizontal != 0 || vertical != 0)
			{
				Vector3 newPosition = transform.forward.normalized * vertical * movementSpeed * Time.deltaTime;
				newPosition += transform.right.normalized * horizontal * movementSpeed * Time.deltaTime;
				GetComponent<Rigidbody>().MovePosition(transform.position + newPosition);
				changeAnimationToWalking(vertical);
			}
			else
			{
				//transform.position = transform.localPosition;
				changeAnimationToIdle();
			}
		
	}

	private void changeAnimationToWalking(float vertical){
		Debug.Log (vertical);
		animator.SetFloat ("Speed", vertical);
	}

	private void changeAnimationToIdle(){
		animator.SetFloat ("Speed",0);
	}
}
