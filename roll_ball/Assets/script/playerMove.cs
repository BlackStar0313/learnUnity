using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
	public float mSpeed = 0 ;
	public GUIText mTextCoin; 
	public GUIText mTextWin; 
	private int mCoinNum ;
	private Rigidbody mRb ; 

	// Use this for initialization
	void Start () {
		this.mRb = GetComponent<Rigidbody> ();
		this.mCoinNum = 0 ; 
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 moveVec = new Vector3 (moveHorizontal , 0.0f , moveVertical);
		this.mRb.AddForce (moveVec * this.mSpeed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("pickUpCoin")) {
			other.gameObject.SetActive(false) ;
			this.updateTextCoin();
		}
	}

	void updateTextCoin() {
		++this.mCoinNum;
		this.mTextCoin.text = "coins: " + this.mCoinNum ; 
		if (this.mCoinNum >= 9 ) {
			this.mTextWin.gameObject.SetActive(true);
		} 
	}

	
}
