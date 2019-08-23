using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SingletonManager : MonoBehaviour
{

    static SingletonManager _instance;

    private int score = 0;
    private int life = 100;
    private int ammunition = 14;

    public int enemies;
    public static SingletonManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject auxObj = Object.FindObjectOfType(typeof(SingletonManager)) as GameObject;

                if (_instance != null)
                {
                    _instance = auxObj.GetComponent<SingletonManager>();
                }
                else
                {
                    GameObject go = new GameObject("SingletonManager");

                    DontDestroyOnLoad(go);

                    go.AddComponent<SingletonManager>();
                    _instance = go.GetComponent<SingletonManager>();
                }
            }
            return _instance;
        }
    }

    public int GetLife
    {
        set
        {
            life = value;
        }
        get
        {
            return life;
        }
    }

    public int GetScore
    {
        set
        {
            score = value;
        }
        get
        {
            return score;
        }
    }
        
    public int GetAmmunition
    {
        set
        {
            ammunition = value;
        }
        get
        {
            return ammunition;
        }
    }

    public void Reset()
    {
        score = 0;
        life = 100;
        ammunition = 14;
    }
}
    //public void tirarVida()
    //{
    //    quantos_inimigos--;
    //}

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //        GoToMenu();
    //}

    //public void NextLevel()
    //{
    //    if (quantos_inimigos <= 0)
    //    {
    //        if (SceneManager.GetActiveScene().name == "Level3")
    //            GoToMenu();
    //        else
    //            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    }
    //}

    //public void GoToLevel(int number)
    //{
    //    SceneManager.LoadScene("Level" + number);
    //}


    //public void GoToMenu()
    //{
    //    SceneManager.LoadScene("Menu");
    //}

