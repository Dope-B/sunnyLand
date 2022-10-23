using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleTon : MonoBehaviour {

    public static singleTon instance;
    #region SingleTon
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion SingleTon
}
