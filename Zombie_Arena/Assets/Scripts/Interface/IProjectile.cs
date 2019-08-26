using System.Collections;
using UnityEngine.EventSystems;

public interface IProjectile : IEventSystemHandler
{
    IEnumerable Setup(int damage);
}
