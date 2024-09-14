using UnityEngine;
using UnityEngine.UI;

public class DisplayBattey : MonoBehaviour
{
    [SerializeField] private Slider batterySlider;

    private void OnEnable()
    {
        batterySlider.value = FirebaseController.Instance.currentBattery;
    }
}