using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] string CharName;
    [SerializeField] CharacterType type;
    [SerializeField] int currentHP;
    [SerializeField] int MaxHP;
    [SerializeField] int atkPower;
    [SerializeField] TMP_Text OverheadName;
    [SerializeField] Image Avatar;
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text typeText;
    [SerializeField] Image HPBar;
    [SerializeField] TMP_Text HPText;
    [SerializeField] Button button;

    public Button Button { get => button; }
    public CharacterType Type { get => type;}
    public int AtkPower { get => atkPower;}
    public int CurrentHP { get => currentHP; }

    // Start is called before the first frame update
    private void Start()
    {
        OverheadName.text = CharName;
        NameText.text = CharName;
        typeText.text = type.ToString();
        button.interactable = false;
        UpdateHPUI();
    }

    public void ChangeHP(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, MaxHP);
        UpdateHPUI();
        
    }
    private void UpdateHPUI()
    {
        HPBar.fillAmount = (float)currentHP/ (float)MaxHP;
        HPText.text = currentHP + "/" + MaxHP;
    }
}
