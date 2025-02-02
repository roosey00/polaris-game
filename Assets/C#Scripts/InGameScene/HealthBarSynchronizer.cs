using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class HealthBarSynchronizer : ComponentInitalizeBehaviour
{
    [ReadOnly(true)] public Creature Owner;
    [SerializeField, ReadOnly] protected Slider HpBarSlider = null;
    [SerializeField, ReadOnly] protected Image HpBarBackgroundImage = null;
    [SerializeField, ReadOnly] protected TextMeshProUGUI HpBarText = null;

    public Vector3 GapFromCenter = new Vector3(0, 100, 0);

    new protected void Reset()
    {
        base.Reset();
        Owner = null;
    }

    override protected void InitalizeComponent()
    {
        HpBarSlider ??= GetComponent<Slider>();
        HpBarBackgroundImage ??= transform.Find("Background").GetComponent<Image>();
        HpBarText ??= transform.Find("Text (TMP)")?.GetComponent<TextMeshProUGUI>();
        UpdateHpBar();        
    }

    public void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(Owner.transform.position) + GapFromCenter;
    }

    public void UpdateHpBar()
    {
        if (Owner is null) return;
        HpBarSlider.maxValue = Owner.Status.MaxHp + Owner.Status.Shield;
        HpBarSlider.value = Owner.Status.CurrentHp;
        HpBarBackgroundImage.color = (Owner.Status.Shield > 0) ? Color.white : Color.black;
        if (!(HpBarText is null))
        {
            HpBarText.text = $"{HpBarSlider.value} / {HpBarSlider.maxValue}" + ((Owner.Status.Shield > 0) ? $"+ {Owner.Status.Shield}" : "");
        }
    }
}
