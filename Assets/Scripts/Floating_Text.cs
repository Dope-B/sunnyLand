using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floating_Text : MonoBehaviour {

    public float speed;
    public float destroyTime;
    public Text text;
    private Vector3 vector;

	void Update () {
        StartCoroutine(floating());
	}
    IEnumerator floating()
    {
        while (true)
        {
            vector.Set(text.transform.position.x, text.transform.position.y + (speed * Time.unscaledDeltaTime), text.transform.position.z);
            text.transform.position = vector;
            destroyTime -= Time.unscaledDeltaTime;
            if (destroyTime <= 0)
            {
                Destroy(this.gameObject);
                break;
            }
            yield return new WaitForSecondsRealtime(0.5f);
        } 
    }
}
