using UnityEngine;

public class CharacterStateTakeTrash : CharacterBaseState
{
    private bool startedPicking = false;

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
                if (!startedPicking && !character.IsPickingTrash())
                {
                    character.PickUpTrash(blackboard.TargetTrash);
                    startedPicking = true;
                }
            }
        }
    }

    public override void ManageTransitions()
    {
        if ((blackboard.TargetTrash == null || !blackboard.TargetTrash.IsAvailable) && !character.IsPickingTrash())
        {
            blackboard.TargetTrash = null;
            startedPicking = false;
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
        }
    }
}
