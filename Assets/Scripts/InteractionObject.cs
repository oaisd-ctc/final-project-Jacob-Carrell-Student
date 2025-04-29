using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private Text itemDisplay;
    [SerializeField] private string interactionText = "I am an interactable object!!!!";
    public GameObject itemItself;
    public GameObject trogArms;
    public GameObject pickArms;
    public GameObject spadeArms;

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

    public void Equip()
    {
        Destroy(itemItself);
        trogArms.SetActive(false);

        if (itemItself.tag == "Great Pick")
        {
            pickArms.SetActive(true);
            spadeArms.SetActive(false);

            weapon.ResetDurability;
        }

        if (itemItself.tag == "Great Spade")
        {
            spadeArms.SetActive(true);
            pickArms.SetActive(false);

            weapon.ResetDurability;
        }

    }
}

