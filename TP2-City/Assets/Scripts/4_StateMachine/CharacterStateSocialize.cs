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
        if (vitals.IsLonelinessBellowTarget)
        {
            if (!vitals.IsHungerBellowTarget)
            {
                var foodBuilding = blackboard.GetRandomFoodBuilding();
                if (foodBuilding != null)
                {
                    blackboard.TargetDestination = foodBuilding;
                    blackboard.NextState = CharacterStateMachine.CharacterStateType.Eat;
                    stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                    return;
                }
            }

            if (!vitals.IsSleepinessBellowTarget)
            {
                if (blackboard.House != null)
                {
                    blackboard.TargetDestination = blackboard.House;
                    blackboard.NextState = CharacterStateMachine.CharacterStateType.Sleep;
                    stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
                    return;
                }
            }

            if (blackboard.Workplace != null)
            {
                blackboard.TargetDestination = blackboard.Workplace;
                blackboard.NextState = CharacterStateMachine.CharacterStateType.Work;
                stateMachine.ChangeCharacterState(CharacterStateMachine.CharacterStateType.Move);
            }
        }
    }
}
