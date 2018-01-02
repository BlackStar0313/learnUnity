using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f ; 
	public float turnDelay = 0.1f;
	public static GameManager instance = null ; 
	private BroadManager mBroadManager;
	private int curLevel = 1 ; 
	// Use this for initialization
	[HideInInspector] private int mPlayerFoodNum = 100 ;
	[HideInInspector] private bool mIsPlayerTurn = true ; 
	[HideInInspector] private bool mIsEnemyTurn = false ; 
	private List<Enemy> mEnemyList = new List<Enemy>();
	private Text levelText;
	private GameObject levelImg;
	private bool doingSetup;

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

	void OnLevelWasLoaded(int index) {
		curLevel++;
		InitGame();
	}

	private void InitGame() {
		doingSetup = true ; 
		levelImg = GameObject.Find("LevelImage");
		levelText = GameObject.Find("levelText").GetComponent<Text>();
		levelText.text = "Day " + curLevel ; 
		levelImg.SetActive(true);
		Invoke("HideLevelImg" , levelStartDelay);

		this.mEnemyList.Clear();
		this.mBroadManager.SetupSecene(this.curLevel);
	}

	public void HideLevelImg() {
		levelImg.SetActive(false);
		doingSetup = false ; 
	}
	
	// Update is called once per frame
	void Update () {
		if ( mIsPlayerTurn || mIsEnemyTurn || doingSetup ) 
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
		levelText.text = "After " + curLevel + "days, you starved ";
		levelImg.SetActive(true);
		enabled = false ; 
	}

	public int GetPlayerFoodNum() { return this.mPlayerFoodNum; }
	public void SetPlayerFoodNum(int v ) { this.mPlayerFoodNum = v ; }

	public bool IsPlayerTurn() { return this.mIsPlayerTurn; }
	public void SetIsPlayerTurn(bool v ) { this.mIsPlayerTurn = v ; }

	public void AddEnemyToList(Enemy e ) {
		this.mEnemyList.Add(e);
	}
}
