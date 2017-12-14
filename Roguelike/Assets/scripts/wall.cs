using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {
	public Sprite mDamageSprite;

	private SpriteRenderer mSpriteRender;
	private int hp = 3 ;

	void Awake()
	{
		mSpriteRender = GetComponent<SpriteRenderer>();
	}
	
	void DamageWall(int loss) {
		mSpriteRender.sprite = mDamageSprite;

		hp -= loss;
		if (hp <= 0 ) {
			gameObject.SetActive(false);
		}
	}


}
