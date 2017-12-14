using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loader : MonoBehaviour {

	public GameManager mGameManager = null ;
	

	void Awake()
	{
		if (GameManager.instance == null) {
			Instantiate(mGameManager);
		}
	}
}
