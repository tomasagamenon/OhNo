using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float rayDistance;
    public LayerMask layerMask;
    public string excludeLayerName = null; 
    public Camera mainCamera;

    private InteractiveObject _interactiveObject;

    public GameObject pressToInteract;

    private bool _uiActive;
    private bool _doOnce;
    private RaycastHit see;
    void Start()
    {

    }
    private void Awake()
    {
        if (pressToInteract == null) GameObject.FindGameObjectWithTag("InteractDisplay");
    }

    void Update()
    {
        //no estoy seguro de que hacer, consultar si seria factible guardar el objeto-que-se-ve y, 
        //en caso de que se vea otro, ahi reiniciar el objeto-que-se-ve al actual y algo asi?
        if(mainCamera.enabled == true)
        {
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, rayDistance, layerMask))
            {
                if (hit.collider.gameObject.GetComponent<InteractiveObject>().canInteract)
                {
                    //Debug.Log("estas viendo a un interactuable");
                    ChangeUI(true);
                    Debug.Log("se activa la UI y estas viendo a " + hit.collider.gameObject.name);

                    _uiActive = true;
                    _doOnce = true;
                    see = hit;
                }
                else ChangeUI(false);

            }
            else
            {
                if (_uiActive)
                {
                    //Debug.Log("se desactiva la UI");
                    ChangeUI(false);
                    _doOnce = false;
                }
            }
            if (Input.GetButtonDown("Interact") && _uiActive)
            {
                see.collider.gameObject.GetComponent<InteractiveObject>().Interact();
                ChangeUI(false);
                _doOnce = false;
                Debug.Log("interactuaste con " + see.collider.gameObject.name);
            }
        }
    }

    void ChangeUI(bool on)
    {
        if(on)
        {
            pressToInteract.SetActive(true);
        }
        else
        {
            pressToInteract.SetActive(false);
            _uiActive = false;
            _doOnce = false;
        }
    }

    
}
