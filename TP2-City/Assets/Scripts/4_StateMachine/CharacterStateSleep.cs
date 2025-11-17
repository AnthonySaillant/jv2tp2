using UnityEngine;

public class CharacterStateSleep : CharacterBaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.LowerSleepiness();
    }

    public override void ManageTransitions()
    {
        if (vitals.IsSleepinessAboveThreshold)
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
            else
            {
                blackboard.TargetDestination = blackboard.Workplace;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
            }
        }
    }
}