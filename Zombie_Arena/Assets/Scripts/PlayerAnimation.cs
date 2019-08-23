using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	public void SoundReload()
    {
        GetComponentInParent<PlayerScript>().SoundReload();
    }
}
