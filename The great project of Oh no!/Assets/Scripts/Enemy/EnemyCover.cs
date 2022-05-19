using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCover : MonoBehaviour
{
    private Enemy core;
    public LayerMask layer;
    private Vector3 enemyHeight = new Vector3 (0, 2f, 0);
    public Transform goingTo;
    private void Awake()
    {
        core = GetComponent<Enemy>();
    }
    private void OnEnable()
    {
        Debug.Log("EnemyCover activado");
        goingTo = null;
        SearchCover();
    }
    private void Update()
    {
        if (goingTo != null)
            core.nma.SetDestination(goingTo.position);
        if(core.nma.remainingDistance < 0.3f)
            core.Shoot();
    }
    private void SearchCover()
    {
        Collider[] coversColliders = Physics.OverlapSphere(transform.position, core.coverRange, core.coverLayer);
        List<Transform> coversTransform = new List<Transform>();
        if (coversColliders[0] != null)
        {
            for(int i = 0; i < coversColliders.Length; i++)
            {
                if (!coversColliders[0].GetComponent<Cover>().isFull)
                {
                    coversTransform.Add(coversColliders[i].transform);
                    break;
                }
            }
            Transform bestCover = coversTransform[0];
            float modifier = Calculate(bestCover.GetComponent<Cover>());
            float bestCoverPoints = Vector3.Distance(transform.position, bestCover.position) * modifier;
            for (int i = 1; i < coversTransform.Count; i++)
            {
                Transform actualCover = coversTransform[i];
                if (!actualCover.GetComponent<Cover>().isFull)
                {
                    modifier = Calculate(actualCover.GetComponent<Cover>());
                    float actualCoverPoints = Vector3.Distance(transform.position, actualCover.position) * modifier;
                    if (actualCoverPoints < bestCoverPoints)
                    {
                        bestCover = actualCover;
                        bestCoverPoints = actualCoverPoints;
                    }
                }
            }
            Opose(bestCover.GetComponent<Cover>());
        }
        else
        {
            core.Decide();
        }
    }
    private void Opose(Cover cover)
    {
        Vector3 direction = new Vector3();
        List<Transform> posibilities = new List<Transform>();
        List<Transform> finals = new List<Transform>();
        for (int i = 0; i < cover.positions.Count; i++)
        {
            Transform _pos = cover.positions[i];
            direction = core.player.position - _pos.position - enemyHeight;
            if (!cover.isTaken[i] && Physics.Raycast(_pos.position + enemyHeight, direction, out RaycastHit hit, layer))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    posibilities.Add(_pos);
                }
            }
        }
        
        if (posibilities.Count != 0)
        {
            foreach (Transform _posibilities in posibilities)
            {
                direction = core.player.position - _posibilities.position;
                if (Physics.Raycast(_posibilities.position, direction, out RaycastHit _hit, core.coverLayer))
                {
                    if (_hit.collider.gameObject.CompareTag("Cover"))
                    {
                        finals.Add(_posibilities);
                    }
                }
            }
            if (finals.Count > 0)
            {
                if (finals.Count == 1)
                {
                    goingTo = finals[0];
                }
                else if (finals.Count > 1)
                {
                    goingTo = finals[Random.Range(0, finals.Count)];
                }
                goingTo.parent.GetComponent<Cover>().CoverTaken(goingTo, true);
                core.nma.SetDestination(goingTo.position);
            }
        }
    }
    private float Calculate(Cover cover)
    {
        float mod;
        if(cover.coverQuality == Cover.CoverQuality.Low)
        {
            mod = 1.25f;
        } else if(cover.coverQuality == Cover.CoverQuality.Medium)
        {
            mod = 1;
        } else
        {
            mod = 0.75f;
        }
        return(mod);
    }

    public void GetOff()
    {
        if (goingTo != null)
            goingTo.parent.GetComponent<Cover>().CoverTaken(goingTo, false);
    }

    private void OnDisable()
    {
        GetOff();
    }
}
