using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
	
	[SerializeField]
	private GameObject _title;

	void Start () {
		//_title.SetActive(true);
	}
	
	void Update () {
		
	}

	public void onStartGame(){
		_title.SetActive(false);

	}
}
