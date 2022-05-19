using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float maxLife;
    private float _actualLife;

    public GameObject lifeBar;

    private void Awake()
    {
        _actualLife = maxLife;
    }

    void Update()
    {
        if(_actualLife <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                //lifeBar.GetComponentInParent<Menu>().SendMessageUpwards("Defeat");
            }
            else 
            {
                //Destroy(gameObject);
                Debug.Log("Mori weon");
            }
        }
    }

    public void Damaged(float damage)
    {
        _actualLife -= damage;
        if (gameObject.CompareTag("Player"))
        {
            //lifeBar.GetComponent<HealthBar>().playerLife = _actualLife;
            Debug.Log("Tu vida actual es= " + _actualLife);
        }

        if (gameObject.CompareTag("Enemy"))
        {
            if(_actualLife <= 25)
                SendMessage("ReadyToFlee");
            Debug.Log("La vida del enemigo es= " + _actualLife);
        }
    }
}
