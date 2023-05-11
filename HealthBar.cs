using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.value = health; //sets the slider's value to the max health when game starts
    }

    public void SetHealth(int health)
    {
        slider.value = health; //sets the slider's value to whaterver the current health is
    }
}
