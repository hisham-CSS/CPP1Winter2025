using TMPro;
using UnityEngine;

public class InGame : BaseMenu
{
    public TMP_Text livesText;

    public override void Init(MenuController context)
    {
        base.Init(context);
        state = MenuStates.InGame;

        livesText.text = $"Lives: {GameManager.Instance.lives}";

        GameManager.Instance.OnLifeValueChanged += LifeValueChanged;
    }

    private void LifeValueChanged(int value) => livesText.text = $"Lives: {value}";

    private void OnDestroy() => GameManager.Instance.OnLifeValueChanged -= LifeValueChanged;
}
