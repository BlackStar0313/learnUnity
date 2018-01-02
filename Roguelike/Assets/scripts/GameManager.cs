using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f ; 
	public float turnDelay = 0.1f;
	public static GameManager instance = null ; 
	private BroadManager mBroadManager;
	private int curLevel = 3 ; 
	// Use this for initialization
	[HideInInspector] private int mPlayerFoodNum = 100 ;
	[HideInInspector] private bool mIsPlayerTurn = true ; 
	[HideInInspector] private bool mIsEnemyTurn = false ; 
	private List<Enemy> mEnemyList = new List<Enemy>();

	public static GameManager getInstance() {
		return GameManager.instance;
	}

	public void Init() {

	}

	void Awake()
	{
		if (instance == null) {
			instance = this ;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		this.mBroadManager = GetComponent<BroadManager>();
		this.InitGame();
	}

	private void InitGame() {
		this.mEnemyList.Clear();
		this.mBroadManager.SetupSecene(this.curLevel);
	}
	
	// Update is called once per frame
	void Update () {
		if ( mIsPlayerTurn || mIsEnemyTurn ) 
			return ;
		
		StartCoroutine(MoveEnemies());
	}

	IEnumerator MoveEnemies() {
		mIsEnemyTurn = true ; 
		yield return new WaitForSeconds(turnDelay);

		for (int i = 0 ; i < mEnemyList.Count ; ++i) {
			mEnemyList[i].moveEnemy();

			yield return new WaitForSeconds(mEnemyList[i].mMoveingTime);
		}

		mIsPlayerTurn = true ; 
		mIsEnemyTurn = false ; 
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
