using UnityEngine;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
public class HandManager : MonoBehaviour {
	
	[SerializeField] private BodySourceView _view;
	private GameObject _handObj_R;
	private GameObject _handObj_L;

	public Transform hand_R{ get; private set; }
	public Transform hand_L{ get; private set; }

	void Start (){
		_handObj_R = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		_handObj_L = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		_handObj_R.GetComponent<BoxCollider>().enabled = false;
		_handObj_L.GetComponent<BoxCollider>().enabled = false;
		_handObj_R.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		_handObj_L.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
	}
	
	void Update () {
		//TODO: ここにキー入力時の移動処理を書く

		foreach(var pair in _view.Bodies){
			//pair.value は bodyObj
			hand_R = pair.Value.transform.FindChild(Kinect.JointType.HandRight.ToString());
			_handObj_R.transform.position = hand_R.position;
			_handObj_R.transform.rotation = hand_R.rotation;

			hand_L = pair.Value.transform.FindChild(Kinect.JointType.HandRight.ToString());
			_handObj_L.transform.position = hand_L.position;
			_handObj_L.transform.rotation = hand_L.rotation;
		}
	}
}
