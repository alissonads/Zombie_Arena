using UnityEngine;
using System.Collections;
using Syrinj;

public class ZombieAnimation : MonoBehaviour {
    ZombieScript z;
    // Use this for initialization
    void DanoPlayer()
    {
        GetComponentInParent<ZombieScript>().Dano();
    }
}
