using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraDialogueVisual : MonoBehaviour
{
    public float rotationSpeed;
    private bool isWatching;
    private Vector3 _target;
    private void LateUpdate()
    {
        if (isWatching)
        {
            Vector3 direction = _target -  transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }
    public void VisualFocus(Vector3 target)
    {
        _target = target;
        isWatching = true;
    }
    public void DeFocus()
    {
        isWatching = false;
    }
}
