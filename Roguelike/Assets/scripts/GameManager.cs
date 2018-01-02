using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null ; 
	private BroadManager mBroadManager;
	private int curLevel = 3 ; 
	// Use this for initialization
	[HideInInspector] private int mPlayerFoodNum = 100 ;
	[HideInInspector] private bool mIsPlayerTurn = true ; 
	private List<Enemy> mEnemyList = new List<Enemy>();

	public static GameManager getInstance() {
		if (!GameManager.instance) {
			Instantiate(GameManager.instance);
		}
		return GameManager.instance;
	}

	public void Init() {

	}

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

	public void GameOver() {

	}

	public int GetPlayerFoodNum() { return this.mPlayerFoodNum; }
	public void SetPlayerFoodNum(int v ) { this.mPlayerFoodNum = v ; }

	public bool IsPlayerTurn() { return this.mIsPlayerTurn; }
	public void SetIsPlayerTurn(bool v ) { this.mIsPlayerTurn = v ; }

	public void AddEnemyToList(Enemy e ) {
		this.mEnemyList.Add(e);
	}
}
