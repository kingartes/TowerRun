using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _checkCollider;

    private List<Human> _humans;

    public event UnityAction<int> HumanAdded;

    private void Start ()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        Human startHuman = Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform);
        InsertHuman(startHuman);        
        _humans[0].Run();
        HumanAdded?.Invoke(_humans.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            List<Human> collectedHumans = collisionTower.CollectHuman(_distanceChecker, _fixationMaxDistance);

            if (collectedHumans != null )
            {
                _humans[0].StopRun();

                for (int i = collectedHumans.Count - 1; i >= 0; i--)
                {
                    var insertHuman = collectedHumans[i];
                    InsertHuman(insertHuman);
                    DisplaceCheckers(insertHuman);
                }

                HumanAdded?.Invoke(_humans.Count);
                StartAnimations();
            }
            collisionTower.Break();
        }
    }

    private void InsertHuman(Human insertHuman)
    {
        _humans.Insert(0, insertHuman);
        SetHumanPosition(insertHuman);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplaceCheckers(Human human)
    {
        float displaceScale = human.GetHeight();
        Vector3 distanceCheckerNewPosition = _distanceChecker.position;
        distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;
        _distanceChecker.position = distanceCheckerNewPosition;
        _checkCollider.center = _distanceChecker.localPosition;
    }

    private void StartAnimations()
    {
        for (var i = 0; i < _humans.Count; i++)
        {
            if (i == 0)
            {
                _humans[i].Run();
            } else
            {
                _humans[i].StartRandomAnimation();
            }
        }
    }
}
