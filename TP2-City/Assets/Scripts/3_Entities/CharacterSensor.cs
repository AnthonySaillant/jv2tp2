using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class CharacterSensor : MonoBehaviour
{
    private CharacterBlackboard blackboard;
    private CharacterStateMachine stateMachine;

    void Start()
    {
        blackboard = GetComponent<CharacterBlackboard>();
        stateMachine = GetComponent<CharacterStateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trash") &&
            stateMachine.TrashBehaviour == CharacterStateMachine.CityCharacterTrashBehaviour.PickUp)
        {
            blackboard.LastSeenTrash = other.gameObject;
        }
        else if (other.gameObject.CompareTag("Character"))
        {
            Character friend = other.GetComponent<Character>();
            if (friend != null && blackboard.Friends.Contains(friend))
            {
                blackboard.LastSeenFriend = friend;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trash") && blackboard.LastSeenTrash == other.gameObject)
        {
            blackboard.LastSeenTrash = null;
        }

        if (other.isTrigger && other.gameObject.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
            if (blackboard.LastSeenFriend == character)
            {
                blackboard.LastSeenFriend = null;
            }
        }
    }
}
