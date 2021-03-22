using Prime31.MessageKit;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject tutorialCanvas;
    [SerializeField] private GameObject highScoreCanvas;

    [SerializeField] private TextMeshProUGUI ingameScore;
    [SerializeField] private TextMeshProUGUI endScore;

    [SerializeField] private Slider ammo;

    private float _score = 0;

    // Start is called before the first frame update
    void Start()
    {
        MessageKit.addObserver(MessageIds.PLAYER_KILLED, HandlePlayerKilled);
        MessageKit.addObserver(MessageIds.INIT_RESTART, HandleRestart);
        MessageKit<float>.addObserver(MessageIds.AMMO_LEFT, HandleAmmo);
        MessageKit<TargetCollider>.addObserver(MessageIds.TARGET_HIT, HandleTargetHit);

        HandleRestart();
    }

    private void OnDestroy()
    {
        MessageKit.removeObserver(MessageIds.PLAYER_KILLED, HandlePlayerKilled);
        MessageKit.removeObserver(MessageIds.INIT_RESTART, HandleRestart);
        MessageKit<float>.removeObserver(MessageIds.AMMO_LEFT, HandleAmmo);
        MessageKit<TargetCollider>.removeObserver(MessageIds.TARGET_HIT, HandleTargetHit);
    }

    private void HandleAmmo(float val)
    {
        ammo.value = val;
    }

    private void HandleTargetHit(TargetCollider val)
    {
        _score += val.Score;
        ingameScore.text = "SCORE: " + _score.ToString("0");
    }

    private void HandlePlayerKilled()
    {
        endScore.text = "SCORE: " + _score.ToString("0");

        gameplayCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);
        highScoreCanvas.SetActive(true);
    }

    private void HandleRestart()
    {
        _score = 0;
        ingameScore.text = "SCORE: " + _score.ToString("0");

        gameplayCanvas.SetActive(true);
        highScoreCanvas.SetActive(false);
    }
}
