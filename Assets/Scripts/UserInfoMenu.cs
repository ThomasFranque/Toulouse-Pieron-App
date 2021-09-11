using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] TMP_InputField _dayInput;
    [SerializeField] TMP_InputField _monthInput;
    [SerializeField] TMP_InputField _yearInput;
    [SerializeField] Button _continueButton;
    [SerializeField] Toggle _toggleButton;
    [Space]
    [SerializeField] private GameObject _loadingScreen;
    [Space]
    [SerializeField] private GameObject[] _enableOnContinue;
    [SerializeField] private GameObject[] _disableOnContinue;

    private bool showingInfo;

    private void Update()
    {
        bool canContinue = true;
        if (!showingInfo)
        {
            canContinue &= !string.IsNullOrEmpty(_nameInput.text);
            canContinue &= !string.IsNullOrEmpty(_dayInput.text);
            canContinue &= !string.IsNullOrEmpty(_monthInput.text);
            canContinue &= !string.IsNullOrEmpty(_yearInput.text);
        }
        else // Showing rules and info
        {
            canContinue &= _toggleButton.isOn;
        }

        _continueButton.interactable = canContinue;
    }

    public void Continue()
    {
        if (!showingInfo)
        {

            foreach (GameObject g in _disableOnContinue)
                g.SetActive(false);
            foreach (GameObject g in _enableOnContinue)
                g.SetActive(true);

            showingInfo = true;
        }
        else
        {
            _loadingScreen.SetActive(true);
            StartCoroutine(CDelayBeforeNewSession());
        }
    }
    private IEnumerator CDelayBeforeNewSession()
    {
        yield return new WaitForSeconds(.5f);
        SessionManager.NewSession(_nameInput.text, _dayInput.text, _monthInput.text, _yearInput.text);
    }
}