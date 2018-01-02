using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loader : MonoBehaviour {
	void Awake()
	{
		GameManager.getInstance().Init();
	}
}
