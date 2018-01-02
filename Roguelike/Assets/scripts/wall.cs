using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {
	public Sprite mDamageSprite;

	private SpriteRenderer mSpriteRender;
	private int hp = 3 ;
	public AudioClip chopSoun1;
	public AudioClip chopSoun2;

	void Awake()
	{
		mSpriteRender = GetComponent<SpriteRenderer>();
	}
	
	public void DamageWall(int loss) {
		mSpriteRender.sprite = mDamageSprite;

		hp -= loss;
		SoundsManager.instance.RandomiSfx(chopSoun1 , chopSoun2);
		if (hp <= 0 ) {
			gameObject.SetActive(false);
		}
	}


}
