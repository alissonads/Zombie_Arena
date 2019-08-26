using UnityEngine;
using System.Collections;
using System;
using Syrinj;

public class PlayerScript : MonoBehaviour, IActor, ITarget
{
    public float vel;
    
    [FindWithTagAttribute("SpawnPoint")]
    private Transform spawPosition;
    
    public GameObject bullet;

    public AudioClip []audios;

    [FindWithTagAttribute("MainCamera")]
    private AudioSource audioS;

    [GetComponentInChildrenAttribute(typeof(Animator))]
    private Animator anim;
    private int ammunition;
    private int comb;
    private int life;

    // Use this for initialization
    void Start () {
        //anim = GetComponentInChildren<Animator>();
        ammunition = SingletonManager.instance.GetAmmunition;
        life = SingletonManager.instance.GetLife;
        comb = 7;
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal2");
        float y = Input.GetAxis("Vertical2");
        
        bool move = (x > 0 || x < 0 || y > 0 || y < 0) ? true : false;

        gameObject.Send<IActor>(player => player.Rotate(x, y, 0));

        if (move)
        {
            gameObject.Send<IActor>(player => player.Move(0, 0, 0));
            if(anim) anim.SetBool("move", true);
        }
        else
        {
            if (anim) anim.SetBool("move", false);
        }
        Shoot();
    }

    private void Shoot()
    {
        if(Input.GetButtonDown("FIRE"))
        {
            if (ammunition > 0)
            {
                gameObject.Send<IActor>(player => player.Attack());
                if (anim) anim.SetBool("shoot", true);
            }
            else
                audioS.PlayOneShot(audios[1], 1.0f);
        }
        if(Input.GetKeyDown(KeyCode.Q) && ammunition > 0)
        {
            if (anim) anim.SetBool("reload", true);
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

    public IEnumerable Move(float x, float y, float z)
    {
        transform.Translate(Vector3.right * vel * Time.deltaTime);
        
        yield return null;
    }

    public IEnumerable Move(Vector3 position)
    {
        transform.Translate(Vector3.right * vel * Time.deltaTime);

        yield return null;
    }

    public IEnumerable Attack()
    {
        var obj = Instantiate(bullet, spawPosition.position, spawPosition.rotation) as GameObject;
        obj.Send<IProjectile>(projectile => projectile.Setup(10));
        obj.GetComponent<Rigidbody>().AddForce(spawPosition.right * 1000);
        ammunition--;
        SingletonManager.instance.GetAmmunition = ammunition;
        audioS.PlayOneShot(audios[0], 1.0f);

        yield return null;
    }

    public IEnumerable GetHit(int damage)
    {
        life -= damage;
        SingletonManager.instance.GetLife = life;
        yield return null;
    }

    public IEnumerable Rotate(float x, float y, float z)
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

        yield return null;
    }

    public IEnumerable GetHit(int damage, Vector3 location)
    {
        throw new NotImplementedException();
    }
}
