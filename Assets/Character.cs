using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] string CharName;
    [SerializeField] CharacterType Type;
    [SerializeField] int CurrentHP;
    [SerializeField] int MaxHP;
    [SerializeField] int Atk;
    [SerializeField] TMP_Text OverheadName;
    [SerializeField] Image Avatar;
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text typeText;
    [SerializeField] Image HPBar;
    [SerializeField] TMP_Text HPText;
    [SerializeField] Button button;

    public Button Button { get => button; }

    // Start is called before the first frame update
    private void Start()
    {
        OverheadName.text = CharName;
        NameText.text = CharName;
        typeText.text = Type.ToString();
        HPBar.fillAmount = (float)CurrentHP/ (float)MaxHP;
        HPText.text = CurrentHP + "/" + MaxHP;
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
