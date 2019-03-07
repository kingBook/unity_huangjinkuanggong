using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour {
	[SerializeField]
	private float _designPixelWidth=800;
	[SerializeField]
	private float _designPixelHeight=600;

	public enum CameraMatchMode {
		MatchWidth,
		MatchHeight
	}
	[SerializeField]
	private CameraMatchMode _matchMode=CameraMatchMode.MatchHeight;
	
	private Vector2 _recordScreenSize;
	
	private void Start () {
		_recordScreenSize=new Vector2(Screen.width,Screen.height);
		fit();
	}
	
	private void Update () {
		if(Screen.width!=_recordScreenSize.x||Screen.height!=_recordScreenSize.y){
			_recordScreenSize.Set(Screen.width,Screen.height);
			fit();
		}
	}

	private void fit(){
		float designCameraWidth=_designPixelWidth*0.01f;
		float designCameraHeight=_designPixelHeight*0.01f;
		float ratio=(float)Screen.width/(float)Screen.height;
	
		if(_matchMode==CameraMatchMode.MatchWidth){
			//ratio=w/h
			//ratio=designW/h
			float cameraHeight=designCameraWidth/ratio;
			Camera.main.orthographicSize=cameraHeight*0.5f;
		}else if(_matchMode==CameraMatchMode.MatchHeight){
			Camera.main.orthographicSize=designCameraHeight*0.5f;
		}
	}
}
