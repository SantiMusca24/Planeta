using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moneyOvertime : MonoBehaviour
{
    DateTime _nowStaminaTime;
    DateTime _lastStaminaTime;
    TimeSpan _differenceTime;
    int _hoursPassed;
    public TMP_Text goldTxt;
    public GameObject popUp;


    [SerializeField] int _maxStamina = 10;
    int _currentStamina = 10;

    //[SerializeField] float _timerToRecharge = 10;

    bool _recharging;

    TimeSpan notifTimer;
    //int id;
    void Start()
    {
        LoadGame();
        _nowStaminaTime = DateTime.Now;        
        _differenceTime = _nowStaminaTime - _lastStaminaTime;
        _hoursPassed = _differenceTime.Hours + (_differenceTime.Days*24);
        Debug.Log("" + _nowStaminaTime + " - " + _lastStaminaTime + " = " + _differenceTime);
        Debug.Log("ELAPSED TIME: " + _hoursPassed + " hours");
        if (_hoursPassed > 8) _hoursPassed = 8;
        if (_hoursPassed < 0) _hoursPassed = 0;
        if (_hoursPassed > 0)
        {
            popUp.SetActive(true);
            goldTxt.text = "" + GameManager.Instance.IPS * (3600 * _hoursPassed);
        }
        Debug.Log("" + GameManager.Instance.IPS);
        Debug.Log("" + GameManager.Instance.IPS * (3600 * _hoursPassed));
        GameManager.Instance.count += GameManager.Instance.IPS * (3600 * _hoursPassed);


        //LoadGame();
        //StartCoroutine(ChargingStamina());
    }

    private void Update()
    {
        _lastStaminaTime = DateTime.Now;
        SaveGame();
    }

    public void OnClick()
    {
        popUp.SetActive(false);
    }
    /*IEnumerator ChargingStamina()
    {
        //UpdateStamina();
        UpdateTimer();
        _recharging = true;

        while (_currentStamina < _maxStamina)
        {
            DateTime current = DateTime.Now;
            DateTime nextTime = _nextStaminaTime;

            bool addingStamina = false;

            while (current > nextTime)
            {
                if (_currentStamina >= _maxStamina) break;

                _currentStamina++;
                addingStamina = true;
                UpdateStamina();

                DateTime timeToAdd = nextTime;

                if (_lastStaminaTime > nextTime) timeToAdd = _lastStaminaTime;

                nextTime = AddDuration(timeToAdd, _timerToRecharge);
            }

            if (addingStamina)
            {
                _nextStaminaTime = nextTime;
                _lastStaminaTime = DateTime.Now;
            }

            UpdateTimer();
            UpdateStamina();
            SaveGame();
        }
        _recharging = false;
    }*/

    //DateTime AddDuration(DateTime timeToAdd, float duration) => timeToAdd.AddSeconds(duration);

    //public bool HasEnoughStamina(int stamina) => _currentStamina - stamina >= 0;

    void UpdateTimer()
    {
        if (_currentStamina >= _maxStamina)
        {
            return;
        }

        notifTimer = _nowStaminaTime - DateTime.Now;
    }

    void SaveGame()
    {
        //PlayerPrefs.SetInt("_currentStamina", _currentStamina);
        //PlayerPrefs.SetString("_nowStaminaTime", _nowStaminaTime.ToString());
        PlayerPrefs.SetString("_lastStaminaTime", _lastStaminaTime.ToString());
    }
    void LoadGame()
    {
        _lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("_lastStaminaTime"));
    }

    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
            return DateTime.Now;
        else
            return DateTime.Parse(date);
    }

}
