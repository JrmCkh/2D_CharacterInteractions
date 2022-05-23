using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    public void SetMaxHealth(int health)
    {
        this.slider.maxValue = health;
        this.slider.value = health; 
    }

    public void SetHealth(int health)
    {
        this.slider.value = health;
    }
}
