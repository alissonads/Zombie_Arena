using UnityEngine;
using System.Collections;
using System;
using Syrinj;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour, IActor, ITarget
{
    [FindAttribute("Player")]
    private GameObject player;

    [GetComponentInChildrenAttribute(typeof(Animator))]
    private Animator anim;

    [GetComponentAttribute(typeof(NavMeshAgent))]
    private NavMeshAgent nvm;

    [GetComponentInChildrenAttribute(typeof(SpriteRenderer))]
    private SpriteRenderer renderer;

    private bool attack;
    private bool move;
    private bool collision;
    private int index;
    private float time;
    private float ctrlSound;

    public int life;
    public GameObject blood;
    public Material []material;

    //public AudioClip audio;
    //public AudioSource audioS;
    // Use this for initialization
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        attack = false;
        move = true;
        //GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.transform.position);
        //anim = GetComponentInChildren<Animator>();
        collision = false;
        index = 0;
        time = 0;
        ctrlSound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.Send<IActor>(monster => monster.Move(player.transform.position));
        gameObject.Send<IActor>(monster => monster.Attack());

        if (life <= 0)
        {
            SingletonManager.instance.GetScore += 30;
            SingletonManager.instance.enemies--;
            Destroy(gameObject);
        }
        if (collision)
        {
            time += Time.deltaTime * 2;

            if (time > 1.0f)
            {
                index = index > 1 ? 0 : index;
                renderer.color = material[index].color;
                index++;
                time = 0;
            }
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
        player.Send<ITarget>(p => p.GetHit(5));
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
        nvm.speed = 7.0f;
        anim.speed = 2;
        collision = true;

        yield return null;
    }

    public IEnumerable GetHit(int damage, Vector3 location)
    {
        life -= damage;
        nvm.speed = 7.0f;
        anim.speed = 2;
        collision = true;

        Instantiate(blood, location, blood.transform.rotation);

        yield return null;
    }
}
