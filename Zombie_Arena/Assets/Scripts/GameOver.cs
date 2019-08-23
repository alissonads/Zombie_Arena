using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
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
