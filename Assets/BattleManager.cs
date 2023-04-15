using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Player AI;
    [SerializeField] State state;
    //[SerializeField] bool AISelected;
    //[SerializeField] bool AtkDone;
    //[SerializeField] bool DamageDone;
    [SerializeField] bool ReturnDone;
    [SerializeField] bool PlayerEliminated;

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

                    if(player.SelectedChar.CurrentHP == 0)
                    {
                        player.Remove(player.SelectedChar);
                    }
                    if(AI.SelectedChar.CurrentHP == 0)
                    {
                        AI.Remove(AI.SelectedChar);
                    }
                }
                break;

            case State.Damaging:
                if(player.IsDamaging() == false && AI.IsDamaging() == false) 
                {
                    
                    state = State.Prepare;
                }
                break;

            case State.Returning:
                if(ReturnDone)
                {
                    if(PlayerEliminated) 
                    {
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
}
