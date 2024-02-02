using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Transform cameraPosition, cameraTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartThisDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndThisDialogue();
        }
    }

    public void StartThisDialogue()
    {
        CameraBehavior.instance.FocusCamera(cameraTarget, cameraPosition.position);
    }

    public void EndThisDialogue()
    {
        CameraBehavior.instance.RestoreFollowPlayer();
    }
}
