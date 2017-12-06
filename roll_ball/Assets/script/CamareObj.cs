using UnityEngine;
using System.Collections;

public class CamareObj : MonoBehaviour {
	public GameObject mPlayer ; 
	private Vector3 mOffset; 

	// Use this for initialization
	void Start () {
		this.mOffset = this.transform.position - mPlayer.transform.position; 
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = this.mOffset + mPlayer.transform.position; 
	}
}
