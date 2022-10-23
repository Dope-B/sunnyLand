using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class houseTransfer : MonoBehaviour {

    public string dest;
    public string depart;
    public int destSound;
    private player_movement player;
    private BGM_Manager BGM;
    private AudioManager Audio;
    private cam_manager cam;
    private fadeManager FM;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam_manager>();
        FM = FindObjectOfType<fadeManager>();
        BGM = FindObjectOfType<BGM_Manager>();
        Audio = FindObjectOfType<AudioManager>();

        if (player.CurrentMap == depart && player.PreviousMap == dest)
        {
            cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cam.transform.position.z);
            player.transform.position = this.transform.position;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            Audio.play("door");
            StartCoroutine(TransferC());
        }
    }
    IEnumerator TransferC()
    {
        Time.timeScale = 0f;
        BGM.fadeout();
        FM.FadeOut();
        yield return new WaitUntil(()=>FM.fadeDone&&BGM.isDone);
        BGM.play(destSound);
        BGM.fadein();
        player.CurrentMap = dest;
        player.PreviousMap = depart;
        SceneManager.LoadScene(dest);
        Time.timeScale = 1f;
        FM.FadeIn();
    }
}
