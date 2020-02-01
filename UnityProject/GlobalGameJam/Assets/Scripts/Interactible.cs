using UnityEngine;

public abstract class Interactible : MonoBehaviour, IInteractible
{
    [SerializeField] private float _interactibleRange = 0f;
    public float InteractionRange { get => _interactibleRange; set => _interactibleRange = value; }

    public abstract void Interact();
}
