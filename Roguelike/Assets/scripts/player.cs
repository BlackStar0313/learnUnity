﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : moveingObj {
	public float mRestartLevelDelay = 1f ; 
	public int mPointsPreFood = 10 ; 
	public int mPointsPreSoda = 20 ; 
	public int mWallDamage = 1 ; 

	private Animator mAnimator ; 
	private int mFoodsNum ; 

	protected override void Start () {
		mAnimator = GetComponent<Animator>();
		mFoodsNum = GameManager.getInstance().GetPlayerFoodNum();
		base.Start();
	}

	private void OnDisable()
	{
		GameManager.getInstance().SetPlayerFoodNum(this.mFoodsNum);
	}
	
	// Update is called once per frame
	private void Update () {
		if (!GameManager.getInstance().IsPlayerTurn()) return ;

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)(Input.GetAxisRaw("Horizontal"));
		vertical = (int)(Input.GetAxisRaw("Vertical"));

		if (horizontal != 0 ) 
			vertical = 0 ;

		if (horizontal != 0 || vertical != 0) {
			AttemptMove<wall> (horizontal , vertical);
		}
	}

	protected override void OnCanMove <T> (T component ) {
		wall hitwall = component as wall ; 
		hitwall.DamageWall(mWallDamage);

		mAnimator.SetTrigger("player_hit");
	}

	protected override void AttemptMove <T> (int xDir , int yDir)  {
		mFoodsNum--;
		base.AttemptMove <T> (xDir , yDir);

		RaycastHit2D hit ; 
		if (Move(xDir, yDir,out hit)) {

		} 

		checkIfGameOver();
		GameManager.getInstance().SetIsPlayerTurn(false);
	}

	private void checkIfGameOver() {
		if (mFoodsNum <= 0) {
			GameManager.getInstance().GameOver();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag)
		{
			case "Exit": 
			{
				Invoke("Restart" , mRestartLevelDelay);
				this.enabled = false ; 
				break;
			}

			case "Food":
			{
				mFoodsNum += mPointsPreFood;
				other.gameObject.SetActive(false);
				break;
			}

			case "Soda":
			{
				mFoodsNum += mPointsPreSoda;
				other.gameObject.SetActive(false);
				break;
			}
			default:
				break;
		}
	}

	private void Restart() {
		SceneManager.LoadScene(0);
	}

	public void LoseFood(int loss) {
		mAnimator.SetTrigger("player_demaged");
		mFoodsNum -= loss; 
		checkIfGameOver();
	}
}
