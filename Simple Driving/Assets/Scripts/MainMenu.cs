using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private NotificationHandler notificationHandler;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;

    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start()
    {
        OnApplicationFocus(true);
    }
    //check energy levels when player has appliacion in focus
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            return;
        }

        CancelInvoke();

        highScoreText.text = "High Score: " + PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0).ToString(); // get the current highscrore and diplays it

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);// check current energy

        if(energy == 0)//if out of energy
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);//get the datetime when last saved

            if(energyReadyString == string.Empty)//if datetime empty return
            {
                return;
            }

            DateTime energyReady = DateTime.Parse(energyReadyString);//turn string to DateTime variable

            if(DateTime.Now > energyReady)// checks if datetime of Now is greater then energy ready datetime and then restores the energy
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged),(energyReady - DateTime.Now).Seconds);
            }
        }

        energyText.text = $"Play ({energy})";
    }

    //Set energy to max energy and save the energy and set the text
    private void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play ({energy})";
    }

    //Changes the scene to the play scene using index
    public void Play()
    {
        if(energy > 0)//checks if you have more then 0 energy
        {
            energy--;// removes 1 energy from your energy

            PlayerPrefs.SetInt(EnergyKey, energy);// saves your energy to player prefs

            if(energy == 0)//checks if the updated energy is equal to 0 if true will save the datetime you will get max energy again 
            {
                DateTime savedata = DateTime.Now.AddMinutes(energyRechargeDuration);
                PlayerPrefs.SetString(EnergyReadyKey, savedata.ToString());
#if UNITY_ANDROID
                notificationHandler.ScheduleNotification(savedata);
#endif
            }

            SceneManager.LoadScene(1);
        }

    }
}
