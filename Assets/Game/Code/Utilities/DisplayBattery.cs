using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayBattey : MonoBehaviour
{
    [SerializeField] private Slider batterySlider;
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        batterySlider.value = FirebaseController.Instance.currentBattery;
        text.text = FirebaseController.Instance.currentBattery.ToString() + "/" + batterySlider.maxValue;
    }
}