using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public void Play()
    {
        Application.LoadLevel("Menu_scene");
    }

    public void Scene(string scene)
    {
        SingletonManager.instance.Reset();
        Application.LoadLevel(scene);
    }
}
