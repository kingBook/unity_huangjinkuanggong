using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public string upKey="up";
	public string downKey="down";
	public GameObject hook_prefab;
	private Animator _animator;
    private Hook _hook;
	void Start () {
		_animator=GetComponent<Animator>();
		//initialize hook
		GameObject hookObj=Instantiate(hook_prefab) as GameObject;
		SimplePendulum simpP=hookObj.GetComponent<SimplePendulum>();
		simpP.gravity = 1.5f;
		simpP.len = 0.2f;
		simpP.startAngle = 70;
		simpP.origin = transform.localPosition;
		simpP.origin += new Vector2(-0.24f,-0.32f);
        _hook=hookObj.GetComponent<Hook>();
	}
	
	// Update is called once per frame
	void Update () {
		bool isJpDown = Input.GetKeyDown (downKey);
		bool isJpUp = Input.GetKeyDown (upKey);
		if (isJpDown) {
            _hook.go();
		}else if(isJpUp){

        }

	}
}
