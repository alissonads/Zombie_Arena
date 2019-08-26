using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Syrinj;

public class GameOver : MonoBehaviour {
    [FindAttribute("num")]
    public Text score;

	public void ReturnMenu()
    {
        Application.LoadLevel("Menu");
    }

    void Update()
    {
        score.text = SingletonManager.instance.GetScore.ToString();
    }
}
