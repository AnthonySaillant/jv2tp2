using UnityEngine;

public class CharacterStateWork : CharacterBaseState
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
    }

    public override void ManageTransitions()
    {
        if (vitals.IsHungerBellowTarget)
        {
            var foodBuilding = blackboard.GetRandomFoodBuilding();
            if (foodBuilding != null)
            {
                blackboard.TargetDestination = foodBuilding;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
            }
        }

        if (vitals.IsLonelinessBellowTarget)
        {
            var socialBuilding = blackboard.GetRandomSocialBuilding();
            if (socialBuilding != null)
            {
                blackboard.TargetDestination = socialBuilding;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
            }
        }

        if (vitals.IsSleepinessBellowTarget)
        {
            if (blackboard.House != null)
            {
                blackboard.TargetDestination = blackboard.House;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
            }
        }
    }
}
