using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public float damageMultiplier;

    public void DoDamage(float damage)
    {
        Debug.Log("BodyPart recibe un daño de " + damage);
        Debug.Log("Para un resultado de " + (damage * damageMultiplier));
        gameObject.transform.parent.GetComponent<Life>().Damaged(damage * damageMultiplier);
    }
}
