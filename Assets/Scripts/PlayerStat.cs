using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour {

    public static PlayerStat instance;
    private player_movement player;
    private AudioManager Audio;
    private BGM_Manager bgm;
    private fadeManager FM;
    public GameObject floating_text;
    public GameObject canvas;
    public Slider hpSlider;
    public Slider mpSlider;
    public int hp;
    public int current_hp;
    public int mp;
    public int current_mp;
    public int level;
    public int[] needExp;
    public int current_exp;
    public int armor;
    public int atkDamage;

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
    // Use this for initialization
    void Start () {
        current_hp = hp;
        current_mp = mp;
        player = FindObjectOfType<player_movement>();
        Audio = FindObjectOfType<AudioManager>();
        bgm = FindObjectOfType<BGM_Manager>();
        FM = FindObjectOfType<fadeManager>();
    }
	
	// Update is called once per frame
	void Update () {
        hpSlider.maxValue = hp;
        mpSlider.maxValue = mp;
        hpSlider.value = current_hp;
        mpSlider.value = current_mp;
        isDead();
	}
    private void isDead()
    {
        if (!player.GSanimator.GetBool("isDead")&& current_hp <= 0)
        {
            Audio.play("die");
            StartCoroutine(respawn());
        }
    }
    public void Hit(int damage)
    {
        if (!player_movement.player.isInc)
        {
            current_hp -= (damage / armor) * 10;
            Vector3 vector = this.transform.position;
            vector.y += 0.1f;
            GameObject clone = Instantiate(floating_text, vector, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<Floating_Text>().text.text = "-" + ((damage / armor) * 10);
            clone.GetComponent<Floating_Text>().text.color = Color.red;
            clone.GetComponent<Floating_Text>().text.fontSize = 18;
            clone.transform.SetParent(canvas.transform);
            StopAllCoroutines();
            StartCoroutine(HitEffect());
        }
       
    }
    IEnumerator respawn()
    {
        player.GSanimator.SetBool("isDead", true);
        yield return new WaitForSeconds(0.5f);
        player.OutofControl = true;
        yield return new WaitForSeconds(0.5f);
        bgm.fadeout();
        FM.FadeOut();
        yield return new WaitUntil(() => FM.fadeDone);
        SceneManager.LoadScene("InHouse");
        bgm.play(2);
        bgm.fadein();
        yield return new WaitForSeconds(1f);
        current_hp = hp;
        current_mp = mp;
        player.GSanimator.SetBool("isDead", false);
        player.OutofControl = false;
        player.transform.position = new Vector3(0, 0, 0); 
        player.CurrentMap = "InHouse";
        FM.FadeIn();
    }
    IEnumerator HitEffect()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0.3f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0.3f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 0.3f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
        color.a = 1f;
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.1f);
    }
}
