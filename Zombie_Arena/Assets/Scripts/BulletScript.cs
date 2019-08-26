using UnityEngine;
using System.Collections;
using System;

public class BulletScript : MonoBehaviour, IProjectile {
    private int damage = 0;

    void OnCollisionEnter(Collision col)
    {
        col.gameObject.Send<ITarget>(obj => obj.GetHit(damage, transform.position));
        Destroy(gameObject);
    }

    public IEnumerable Setup(int damage)
    {
        this.damage = damage;
        yield return null;
    }
}
