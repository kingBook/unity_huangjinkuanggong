using UnityEngine;
using System.Collections;
/**单摆*/
public class SimplePendulum : MonoBehaviour {

	public Vector2 origin;
	public float gravity=9.81f;
	public float len=1;//摆长
	public float startAngle=90;
	private float delta=0.016f;
	private float currentW;
	private float currentAngle;
    private Vector2 _pos;
    private bool _pause;
    void Awake(){
        currentAngle=startAngle*Mathf.PI/180;
        currentW = 0;
        float x=origin.x+Mathf.Sin (currentAngle)*len;
        float y=origin.y+Mathf.Cos (currentAngle)*len;
        _pos = new Vector2 (x,y);
    }

    void FixedUpdate(){
        if(_pause)return;
        float k1,k2,k3,k4;
        float l1,l2,l3,l4;
        {
            k1=currentW;
            l1=-(gravity/len)*Mathf.Sin(currentAngle);
            
            k2=currentW+delta*l1/2;
            l2=-(gravity/len)*Mathf.Sin(currentAngle+delta*k1/2);
            
            k3=currentW+delta*l2/2;
            l3=-(gravity/len)*Mathf.Sin(currentAngle+delta*k2/2);
            
            k4=currentW+delta*l3;
            l4=-(gravity/len)*Mathf.Sin(currentAngle*delta*k3);
            
            currentAngle+=delta*(k1+2*k2+2*k3+k4)/(6/*2*Math.PI*/);
            
            currentW+=delta*(l1+2*l2+2*l3+l4)/(6/*2*Math.PI*/);
            
        }
        //
        _pos.x=origin.x+Mathf.Sin(currentAngle)*len;
        _pos.y=origin.y-Mathf.Cos(currentAngle)*len;
    }

    public void setPause(bool value){
        _pause = value;
    }

    /**返回单摆当前欧拉角度*/
    public float eulerAngle{
        get{return currentAngle * 180/Mathf.PI-90;}
    }

    public Vector2 position{get{ return _pos; }}
}
