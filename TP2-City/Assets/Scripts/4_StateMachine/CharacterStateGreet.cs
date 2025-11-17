using UnityEngine;

public class CharacterStateGreet : CharacterBaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.LowerLoneliness();

        if (blackboard.LastSeenFriend != null && !character.IsGreetingCharacter())
        {
            character.GreetCharacter(blackboard.LastSeenFriend);
        }
    }

    public override void ManageTransitions()
    {
        if (!character.IsGreetingCharacter())
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);

            blackboard.LastSeenFriend = null;
        }
    }
}
