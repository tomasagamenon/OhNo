using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public float damageMultiplier;
    private Life life;
    private void Awake()
    {
        life = GetComponentInParent<Life>();
    }
    public void DoDamage(float damage)
    {
        Debug.Log("BodyPart recibe un daño de " + damage);
        Debug.Log("Para un resultado de " + (damage * damageMultiplier));
        life.Damaged(damage * damageMultiplier);
    }
}
