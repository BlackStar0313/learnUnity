using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BroadManager : MonoBehaviour {


	[Serializable]
	public class Count {
		public int maximum;
		public int minimum;

		public Count(int maximum , int minimum ) {
			this.maximum = maximum ; 
			this.minimum = minimum ; 
		}
	}

	public int col = 8 ; 
	public int row = 8 ; 

	public Count mRockCount = new Count(5 ,9);
	public Count mFoodCount = new Count(1,5);

	public GameObject exist ;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] rockTiles; 
	public GameObject[] enemyTiles;


	private Transform boardHolder ; 
	private List<Vector3> gridPosList = new List<Vector3>();


	private void InitailizePos() {
		gridPosList.Clear();
		for (int x = 1 ; x < this.row ; ++x ) {
			for (int y = 1 ; y < this.col ; ++y) {
				Vector3 pos = new Vector3(x, y , 0f );
				this.gridPosList.Add(pos);
			}
		}
	}

	private void boardSetUp() {
		this.boardHolder = new GameObject("Board").transform ;
		
		for (int x = -1 ; x <= row ; ++x) {
			for (int y = -1 ; y <= col; ++y) {
				GameObject newGrid = this.floorTiles[Random.Range(0, this.floorTiles.Length)];
				if (x == -1 || x == row || y == -1 || y == col) {
					newGrid = this.wallTiles[Random.Range(0, this.wallTiles.Length)];
				}

				GameObject instance = Instantiate(newGrid , new Vector3(x, y , 0f) , Quaternion.identity) as GameObject;
				instance.transform.SetParent(this.boardHolder);
			}
		}
	}

	private Vector3 RandomPos() {
		int randIdx = Random.Range( 0, this.gridPosList.Count);
		Vector3 pos = this.gridPosList[randIdx];
		this.gridPosList.RemoveAt(randIdx);
		return pos ; 
	}

	private void LayoutObjRandom(GameObject[] objArray, int min , int max ) {
		int objCount = Random.Range(min , max);

		for (int i = 0 ; i < objCount ; ++i) {
			Vector3 pos = this.RandomPos();
			GameObject obj = objArray[Random.Range(0 , objArray.Length)];
			Instantiate(obj , pos , Quaternion.identity);
		}
	}


	public void  SetupSecene(int level) {
		this.boardSetUp();
		this.InitailizePos();

		this.LayoutObjRandom(foodTiles , mFoodCount.minimum , mFoodCount.maximum);
		this.LayoutObjRandom(rockTiles , mRockCount.minimum , mRockCount.maximum);

		int enemyCount = (int)Mathf.Log(level, 2f );
		this.LayoutObjRandom(enemyTiles, enemyCount , enemyCount);

		Instantiate(exist , new Vector3(row-1,col-1, 0f), Quaternion.identity);
	}
}
