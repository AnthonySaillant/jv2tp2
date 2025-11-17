using UnityEngine;

public class CharacterStateThrowTrash : CharacterBaseState
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

        if (blackboard.ShouldThrowTrash && !character.IsThrowingTrash())
        {
            character.ThrowTrash();
        }
    }

    public override void ManageTransitions()
    {
        if (!character.IsThrowingTrash())
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
        }
    }
}