using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ParkingCounter : MonoBehaviour
{
    [SerializeField]private TMP_Text parkPlaceCounterText;
    public int parkPlaceCounter = 0;

    private void Start()
    {
        parkPlaceCounterText = gameObject.GetComponent<TMP_Text>();
        parkPlaceCounterText.SetText(parkPlaceCounter.ToString());
    }

    public void UIaddParkPlace()
    {
        parkPlaceCounter++;
        parkPlaceCounterText.SetText(parkPlaceCounter.ToString());
    }

    public void UIdeleteParkPlace()
    {
        parkPlaceCounter--;
        parkPlaceCounterText.SetText(parkPlaceCounter.ToString());
    }
}
