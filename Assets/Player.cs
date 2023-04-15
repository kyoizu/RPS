using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] Character selectedChar;
    [SerializeField] List<Character> characterList;
    [SerializeField] Transform atkRef;
    Vector3 direction = Vector3.zero;

    public Character SelectedChar { get => selectedChar;}

    public void Prepare()
    {
        selectedChar = null;
    }

    public void SelectChar(Character character) 
    {
        selectedChar = character;
    }

    public void SetPlay(bool value)
    {
        foreach (var character in characterList)
        {
            character.Button.interactable = value;
        }
    }

    public void Update()
    {
        //if(direction == Vector3.zero)
        //{
        //    return;
        //}
        //if(Vector3.Distance(selectedChar.transform.position, atkRef.position) > 0.05f)
        //{
        //    selectedChar.transform.position += 5 * direction * Time.deltaTime;
        //}
        //else 
        //{
        //    direction = Vector3.zero;
        //    selectedChar.transform.position = atkRef.position;
        //}
    }

    public void Attack()
    {
        //direction = atkRef.position - selectedChar.transform.position;
        //direction.Normalize();
        selectedChar.transform.DOMove(atkRef.position, 1f).SetEase(Ease.InOutBounce);
    }

    public bool IsAttacking()
    {
        return DOTween.IsTweening(selectedChar.transform, true);
    }

    public void TakeDmg(int damageValue)
    {
        selectedChar.ChangeHP(-damageValue);
        var spriRend = selectedChar.GetComponent<SpriteRenderer>();
        spriRend.DOColor(Color.red, 0.1f).SetLoops(4,LoopType.Yoyo);
    }

    public bool IsDamaging()
    {
        var spriRend = selectedChar.GetComponent<SpriteRenderer>();
        return DOTween.IsTweening(spriRend);
    }

    public void Remove(Character character)
    {
        if(characterList.Contains(character) == false) 
        {
            return;
        }

        character.Button.interactable = false;
        character.gameObject.SetActive(false);
        characterList.Remove(character);
    }
}
