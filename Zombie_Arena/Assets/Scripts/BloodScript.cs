using UnityEngine;
using System.Collections;

public class BloodScript : MonoBehaviour {
    private float time;
	// Use this for initialization
	void Start () {
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime * 2;

        if (time > 1)
            Destroy(gameObject);
	}
}
