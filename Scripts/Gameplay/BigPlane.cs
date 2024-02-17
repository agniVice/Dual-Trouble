using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPlane : MonoBehaviour
{
    private Vector2 _targetPosition;
    private void Start()
    {
        _targetPosition = Vector2.zero;
    }
    private void FixedUpdate()
    {
        _targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, _targetPosition.x*10));
    }
}
