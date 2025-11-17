using UnityEngine;

public class CharacterStateTakeTrash : CharacterBaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();

        if (blackboard.TargetTrash != null && blackboard.TargetTrash.IsAvailable)
        {
            if (!character.IsCloseTo(blackboard.TargetTrash))
            {
                character.NavigateTo(blackboard.TargetTrash);
            }
            else
            {
                character.PickUpTrash(blackboard.TargetTrash);

                blackboard.TargetTrash = null;
            }
        }
    }

    public override void ManageTransitions()
    {
        if ((blackboard.TargetTrash == null || !blackboard.TargetTrash.IsAvailable) && !character.IsPickingTrash())
        {
            blackboard.TargetTrash = null;
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
        }
    }
}