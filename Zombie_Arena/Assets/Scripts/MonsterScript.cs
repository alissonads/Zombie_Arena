using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
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
        player = GameObject.FindGameObjectWithTag("Player");
        attack = false;
        move = true;
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        anim = GetComponentInChildren<Animator>();
        collision = false;
        index = 0;
        time = 0;
        ctrlSound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        anim.SetBool("move", move);
        anim.SetBool("attack", attack);
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
                GetComponentInChildren<SpriteRenderer>().color = material[index].color;
                index++;
                time = 0;
            }
        }

        //ctrlSound += Time.deltaTime;
        
        //if (ctrlSound > 2.0f)
        //{
        //    ctrlSound = 0;
        //    audioS.PlayOneShot(audio, 1.0f);
        //}
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
        if(col.gameObject.tag == "Bullet")
        {
            Life -= 10;
            GetComponent<NavMeshAgent>().speed = 7.0f;
            anim.speed = 2;
            collision = true;
            Instantiate(blood, col.transform.position, blood.transform.rotation);
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
        player.gameObject.GetComponent<PlayerScript>().Life -= 5;
        SingletonManager.instance.GetLife = player.gameObject.GetComponent<PlayerScript>().Life;
    }
}
