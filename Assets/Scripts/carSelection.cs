using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class carSelection : MonoBehaviour
{
    public Sprite[] availableCars;
    public Sprite[] availableBikes;
    public TMP_Dropdown choices;
    public Toggle bikeToggle;
    private Sprite[] toChooseFrom;

    private void Start()
    {
        toChooseFrom = availableCars;
    }
    // Start is called before the first frame update
    public void toggleBike() {
        if (bikeToggle.isOn)
        {
            toChooseFrom = availableBikes;
        }
        else {
            toChooseFrom = availableCars;
        }
        selectCar();
    }

    public void selectCar() {
        int choice = choices.value;
        GetComponent<Image>().sprite = toChooseFrom[choice];
    }
}
