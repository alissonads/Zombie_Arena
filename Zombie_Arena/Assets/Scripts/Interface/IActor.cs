using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public interface IActor : IEventSystemHandler
{
    IEnumerable Move(float x, float y, float z);
    IEnumerable Move(Vector3 position);
    IEnumerable Rotate(float x, float y, float z);
    IEnumerable Attack();
}
