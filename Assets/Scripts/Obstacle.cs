using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector2Int _heightRange;
    private int _height;

    public int Height => _height;

    private void Start ()
    {
        _height = Random.Range(_heightRange.x, _heightRange.y);
        transform.localScale = new Vector3(transform.localScale.x, _height, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, (_height / 2f), transform.position.z);
    }
}
