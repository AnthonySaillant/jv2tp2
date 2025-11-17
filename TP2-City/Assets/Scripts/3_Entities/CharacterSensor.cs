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
        if (other.gameObject.CompareTag("Trash") && stateMachine.TrashBehaviour == CharacterStateMachine.CityCharacterTrashBehaviour.PickUp)
        {
            blackboard.LastSeenTrash = other.gameObject;
            Debug.Log("Poubelle");
        }
        if (other.isTrigger && other.gameObject.CompareTag("Character"))
        {
            Character character = other.GetComponent<Character>();
            foreach (var friend in blackboard.Friends)
            {
                if (friend == character)
                {
                    blackboard.LastSeenFriend = character;
                    Debug.Log("Ami");
                    break;
                }
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
