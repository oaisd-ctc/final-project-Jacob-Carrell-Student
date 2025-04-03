using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private Text itemDisplay;
    [SerializeField] private string interactionText = "I am an interactable object!!!!";

    public UnityEvent OnInteract = new UnityEvent();

    private void OnEnable()
    {
        
    }

    public string GetInteractionText()
    {
        return interactionText;
    }

    public void Interact()
    {
        OnInteract.Invoke();
    }
}

