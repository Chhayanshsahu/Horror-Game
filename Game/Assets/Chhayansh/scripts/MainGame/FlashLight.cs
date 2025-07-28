using UnityEngine;
using TMPro;

public class FlashLight : MonoBehaviour
{
    public Light flashlightLight;
    public TMP_Text text;
    public TMP_Text batteryText;
    public float lifetime = 100f;
    public float batteries = 0f;
    public AudioSource flashON;
    public AudioSource flashOFF;
    private bool isOn;

    void Start()
    {
        flashlightLight = GetComponent<Light>();
        flashlightLight.enabled = false;
        isOn = false;
    }

    void Update()
    {
        UpdateUI();
        if (isOn)
        {
            DrainBattery();
        }
        ClampValues();

        if (lifetime <= 0 && isOn)
        {
            flashlightLight.enabled = false;
            flashOFF.Play();
            isOn = false;
        }

        if (isOn && lifetime < 10f)
        {
            flashlightLight.enabled = Mathf.Sin(Time.time * 20f) > 0;
        }
    }

    void UpdateUI()
    {
        text.text = $"Flashlight: {lifetime:0}%";
        batteryText.text = $"Battery: {batteries}";
    }

    void DrainBattery()
    {
        lifetime -= Time.deltaTime;
    }

    void ClampValues()
    {
        lifetime = Mathf.Clamp(lifetime, 0f, 100f);
        batteries = Mathf.Max(0f, batteries);
    }

    public void ToggleFlashlightByUI()
    {
        if (lifetime <= 0) return;
        isOn = !isOn;
        flashlightLight.enabled = isOn;
        if (isOn) flashON.Play();
        else flashOFF.Play();
    }

    public void ReloadBatteryByUI()
    {
        if (batteries > 0)
        {
            batteries--;
            lifetime += 50f;
        }
    }
}
