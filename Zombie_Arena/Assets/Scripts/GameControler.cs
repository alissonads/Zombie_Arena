using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Syrinj;

public class GameControler : MonoBehaviour {
    public Text []texts;
    public int enemies;

    [FindAttribute("End")]
    private GameObject end;
    [FindAttribute("Slider")]
    private Slider life;

    // Update is called once per frame
    void Start()
    {
        SingletonManager.instance.enemies = enemies;
    }

    void Update () {
        texts[0].text = SingletonManager.instance.GetScore.ToString();
        texts[1].text = SingletonManager.instance.GetLife.ToString() + "%";
        texts[2].text = SingletonManager.instance.GetAmmunition.ToString();
        life.value = SingletonManager.instance.GetLife;

        if (SingletonManager.instance.GetLife <= 0)
        {
            Application.LoadLevel("GameOver");
        }
        if (SingletonManager.instance.enemies <= 0)
        {
            if (end.GetComponent<EndScript>().GetCollide)
            {
                if (end.tag == "End1")
                {
                    Application.LoadLevel("fase2");
                    return;
                }
                if (end.tag == "End2")
                {
                    Application.LoadLevel("fase3");
                    return;
                }
                if (end.tag == "End3")
                {
                    Application.LoadLevel("GameOver");
                    return;
                }
            }
        }
	}
}
