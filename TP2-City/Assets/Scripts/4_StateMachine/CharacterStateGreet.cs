public class CharacterStateGreet : CharacterBaseState
{
    private Character currentFriend;

    protected override void Awake()
    {
        base.Awake();
        currentFriend = null;
    }

    public override void Act()
    {
        vitals.LowerLoneliness();

        if (blackboard.LastSeenFriend != null && !character.IsGreetingCharacter())
        {
            if (currentFriend != blackboard.LastSeenFriend)
            {
                currentFriend = blackboard.LastSeenFriend;
                character.GreetCharacter(currentFriend);
            }
        }
    }

    public override void ManageTransitions()
    {
        if (!character.IsGreetingCharacter())
        {
            stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);

            blackboard.LastSeenFriend = null;
            currentFriend = null;
        }
    }
}
