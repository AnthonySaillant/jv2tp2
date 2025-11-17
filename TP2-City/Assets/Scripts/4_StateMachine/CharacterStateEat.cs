using UnityEngine;

public class CharacterStateEat : CharacterBaseState
{
    private bool enteredState = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.LowerHunger();

        if (!enteredState)
        {
            character.MakeInvisible();
            enteredState = true;
        }
    }

    public override void ManageTransitions()
    {
        if (vitals.IsHungerBellowTarget)
        {
            if (vitals.IsLonelinessAboveThreshold)
            {
                var socialBuilding = blackboard.GetRandomSocialBuilding();
                if (socialBuilding != null)
                {
                    character.MakeVisible();
                    enteredState = false;

                    blackboard.TargetDestination = socialBuilding;
                    blackboard.NextState = CharacterStateMachine.CharacterStateType.Socialize;
                    stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                    return;
                }
            }

            if (!vitals.IsSleepinessAboveThreshold && blackboard.House != null)
            {
                character.MakeVisible();
                enteredState = false;

                blackboard.TargetDestination = blackboard.House;
                blackboard.NextState = CharacterStateMachine.CharacterStateType.Sleep;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                return;
            }

            if (blackboard.Workplace != null)
            {
                character.MakeVisible();
                enteredState = false;

                blackboard.TargetDestination = blackboard.Workplace;
                blackboard.NextState = CharacterStateMachine.CharacterStateType.Work;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
            }
        }
    }
}
