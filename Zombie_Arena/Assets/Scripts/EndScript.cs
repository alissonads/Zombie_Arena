using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {
    private bool collide;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            collide = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            collide = false;
        }
    }

    public bool GetCollide
    {
        get
        {
            return collide;
        }
    }

}
