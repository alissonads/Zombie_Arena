using UnityEngine;
using System.Collections;
using System;
using Syrinj;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour, IActor, ITarget {
    [FindAttribute("Player")]
    private GameObject player;

    public GameObject blood;
    public int life;
    private bool attack;
    private bool move;

    [GetComponentInChildrenAttribute(typeof(Animator))]
    private Animator anim;

    [GetComponentInChildrenAttribute(typeof(NavMeshAgent))]
    private NavMeshAgent nvm;

    // Use this for initialization
    void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        attack = false;
        move = true;
        //nvm.SetDestination(player.transform.position);
        //anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.Send<IActor>(zombie => zombie.Move(player.transform.position));
        gameObject.Send<IActor>(zombie => zombie.Attack());
        
        if (life <= 0)
        {
            SingletonManager.instance.GetScore += (tag == "Enemy")? 10 : 50;
            SingletonManager.instance.enemies--;
            Destroy(gameObject);
        }
    }

    public int Life
    {
        set { life = value; }
        get { return life; }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            attack = true;
            move = false;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            move = true;
            attack = false;
        }
    }

    public void Dano()
    {
        player.Send<ITarget>(p => p.GetHit((tag == "Enemy") ? 2 : 10));
    }

    public IEnumerable Move(float x, float y, float z)
    {
        nvm.SetDestination(new Vector3(x, y, z));
        anim.SetBool("move", move);
        yield return null;
    }

    public IEnumerable Move(Vector3 position)
    {
        nvm.SetDestination(position);
        anim.SetBool("move", move);
        yield return null;
    }

    public IEnumerable Rotate(float x, float y, float z)
    {
        yield return null;
    }

    public IEnumerable Attack()
    {
        //player.Send<ITarget>(p => p.GetHit((tag == "Enemy") ? 2 : 10));
        anim.SetBool("attack", attack);
        yield return null;
    }

    public IEnumerable GetHit(int damage)
    {
        life -= damage;
        yield return null;
    }

    public IEnumerable GetHit(int damage, Vector3 location)
    {
        life -= damage;
        Instantiate(blood, location, blood.transform.rotation);
        yield return null;
    }
}
