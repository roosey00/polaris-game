using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public string HpBarName = "HealthBar";

    private Slider HpBarSlider = null;
    private TextMeshProUGUI HpBarText = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HpBarSlider ??= transform.Find(HpBarName).GetComponent<Slider>();
        HpBarText ??= transform.Find(HpBarName).Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HpBarSlider.value = GameManager.Instance.PlayerClass.CurrentHp;
        HpBarSlider.maxValue = GameManager.Instance.PlayerClass.Status.Hp;
        HpBarText.text = $"{HpBarSlider.value} / {HpBarSlider.maxValue}";
    }
}
