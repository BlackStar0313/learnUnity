using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class moveingObj : MonoBehaviour {

	public float mMoveingTime = .1f ; 
	private BoxCollider2D mBoxCollider ; 
	private Rigidbody2D mRb2d ; 
	private float inverseMovingTime ; 
	public LayerMask blockingLayer; 


	// Use this for initialization
	protected virtual void Start () {
		mBoxCollider = GetComponent<BoxCollider2D>() ;
		mRb2d = GetComponent<Rigidbody2D>() ; 

		inverseMovingTime = 1f/mMoveingTime ; 
	}

	protected bool Move(int xDir, int yDir ,out RaycastHit2D hit) {
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2(xDir , yDir);

		mBoxCollider.enabled = false ;
		hit = Physics2D.Linecast(start , end , blockingLayer);
		mBoxCollider.enabled = true ;

		if (hit.transform == null) {
			StartCoroutine(SmoothMovement(end));
			return true ;
		}
		return false ; 
	}

	protected IEnumerator SmoothMovement(Vector3 end) {
		float dist = (transform.position - end).sqrMagnitude;
		while (dist > float.Epsilon) {
			Vector3 newPos = Vector3.MoveTowards(mRb2d.position, end , inverseMovingTime * Time.deltaTime);
			mRb2d.MovePosition(newPos);
			dist = (transform.position - end).sqrMagnitude;
			yield return null ; 
		}
	}

	protected virtual void AttemptMove <T> (int xDir , int yDir) 
		where T : Component
	{
		RaycastHit2D hit ; 
		bool canMove = this.Move(xDir , yDir ,out hit );

		if (hit.transform == null) 
			return ;

		T hitComponent = hit.transform.GetComponent<T>();

		if (!canMove && hitComponent != null) {
			OnCanMove(hitComponent);
		}
	}


	protected abstract void OnCanMove <T> (T component ) where  T: Component ;
}
