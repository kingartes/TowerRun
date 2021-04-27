using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    [SerializeField] private float _boostStrength;

    public float BoostMultiplier => _boostStrength;
}
