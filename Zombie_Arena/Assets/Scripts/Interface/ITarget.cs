using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public interface ITarget : IEventSystemHandler
{
    IEnumerable GetHit(int damage);
    IEnumerable GetHit(int damage, Vector3 location);
}
