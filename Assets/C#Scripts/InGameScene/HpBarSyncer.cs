using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBarSyncer : MonoBehaviour
{        
    private Slider HpBarSlider = null;
    private TextMeshProUGUI HpBarText = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HpBarSlider ??=GetComponent<Slider>();
        HpBarText ??= transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        HpBarSlider.value = GameManager.Instance.PlayerClass.Status.CurrentHp;
        HpBarSlider.maxValue = GameManager.Instance.PlayerClass.Status.MaxHp;
        HpBarText.text = $"{HpBarSlider.value} / {HpBarSlider.maxValue}";
    }
}
