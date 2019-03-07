using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour {
    private const uint e_isGo   = 0x0001;
    private const uint e_isBack = 0x0002;
    private uint _flags;
    private float _goSpeed=0.05f;
    private float _backSpeed=0.1f;
    private float _goMaxDistance=5;
    private Vector3 _goPos;
    private SimplePendulum _simplePendulum;
    private LineRenderer _lineRenderer;
    private GameObject _gou1;
    private GameObject _gou2;
	void Start () {
		transform.localScale = new Vector2 (0.5f,0.5f);
        _simplePendulum=GetComponent<SimplePendulum>();

        _lineRenderer=GetComponent<LineRenderer>();
        _gou1 = GameObject.Find(gameObject.name+"/gou1");
        _gou2 = GameObject.Find(gameObject.name+"/gou2");
        _gou2.SetActive(false);
	}

	void Update () {
        drawLine();
        if((_flags&e_isGo)>0){
            Vector3 pos=transform.localPosition;
            pos.x+=Mathf.Cos(transform.localRotation.eulerAngles.z*Mathf.PI/180)*_goSpeed;
            pos.y+=Mathf.Sin(transform.localRotation.eulerAngles.z*Mathf.PI/180)*_goSpeed;
            var distance=Vector2.Distance(pos,_goPos);
            if(distance>=_goMaxDistance){
                pos.x=_goPos.x+Mathf.Cos(transform.localRotation.eulerAngles.z*Mathf.PI/180)*_goMaxDistance;
                pos.y=_goPos.y+Mathf.Sin(transform.localRotation.eulerAngles.z*Mathf.PI/180)*_goMaxDistance;
                back();
            }
            transform.localPosition=pos;
        }else if((_flags&e_isBack)>0){
            Vector3 pos=transform.localPosition;
            pos.x+=Mathf.Cos(transform.localRotation.eulerAngles.z*Mathf.PI/180)*-_backSpeed;
            pos.y+=Mathf.Sin(transform.localRotation.eulerAngles.z*Mathf.PI/180)*-_backSpeed;
            var distance=Vector2.Distance(pos,_goPos);
            if(distance<_backSpeed){
                _flags &= ~e_isBack;
                pos=_goPos;
                onBackToOrigin();
            }
            transform.localPosition=pos;
        }else{
            transform.localPosition = _simplePendulum.position;
            transform.localRotation = Quaternion.Euler(0,0,_simplePendulum.eulerAngle);
        }
	}

    private void drawLine(){
        _lineRenderer.SetPosition(0,new Vector3(_simplePendulum.origin.x,_simplePendulum.origin.y,-1));
        _lineRenderer.SetPosition(1,new Vector3(transform.localPosition.x,transform.localPosition.y,-1)); 

    }

    public void go(){
        if((_flags&e_isGo)>0)return;
        _flags |= e_isGo;
        _flags &= ~e_isBack;

        _goPos = transform.localPosition;
        _simplePendulum.setPause(true);
        _gou1.SetActive(true);
    }

    private void back(){
        if((_flags&e_isBack)>0)return;
        _flags |= e_isBack;
        _flags &= ~e_isGo;

        _gou1.SetActive(false);
        _gou2.SetActive(true);
    }

    private void onBackToOrigin(){
        _simplePendulum.setPause(false);
        _gou1.SetActive(true);
        _gou2.SetActive(false);
    }
}
