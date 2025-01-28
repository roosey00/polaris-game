using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class HealthBarSynchronizer : ComponentInitalizeBehaviour
{
    [ReadOnly(true)] public Creature Owner = null;
    [SerializeField, ReadOnly] protected Slider HpBarSlider = null;
    [SerializeField, ReadOnly] protected Image HpBarBackgroundImage = null;
    [SerializeField, ReadOnly] protected TextMeshProUGUI HpBarText = null;

    public Vector3 GapFromCenter = new Vector3(0, 100, 0);

    override protected void InitalizeComponent()
    {
        //Owner ??= GameObject.Find("ForUseXYZ").transform.Find("Player").GetComponent<Player>();
        HpBarSlider ??= GetComponent<Slider>();
        HpBarBackgroundImage ??= transform.Find("Background").GetComponent<Image>();
        HpBarText ??= transform.Find("Text (TMP)")?.GetComponent<TextMeshProUGUI>();
        if (!(Owner is null))
        {
            UpdateHpBar();
        }
    }

    public void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(Owner.transform.position) + GapFromCenter;
    }

    public void UpdateHpBar()
    {
        //Debug.Log(Owner.name);
        HpBarSlider.value = Owner.Status.CurrentHp;
        HpBarSlider.maxValue = Owner.Status.MaxHp + Owner.Status.Shield;
        HpBarBackgroundImage.color = (Owner.Status.Shield > 0) ? Color.white : Color.black;
        if (!(HpBarText is null))
        {
            HpBarText.text = $"{HpBarSlider.value} / {HpBarSlider.maxValue}" + ((Owner.Status.Shield > 0) ? $"+ {Owner.Status.Shield}" : "");
        }
    }
}
