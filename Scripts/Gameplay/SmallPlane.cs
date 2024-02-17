using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPlane : MonoBehaviour
{
    private Vector2 _targetPosition;
    private void Start()
    {
        _targetPosition = Vector2.zero;
    }
    private void FixedUpdate()
    {
        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(_targetPosition.x, transform.position.y);
    }
}
