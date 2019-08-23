using UnityEngine;
using System.Collections;

public class FinishAnimPlayer : MonoBehaviour {

	void Finish()
    {
        GetComponentInParent<PlayerScript>().FinishAnimation();
    }
}
