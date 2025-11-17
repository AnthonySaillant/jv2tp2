using UnityEngine;

public class CharacterStateSleep : CharacterBaseState
{
    private bool enteredState = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.LowerSleepiness();

        if (!enteredState)
        {
            character.MakeInvisible();
            enteredState = true;
        }
    }

    public override void ManageTransitions()
    {
        if (vitals.IsSleepinessBellowTarget)
        {
            if (!vitals.IsHungerAboveThreshold)
            {
                var foodBuilding = blackboard.GetRandomFoodBuilding();
                if (foodBuilding != null)
                {
                    character.MakeVisible();
                    enteredState = false;

                    blackboard.TargetDestination = foodBuilding;
                    blackboard.NextState = CharacterStateMachine.CharacterStateType.Eat;
                    stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                    return;
                }
            }

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
