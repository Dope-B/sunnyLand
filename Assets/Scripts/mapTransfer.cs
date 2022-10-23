using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapTransfer : MonoBehaviour {

    public string dest;
    public string depart;
    public int destSound;
    public int departSound;
    private player_movement player;
    private BGM_Manager BGM;
    private cam_manager cam;
    private fadeManager FM;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam_manager>();
        FM = FindObjectOfType<fadeManager>();
        BGM = FindObjectOfType<BGM_Manager>();

        if (player.CurrentMap == depart&&player.PreviousMap==dest)
        {
            cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cam.transform.position.z);
            player.transform.position = this.transform.position;
            player.GSrigid.velocity = new Vector3(0, 0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            StartCoroutine(TransferC());
        }
    }
    IEnumerator TransferC()
    {
        Time.timeScale = 0f;
        FM.FadeOut();
        BGM.fadeout();
        yield return new WaitUntil(() => FM.fadeDone && BGM.isDone);
        if (departSound == destSound) {
            BGM.fadein();
        }
        else{
            BGM.stop();
            BGM.play(destSound);
            BGM.fadein();
        }
        player.CurrentMap = dest;
        player.PreviousMap = depart;
        SceneManager.LoadScene(dest);
        FM.FadeIn();
        Time.timeScale = 1f;
    }
}
