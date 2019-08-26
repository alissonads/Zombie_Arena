using UnityEngine;
using System.Collections;

public class FinishAnimPlayer : MonoBehaviour {

	void Finish()
    {
        var p = GetComponentInParent(typeof(PlayerScript)) as PlayerScript;
        p.FinishAnimation();
    }
}
