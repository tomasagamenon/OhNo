using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Item
{
    private Transform camera;

    public float range;
    public float damage;
    public float cooldown;
    private float _cooldown;

    public int maxMagazineAmmo;
    private int _actualMagazineAmmo;

    public float reloadCooldown;
    private float _reloadCooldown;

    private bool _isReloading;

    public GameObject[] bulletHits;

    public LayerMask layers;

    private void Start()
    {
        _actualMagazineAmmo = maxMagazineAmmo;
    }
    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _reloadCooldown = reloadCooldown;
        _cooldown = cooldown;
    }

    public override void Action()
    {
        base.Action();
        _cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && _cooldown <= 0 && !_isReloading)
        {
            if (_actualMagazineAmmo != 0)
            {
                Shoot();
                _actualMagazineAmmo--;
            }
            else
            {
                Reload();
            }

        }
        if(Input.GetButtonDown("Reload") && _actualMagazineAmmo != maxMagazineAmmo)
        {
            Reload();
        }

        if (_isReloading)
        {
            _reloadCooldown -= Time.deltaTime;
        }

        if (_reloadCooldown <= 0)
        {
            _actualMagazineAmmo = maxMagazineAmmo;
            _isReloading = false;
            _reloadCooldown = reloadCooldown;
            Debug.Log("Terminaste de recargar");
        }
    }

    void Shoot()
    {
        Debug.Log("Disparaste");
        bool hitted = Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, range, layers);
        if (hitted)
        {

            Debug.DrawRay(camera.position, camera.forward, Color.red, 2f);
            Debug.Log("Golpeaste");
            //Debug.Log("disparaste a " + hit.collider.gameObject.name + " en " + hit.point + " a una distancia de " + hit.distance);
            if (hit.collider.GetComponent<BodyPart>())
            {
                Debug.Log("Daniaste a un enemigo");
                Debug.DrawRay(camera.position, camera.forward, Color.green, 2f);
                hit.collider.GetComponent<BodyPart>().DoDamage(damage);
            }
            /*
            if (hit.collider.CompareTag("Concrete"))
            {
                Instantiate(bulletHits[0], hit.point, Quaternion.LookRotation(hit.normal));
            }else if (hit.collider.CompareTag("Enemy"))
            {
                Instantiate(bulletHits[1], hit.point, Quaternion.LookRotation(hit.normal));

                hit.collider.GetComponent<Life>().Damaged(damage);
            }else if (hit.collider.CompareTag("Metal"))
            {
                Instantiate(bulletHits[2], hit.point, Quaternion.LookRotation(hit.normal));
            }else if (hit.collider.CompareTag("Wood"))
            {
                Instantiate(bulletHits[3], hit.point, Quaternion.LookRotation(hit.normal));
            }
            */
        }
        _cooldown = cooldown;
    }

    void Reload()
    {
        if (!_isReloading)
        {
            _reloadCooldown = reloadCooldown;
            _isReloading = true;
            Debug.Log("Empezaste a recargar");
        }
    }
}
