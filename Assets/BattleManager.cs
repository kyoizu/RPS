using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject battleResult;
    [SerializeField] TMP_Text battleResultTxt;
    [SerializeField] Player AI;
    [SerializeField] State state;

    enum State
    {
        Prepare, 
        PlayerSelect, 
        AISelect, 
        Attack, 
        Damaging, 
        Returning, 
        BattleOver 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Prepare:
                player.Prepare();
                AI.Prepare();
                player.SetPlay(true);
                AI.SetPlay(false);
                state = State.PlayerSelect;
                break;
                
            case State.PlayerSelect:
                if(player.SelectedChar != null)
                {
                    player.SetPlay(false);
                    AI.SetPlay(true);
                    state = State.AISelect;
                }
                break;

            case State.AISelect:
                if(AI.SelectedChar != null)
                {
                    AI.SetPlay(false);
                    player.Attack();
                    AI.Attack();
                    state = State.Attack;
                }
                break;

            case State.Attack:
                if(player.IsAttacking() == false && AI.IsAttacking() == false) 
                {
                    //(Player winner, Player loser) = calculateBattle(player,AI);
                    CalculateBattle(player, AI, out Player winner, out Player loser);
                    if(winner == null) 
                    {
                        player.TakeDmg(AI.SelectedChar.AtkPower);
                        AI.TakeDmg(player.SelectedChar.AtkPower);
                    }
                    else 
                    {
                        loser.TakeDmg(winner.SelectedChar.AtkPower);
                    }
                    state = State.Damaging;

                    
                }
                break;

            case State.Damaging:
                if(player.IsDamaging() == false && AI.IsDamaging() == false) 
                {
                    if(player.SelectedChar.CurrentHP == 0)
                    {
                        player.Remove(player.SelectedChar);
                    }
                    if(AI.SelectedChar.CurrentHP == 0)
                    {
                        AI.Remove(AI.SelectedChar);
                    }
                    if(player.SelectedChar != null)
                    {
                        player.Return();
                    }
                    if(AI.SelectedChar != null)
                    {
                        AI.Return();
                    }
                    
                    state = State.Returning;
                }
                break;

            case State.Returning:
                if(player.IsReturning() == false && AI.IsReturning() == false)
                {
                    if(player.CharacterList.Count == 0 && AI.CharacterList.Count == 0) 
                    {
                        battleResult.SetActive(true);
                        battleResultTxt.text = "Match Draw";
                        state = State.BattleOver;
                    }
                    else if(player.CharacterList.Count == 0)
                    {
                        battleResult.SetActive(true);
                        battleResultTxt.text = "AI Wins";
                        state = State.BattleOver;
                    }
                    else if(AI.CharacterList.Count == 0)
                    {
                        battleResult.SetActive(true);
                        battleResultTxt.text = "Player Wins";
                        state = State.BattleOver;
                    }
                    else 
                    {
                        state = State.Prepare;
                    }
                }
                break;
            case State.BattleOver:
            
                break;
        }
    }

    public void CalculateBattle(Player player, Player aI, out Player winner, out Player loser)
    {
        var type1 = player.SelectedChar.Type;
        var type2 = AI.SelectedChar.Type;

        if(type1 == CharacterType.Rock && type2 == CharacterType.Paper) 
        {
            winner = AI;
            loser = player;
        }
        else if(type1 == CharacterType.Rock && type2 == CharacterType.Scissor) 
        {
            winner = player;
            loser = AI;
        }
        else if(type1 == CharacterType.Paper && type2 == CharacterType.Scissor) 
        {
            winner = AI;
            loser = player;
        }
        else if(type1 == CharacterType.Paper && type2 == CharacterType.Rock) 
        {
            winner = player;
            loser = AI;
        }
        else if(type1 == CharacterType.Scissor && type2 == CharacterType.Rock) 
        {
            winner = AI;
            loser = player;
        }
        else if(type1 == CharacterType.Scissor && type2 == CharacterType.Paper) 
        {
            winner = player;
            loser = AI;
        }
        else
        {
            winner = null;
            loser = null;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
    }
}
