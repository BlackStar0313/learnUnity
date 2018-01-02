using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : moveingObj {
	public int mPlayerDamage; 

	private Animator mAnimator;
	private Transform mTarget ; 
	private bool mIsSkipMove = false ;
	public AudioClip hitSound1 ;
	public AudioClip hitSound2 ;

	// Use this for initialization
	protected override void Start () {
		GameManager.getInstance().AddEnemyToList(this);
		mAnimator = GetComponent<Animator>();
		mTarget = GameObject.FindGameObjectWithTag("Player").transform;

		base.Start();
	}

	protected override void AttemptMove <T> (int xDir , int yDir)  {
		if (mIsSkipMove) {
			mIsSkipMove = false ; 
			return;
		}

		base.AttemptMove <T> (xDir , yDir);
		mIsSkipMove = true;
	}

	public void moveEnemy() {
		int xDir = 0 ;
		int yDir = 0 ;

		if (Mathf.Abs(mTarget.position.x - this.transform.position.x ) < float.Epsilon) {
			yDir = mTarget.position.y  > this.transform.position.y ? 1 : -1;
		}
		else {
			xDir = mTarget.position.x > this.transform.position.x ? 1 : -1;
		}
		AttemptMove <player> (xDir , yDir);
	}

	protected override void OnCanMove <T> (T component ) {
		player hitplayer = component as player ; 
		hitplayer.LoseFood(mPlayerDamage);
		this.mAnimator.SetTrigger("enemy_hit");
		SoundsManager.instance.RandomiSfx(hitSound1, hitSound2);
	}
}
