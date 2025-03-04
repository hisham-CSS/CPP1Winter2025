using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    public Button creditsBtn;
    public Button backBtn;

    public Slider volSlider;
    public TMP_Text volText;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.Settings;

        if (backBtn) backBtn.onClick.AddListener(JumpBack);
        if (creditsBtn) creditsBtn.onClick.AddListener(() => SetNextMenu(MenuStates.Credits));

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);
            OnSliderValueChanged(volSlider.value);
        }
    }

    void OnSliderValueChanged(float value)
    {
        float roundedValue = Mathf.Round(value * 100);
        if (volText) volText.text = $"{roundedValue}%";
    }
}
