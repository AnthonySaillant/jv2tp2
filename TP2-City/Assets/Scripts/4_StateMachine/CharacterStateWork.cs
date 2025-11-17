using UnityEngine;

public class CharacterStateWork : CharacterBaseState
{
    private bool enteredWorkplace = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.RaiseHunger();
        vitals.RaiseLoneliness();
        vitals.RaiseSleepiness();

        if (!enteredWorkplace && blackboard.Workplace != null && character.IsCloseTo(blackboard.Workplace))
        {
            character.MakeInvisible();
            enteredWorkplace = true;
        }
    }

    public override void ManageTransitions()
    {
        if (vitals.IsHungerAboveThreshold)
        {
            var foodBuilding = blackboard.GetRandomFoodBuilding();
            if (foodBuilding != null)
            {
                character.MakeVisible();
                enteredWorkplace = false;
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
                enteredWorkplace = false;
                blackboard.TargetDestination = socialBuilding;
                blackboard.NextState = CharacterStateMachine.CharacterStateType.Socialize;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                return;
            }
        }

        if (vitals.IsSleepinessAboveThreshold)
        {
            if (blackboard.House != null)
            {
                character.MakeVisible();
                enteredWorkplace = false;
                blackboard.TargetDestination = blackboard.House;
                blackboard.NextState = CharacterStateMachine.CharacterStateType.Sleep;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                return;
            }
        }
    }
}
