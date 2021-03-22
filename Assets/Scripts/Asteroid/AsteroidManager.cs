using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [Header("Gameplay Variables")]
    [SerializeField] private AnimationCurve asteroidSpawnRate;

    [SerializeField] private float[] asteroidScaleProgression;
    [SerializeField] private Vector2[] asteroidThrustProgression;
    [SerializeField] private float[] asteroidScoreProgression;

    [Header("Setup")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject targetPrefab;

    private Vector2 _screenSize;

    private float _roundStartTime;
    private float _lastCreateAsteroidTime;

    void Start()
    {
        MessageKit<AsteroidKillInfo>.addObserver(MessageIds.ASTEROID_KILLED, AsteroidKilled);
        MessageKit<Vector2>.addObserver(MessageIds.SCREEN_SIZE, UpdateScreenSize);
        MessageKit.addObserver(MessageIds.INIT_RESTART, Restart);
        Restart();
    }
    
    void OnDestroy()
    {
        MessageKit<AsteroidKillInfo>.removeObserver(MessageIds.ASTEROID_KILLED, AsteroidKilled);
        MessageKit<Vector2>.removeObserver(MessageIds.SCREEN_SIZE, UpdateScreenSize);
        MessageKit.removeObserver(MessageIds.INIT_RESTART, Restart);
    }

    void Update()
    {
        float createCooldown = asteroidSpawnRate.Evaluate(Time.time - _roundStartTime);
        if (Time.time > _lastCreateAsteroidTime + createCooldown)
        {
            CreateAsteroid();
        }
    }

    public void Restart()
    {
        _roundStartTime = Time.time;
        
        StartCoroutine(CreateFirstAsteroid());
    }

    //Need to wait until the manager knows what the screen boundaries are
    private IEnumerator CreateFirstAsteroid()
    {
        yield return new WaitForEndOfFrame();
        CreateAsteroid();
    } 

    private void CreateAsteroid()
    {
        _lastCreateAsteroidTime = Time.time;
        SpawnAsteroidPrefab(new Vector3(
                                    Random.Range(-_screenSize.x / 2, _screenSize.x / 2), 
                                    Random.Range(-_screenSize.y / 2, _screenSize.y / 2), 
                                    0), 
                                asteroidScaleProgression[0], asteroidThrustProgression[0], true);
    }

    private void AsteroidKilled(AsteroidKillInfo killInfo)
    {
        int i;
        for (i = 0; i < asteroidScaleProgression.Length; i++)
        {
            if (killInfo.scale > asteroidScaleProgression[i])
                break;
        }
        
        if(i < asteroidScaleProgression.Length)
        {
            SpawnAsteroidPrefab(killInfo.asteroid.transform.position, asteroidScaleProgression[i], asteroidThrustProgression[i], false);
            SpawnAsteroidPrefab(killInfo.asteroid.transform.position, asteroidScaleProgression[i], asteroidThrustProgression[i], false);
        }

        if(i-1 < asteroidScoreProgression.Length)
        {
            SpawnTarget(killInfo.asteroid.transform.position, asteroidScoreProgression[i-1]);
        }
    }

    private void SpawnTarget(Vector3 position, float score)
    {
        GameObject target = TrashMan.spawn(targetPrefab, position);
        TargetCollider targetCollider = target.GetComponent<TargetCollider>();
        targetCollider.Score = score;
    }

    private void SpawnAsteroidPrefab(Vector3 position, float scale, Vector2 thrustRange, bool ghost)
    {
        GameObject asteroid = TrashMan.spawn(asteroidPrefab, position);
        AsteroidChildController asteroidChildController = asteroid.GetComponent<AsteroidChildController>();
        InitSpeedAndTorque initSpeedAndTorque = asteroid.GetComponent<InitSpeedAndTorque>();

        asteroidChildController.SetScale(scale);
        initSpeedAndTorque.ThrustRange = thrustRange;
        initSpeedAndTorque.Go();

        if (ghost)
            asteroidChildController.SpawnAsGhost();
        else
            asteroidChildController.SpawnAsSolid();
    }

    private void UpdateScreenSize(Vector2 value)
    {
        _screenSize = value;
    }
}
