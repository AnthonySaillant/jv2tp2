using UnityEngine;

public class CharacterStateMove : CharacterBaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Act()
    {
        if (blackboard.TargetDestination != null)
        {
            character.NavigateTo(blackboard.TargetDestination);
        }
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();
    }

    public override void ManageTransitions()
    {
        if (stateMachine.TrashBehaviour == CharacterStateMachine.CityCharacterTrashBehaviour.Throw &&
            blackboard.ShouldThrowTrash)
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.ThrowTrash);

            blackboard.ShouldThrowTrash = false;
        }

        if (blackboard.LastSeenTrash != null)
        {
            Trash trash = blackboard.LastSeenTrash.GetComponent<Trash>();
            if (trash != null && trash.IsAvailable)
            {
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.TakeTrash);
            }
        }
        if (blackboard.LastSeenFriend != null && blackboard.LastSeenFriend.IsAvailable)
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Greet);
            return;
        }
        if (blackboard.TargetDestination != null && character.IsCloseTo(blackboard.TargetDestination))
        {
            //changer le state selon la destination
        }
    }
}
