using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopUp : MonoBehaviour {

    [SerializeField] private Slider speedSlider;
    [SerializeField] private Text nameText;

    public void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 2);
        nameText.text = PlayerPrefs.GetString("name", "Nothing saved");
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }


    public void OnSubmitName(string name)
    {
        Debug.Log(name);
        nameText.text = name;
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.Save();

    }

    public void OnSpeedValue(float speed)
    {
        PlayerPrefs.SetFloat("speed", speed);
        PlayerPrefs.Save();

        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);

    }

}
