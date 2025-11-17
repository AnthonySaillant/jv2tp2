using UnityEngine;

public abstract class CharacterBaseState : MonoBehaviour
{
    protected CharacterStateMachine stateMachine;
    protected Character character;
    protected CharacterVitals vitals;
    protected CharacterBlackboard blackboard;

    protected virtual void Awake()
    {
        stateMachine = GetComponent<CharacterStateMachine>();
        character = GetComponent<Character>();
        vitals = GetComponent<CharacterVitals>();
        blackboard = character.Blackboard;
    }

    protected virtual void Update()
    {
        Act();
        ManageTransitions();
    }

    public abstract void Act();

    public abstract void ManageTransitions();
}
