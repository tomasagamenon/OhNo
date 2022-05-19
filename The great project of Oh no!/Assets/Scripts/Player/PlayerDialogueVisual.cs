using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueVisual : MonoBehaviour
{
    public float rotationSpeed;
    private bool isWatching;
    private Vector3 _target;
    private void Update()
    {
        if (isWatching)
        {
            Vector3 direction = _target - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public void VisualFocus(Vector3 target)
    {
        _target = target;
        isWatching = true;
        //GetComponentInChildren<PlayerCameraDialogueVisual>().VisualFocus(target);
    }
    public void DeFocus()
    {
        isWatching = false;
        //GetComponentInChildren<PlayerCameraDialogueVisual>().DeFocus();
    }
}
