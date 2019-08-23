using UnityEngine;
using System.Collections;

public class ZombieScript : MonoBehaviour {
    private GameObject player;
    public GameObject blood;
    public int life;
    private bool attack;
    private bool move;
    private Animator anim;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        attack = false;
        move = true;
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        anim = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        anim.SetBool("move", move);
        anim.SetBool("attack", attack);
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
        if (col.gameObject.tag == "Bullet")
        {
            Life -= 10;
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
        player.gameObject.GetComponent<PlayerScript>().Life -= (tag == "Enemy")? 2 : 10;
        SingletonManager.instance.GetLife = player.gameObject.GetComponent<PlayerScript>().Life;
    }
}
