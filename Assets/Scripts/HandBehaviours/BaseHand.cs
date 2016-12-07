using UnityEngine;

//TODO: 左手の実装
public class BaseHand : MonoBehaviour {
	
	public bool isHit{ get; protected set; }
	protected Rigidbody _rigidbody;
	protected Transform _virtualHand_R;
	protected SerialHandler _serialHandler;
	protected virtual void Start (){
		if(!_serialHandler){
			_serialHandler = GetComponent<SerialHandler>();
		}
		_rigidbody = GetComponent<Rigidbody>();
		_serialHandler.OnDataReceived += OnDataReceived;
		var hitHandler = _virtualHand_R.gameObject.GetComponent<HitEventHandler>();
		hitHandler.OnHitEnter += OnHandEnter;
	}
	
	protected virtual void Update (){

	}
	protected virtual void FixedUpdate (){
		
	}
	protected virtual void OnDataReceived (string message){
		try{
			//Debug.log(int.Parse(message));
		} catch (System.Exception e) {
			//Debug.LogWarning(e.Message);
		}
	}

	protected virtual void OnHandEnter(Collider col){
		isHit = true;
	}

	protected virtual void OnHandExit(Collider col){
		isHit = false;
	}

	protected virtual void OnHandStay(Collider col){
		//
	}

	void OnApplicationQuit (){
		_serialHandler.Write(128);
	}
}
