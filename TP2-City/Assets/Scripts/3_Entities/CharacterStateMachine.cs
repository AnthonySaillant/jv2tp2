using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public enum CharacterStateType
    {
        Move,
        Work,
        Eat,
        Socialize,
        Sleep,
        TakeTrash,
        ThrowTrash,
        Greet
    }

    public enum CityCharacterTrashBehaviour
    {
        Ignore,
        PickUp,
        Throw
    }

    [Header("Behaviour")]
    [SerializeField] private CityCharacterTrashBehaviour trashBehaviour = CityCharacterTrashBehaviour.Ignore;
    [SerializeField, Range(0, 100)] private float throwTrashChances = 5f;
    [SerializeField, Min(0)] private float throwTrashCheckDelay = 1f;

    private Character character;
    private CharacterBaseState currentState;
    private float throwTrashCheckTimer;

    public string CurrentStateName => currentState != null ? currentState.GetType().Name : "None";
    public CityCharacterTrashBehaviour TrashBehaviour => trashBehaviour;

    private void Awake()
    {
        character = GetComponent<Character>();
        throwTrashCheckTimer = 0f;
    }

    private void Update()
    {
        UpdateTimers();

        // Appeler Act et ManageTransitions de l'état courant
        if (currentState != null)
        {
            currentState.Act();
            currentState.ManageTransitions();
        }
    }

    private void UpdateTimers()
    {
        if (trashBehaviour == CityCharacterTrashBehaviour.Throw)
        {
            throwTrashCheckTimer += Time.deltaTime;
            if (throwTrashCheckTimer >= throwTrashCheckDelay)
            {
                character.Blackboard.ShouldThrowTrash = RandomUtils.Chance(throwTrashChances);
                throwTrashCheckTimer = 0f;
            }
        }
        else
        {
            throwTrashCheckTimer = 0f;
        }
    }

    // --- Méthode pour changer d'état ---
    public void ChangeCharacterState(CharacterStateType newStateType)
    {
        // Détruire ou désactiver l'état courant si nécessaire
        if (currentState != null)
        {
            Destroy(currentState);
            currentState = null;
        }

        // Créer un nouvel état en fonction de l'énumération
        switch (newStateType)
        {
            case CharacterStateType.Move:
                currentState = gameObject.AddComponent<CharacterStateMove>();
                break;
            case CharacterStateType.Work:
                currentState = gameObject.AddComponent<CharacterStateWork>();
                break;
            case CharacterStateType.Eat:
                currentState = gameObject.AddComponent<CharacterStateEat>();
                break;
            case CharacterStateType.Sleep:
                currentState = gameObject.AddComponent<CharacterStateSleep>();
                break;
            case CharacterStateType.Socialize:
                currentState = gameObject.AddComponent<CharacterStateSocialize>();
                break;
            case CharacterStateType.TakeTrash:
                currentState = gameObject.AddComponent<CharacterStateTakeTrash>();
                break;
            case CharacterStateType.ThrowTrash:
                currentState = gameObject.AddComponent<CharacterStateThrowTrash>();
                break;
            case CharacterStateType.Greet:
                currentState = gameObject.AddComponent<CharacterStateGreet>();
                break;
        }
    }
}
