using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Door : Interactable
{
    private bool isOpen = false;

    protected override void BaseInteract()
    {
        isOpen = !isOpen;
        GetComponent<Animator>().SetBool("IsOpen", isOpen);
    }
}
