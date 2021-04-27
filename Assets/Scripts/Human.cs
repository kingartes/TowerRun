using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;
    private Animator _animator;

    private BoxCollider _colider;

    public Transform FixationPoint => _fixationPoint;
    

    public void Awake()
    {
        _colider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
    }


    public float GetHeight()
    {
        return 1.15f;
    }

    public void Run()
    {
        _animator.SetBool("isRunning", true);
    }

    public void StopRun()
    {
        _animator.SetBool("isRunning", false);
    }

}
