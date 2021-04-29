using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private JumpBooster _jumpBoosterTemplate;
    [SerializeField] private int _humanTowerCount;
    [SerializeField] private float _boosterPossitionOffset;
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private int _obstacleCount;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetweenTower = roadLength / _humanTowerCount;

        float distanceTravelled = 0;
    

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravelled += distanceBetweenTower;
            GenerateTower(distanceTravelled);            
        }
        GenerateObstacles();
    }

    private void GenerateTower(float distanceTravelled)
    {
        Vector3 spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        Instantiate(_towerTemplate, spawnPoint, Quaternion.identity);
        GenerateBooster(distanceTravelled - _boosterPossitionOffset);
    }

    private void GenerateBooster(float distanceTravelled)
    {
        Vector3 spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        Instantiate(_jumpBoosterTemplate, spawnPoint, Quaternion.identity);
    }

    private void GenerateObstacle(float distanceTravelled)
    {
        Vector3 spawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        Vector3 rotation = _pathCreator.path.GetDirection(distanceTravelled, EndOfPathInstruction.Stop);
        Obstacle obstacle = Instantiate(_obstacle, spawnPoint,Quaternion.Euler(rotation));
    }

    private void GenerateObstacles()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetweenTower = roadLength / _obstacleCount;

        float distanceTravelled = 0;


        for (int i = 0; i < _obstacleCount; i++)
        {
            distanceTravelled += distanceBetweenTower;
            GenerateObstacle(distanceTravelled);
        }
    }
}
