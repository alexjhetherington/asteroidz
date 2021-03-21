using Prime31.MessageKit;
using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;

    private Vector2 _screenSize;

    void Start()
    {
        MessageKit<TargetCollider>.addObserver(MessageIds.TARGET_HIT, TargetHit);
        MessageKit<Vector2>.addObserver(MessageIds.SCREEN_SIZE, UpdateScreenSize);
        StartCoroutine(SpawnFirstTarget());
    }

    void OnDestroy()
    {
        MessageKit<TargetCollider>.removeObserver(MessageIds.TARGET_HIT, TargetHit);
        MessageKit<Vector2>.removeObserver(MessageIds.SCREEN_SIZE, UpdateScreenSize);
    }

    //Need to wait until the manager knows what the screen boundaries are
    private IEnumerator SpawnFirstTarget()
    {
        yield return new WaitForEndOfFrame();
        SpawnNewTarget();
    }

    private void SpawnNewTarget()
    {
        //Don't spawn exactly on the screen edge
        TrashMan.spawn(targetPrefab, new Vector3(
                                    Random.Range(-_screenSize.x / 2 + 1.5f, _screenSize.x / 2 - 1.5f),
                                    Random.Range(-_screenSize.y / 2 + 1.5f, _screenSize.y / 2 - 1.5f),
                                    0));
    }

    private void TargetHit(TargetCollider obj)
    {
        SpawnNewTarget();
    }

    private void UpdateScreenSize(Vector2 value)
    {
        _screenSize = value;
    }
}
