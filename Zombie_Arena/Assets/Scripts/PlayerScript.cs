using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public float vel;
    public Transform spawPosition;
    public GameObject bullet;

    //public GameObject []bullets;

    public AudioClip []audios;
    public AudioSource audioS;

    private Animator anim;
    private int ammunition;
    private int comb;
    private int life;

    // Use this for initialization
    void Start () {
        anim = GetComponentInChildren<Animator>();
        ammunition = SingletonManager.instance.GetAmmunition;
        life = SingletonManager.instance.GetLife;
        comb = 7;
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal2");
        float y = Input.GetAxis("Vertical2");
        
        bool move = (x > 0 || x < 0 || y > 0 || y < 0) ? true : false;
        Rotate(x, y);

        if (move)
        {
            transform.Translate(Vector3.right * vel * Time.deltaTime);
            //GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
        Shoot();
    }

    private void Rotate(float x, float y)
    {
        if (x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (y > 0)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else if (y < 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (x > 0 && y > 0)
        {
            transform.eulerAngles = new Vector3(0, 315, 0);
        }
        else if (x < 0 && y > 0)
        {
            transform.eulerAngles = new Vector3(0, 225, 0);
        }
        else if (x < 0 && y < 0)
        {
            transform.eulerAngles = new Vector3(0, 135, 0);
        }
        else if (x > 0 && y < 0)
        {
            transform.eulerAngles = new Vector3(0, 45, 0);
        }
    }

    private void Shoot()
    {
        if(Input.GetButtonDown("FIRE"))
        {
            if (ammunition > 0)
            {
                GameObject obj = Instantiate(bullet, spawPosition.position, spawPosition.rotation) as GameObject;
                obj.GetComponent<Rigidbody>().AddForce(spawPosition.right * 1000);
                ammunition--;
                SingletonManager.instance.GetAmmunition = ammunition;
                anim.SetBool("shoot", true);
                audioS.PlayOneShot(audios[0], 1.0f);
            }
            else
                audioS.PlayOneShot(audios[1], 1.0f);
        }
        if(Input.GetKeyDown(KeyCode.Q) && ammunition > 0)
        {
            anim.SetBool("reload", true);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ammunition")
        {
            ammunition += 14;
            SingletonManager.instance.GetAmmunition = ammunition;
            audioS.PlayOneShot(audios[2], 0.4f);
            Destroy(col.gameObject);
        }
    }

    public void FinishAnimation()
    {
        anim.SetBool("shoot", false);
        anim.SetBool("move", false);
        anim.SetBool("reload", false);
    }

    public int Life
    {
        set
        {
            life = value;
        }
        get
        {
            return life;
        }
    }

    public void SetLife(int life)
    {
        this.life += life;
    }

    public int Ammunition
    {
        get
        {
            return ammunition;
        }
    }

    public void SoundReload()
    {
        audioS.PlayOneShot(audios[2], 0.2f);
    }
}
