using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Player AI;
    [SerializeField] State state;
    [SerializeField] bool PlayerSelected;
    [SerializeField] bool AISelected;
    [SerializeField] bool AtkDone;
    [SerializeField] bool DamageDone;
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
                    player.Attack();
                    AI.Attack();
                    state = State.Attack;
                }
                break;

            case State.Attack:
                if(player.IsAttacking() == false && AI.IsAttacking() == false) 
                {
                    state = State.Damaging;
                }
                break;

            case State.Damaging:
                if(DamageDone) 
                {
                    state = State.Returning;
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
}
