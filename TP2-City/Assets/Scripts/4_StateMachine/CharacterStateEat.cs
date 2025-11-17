using UnityEngine;

public class CharacterStateEat : CharacterBaseState
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Act()
    {
        vitals.LowerHunger();
    }

    public override void ManageTransitions()
    {
        if (vitals.IsHungerAboveThreshold)
        {
            if (vitals.IsLonelinessBellowTarget)
            {
                var socialBuilding = blackboard.GetRandomSocialBuilding();
                if (socialBuilding != null)
                {
                    blackboard.TargetDestination = socialBuilding;
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
