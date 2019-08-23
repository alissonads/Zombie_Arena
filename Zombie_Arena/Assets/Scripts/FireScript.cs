using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
