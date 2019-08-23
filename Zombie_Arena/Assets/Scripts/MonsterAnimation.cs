using UnityEngine;
using System.Collections;

public class MonsterAnimation : MonoBehaviour {

	void DanoPlayer()
    {
        GetComponentInParent<MonsterScript>().Dano();
    }
}
