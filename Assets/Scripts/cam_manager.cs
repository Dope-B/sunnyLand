using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_manager : MonoBehaviour {

    static public cam_manager cam;
    public GameObject target;
    public float speed;
    private Vector3 targetPosition;
    public BoxCollider2D bound;
    private Vector3 minBound;
    private Vector3 maxBound;
    private float halfWidth;
    private float halfHeight;
    private Camera TheCamera;



   private  void Awake() {
        if (cam == null)
        {
            cam = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this.gameObject); }
    }
	// Use this for initialization
	void Start () {
        TheCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = TheCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y+0.3f, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, speed * Time.deltaTime);
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            if (bound.bounds.size.x- halfWidth*2 < 0)
            {
                clampedX = (minBound.x + maxBound.x)/2;
            }
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
            if (bound.bounds.size.y - halfHeight*2 < 0)
            {
                clampedY = (minBound.y  + maxBound.y)/2;
            }
            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
	}
    public void SetBound(BoxCollider2D box) {
        bound = box;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
