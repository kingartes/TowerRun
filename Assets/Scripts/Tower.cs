﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2Int _humanInTowerRange;
    [SerializeField] private Human[] _humansTemplate;
    [SerializeField] private float _splashForce;

    private List<Human> _humanInTower;

    private void Start()
    {
        _humanInTower = new List<Human>();
        int humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }

    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _humansTemplate[Random.Range(0, _humansTemplate.Length)];
            _humanInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));
            _humanInTower[i].transform.localPosition = new Vector3(0, _humanInTower[i].transform.localPosition.y, 0);

            spawnPoint = _humanInTower[i].FixationPoint.position;
        }
    }

    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for(var i = 0; i < _humanInTower.Count; i++)
        {
            var human = _humanInTower[i];
            float distanceBetweenPoint = CheckDistanceY(distanceChecker, human.FixationPoint.transform);
            if(distanceBetweenPoint < fixationMaxDistance)
            {
                List<Human> collectedHumans = _humanInTower.GetRange(0, i + 1);
                _humanInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
        }
        return null;
    }

    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
        return Vector3.Distance(distanceCheckerY, humanFixationPointY);
    }
    

    public void Break()
    {
        SplashHumans();
        StartCoroutine(DestroyOnDelay());
    }

    private void SplashHumans()
    {
        foreach(var human in _humanInTower)
        {
            var humanRigidBody = human.GetComponent<Rigidbody>();
            humanRigidBody.isKinematic = false;
            
            humanRigidBody.AddForce(GetSplashForce(), ForceMode.Impulse);
        }
    }

    private IEnumerator DestroyOnDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }

    private Vector3 GetSplashForce()
    {
        return new Vector3(Random.Range(0, 10) * _splashForce, Random.Range(0, 10) * _splashForce, Random.Range(0, 10) * _splashForce);
    }
}
