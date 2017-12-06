using UnityEngine;
using System.Collections;

public class coinObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3(15,30,45) * Time.deltaTime *3);
	}
}
