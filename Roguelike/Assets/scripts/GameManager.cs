using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager instance = null ; 
	private BroadManager mBroadManager;
	private int curLevel = 3 ; 
	// Use this for initialization
	void Awake()
	{
		this.mBroadManager = GetComponent<BroadManager>();
		this.InitGame();
	}

	private void InitGame() {
		this.mBroadManager.SetupSecene(this.curLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
