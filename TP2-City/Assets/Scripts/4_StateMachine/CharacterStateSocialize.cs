using UnityEngine;

public class CharacterStateSocialize : CharacterBaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.LowerLoneliness();
    }

    public override void ManageTransitions()
    {
        if (vitals.IsLonelinessAboveThreshold)
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

            else if (vitals.IsSleepinessBellowTarget)
            {
                if (blackboard.House != null)
                {
                    blackboard.TargetDestination = blackboard.House;
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