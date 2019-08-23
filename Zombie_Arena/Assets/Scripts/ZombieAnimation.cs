using UnityEngine;
using System.Collections;

public class ZombieAnimation : MonoBehaviour {

    // Use this for initialization
    void DanoPlayer()
    {
        GetComponentInParent<ZombieScript>().Dano();
    }
}
