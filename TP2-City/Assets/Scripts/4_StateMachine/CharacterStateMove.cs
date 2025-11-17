using UnityEngine;

public class CharacterStateMove : CharacterBaseState
{

    public override void Act()
    {
        if (blackboard.TargetDestination != null)
        {
            character.NavigateTo(blackboard.TargetDestination);
        }
    }

    public override void ManageTransitions()
    {
        if (stateMachine.TrashBehaviour == CharacterStateMachine.CityCharacterTrashBehaviour.PickUp)
        {
            
        }
    }
}
