using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopUp settingsPopup;

    private int _score;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);  // Add a listener to do a function when event is called
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); // Remove the event when an object is destroyed to prevent errors
    }

    private void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString();
        settingsPopup.Close();
    }

    public void OnEnemyHit()
    {
        _score++;
        scoreLabel.text = _score.ToString();
    }

    public void OpenSettings()
    {
        settingsPopup.Open();
    }

    public void CloseSettings()
    {
        settingsPopup.Close();
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }


}
