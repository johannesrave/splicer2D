using UnityEngine;

public class InitGameManager : MonoBehaviour
{
    public ScriptableObject _GM;
    [SerializeField]
    private ScriptableObject entityManager;

    public ScriptableObject EntityManager => entityManager;
}