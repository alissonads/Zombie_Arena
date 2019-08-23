using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }
}
