using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Tooltip("Total number of weapons that can be equipped"), SerializeField]
    const int weaponSlots = 2;
    // List of all weapons equipped by the player
    static GameObject[] weaponList;
    [Tooltip("Copy of weaponList to show in Inspector"), SerializeField]
    GameObject[] weaponListCopy;
    // Current weapon equipped
    static int weaponIndex = 0;

    [Header("Weapon Stats")]
    [Tooltip("Time between shots")]
    const float fireRateVal = 0.5f;
    float fireRate = fireRateVal;
    [Tooltip("Damage dealt per shot")]
    const int weaponDmg = 1;
    [Tooltip("Bullet speed")]
    const float bulletSpeed = 50.0f;
    [Tooltip("Bullet prefab"), SerializeField]
    GameObject bulletPrefab;
    [Tooltip("Total ammo capacity of weapon"), SerializeField]
    uint magazineCap = 30;
    [Tooltip("Amount of remaining ammo"), SerializeField]
    uint magazine;

    [Tooltip("Delay between swapping weapons")]
    const float scrollCoolDownVal = 0.2f;
    static float scrollCoolDown = scrollCoolDownVal;

    void Awake()
    {
        if (weaponList == null)
        {
            weaponList = new GameObject[2];
        }
        for (int i = 0; i < weaponList.Length; ++i)
        {
            if (weaponList[i] == null)
            {
                weaponList[i] = gameObject;
                //if (i == 0)
                //    gameObject.SetActive(false);
                break;
            }
        }

        magazine = magazineCap;
    }
    private void Start()
    {
        weaponListCopy = weaponList;
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1 && fireRate == 0 && magazine != 0)
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R) && magazine != magazineCap)
        {
            Reload();
        }
        {
            float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
            if (scrollAxis > 0)
            {
                SwapWeapons(1);
            }
            else if (scrollAxis < 0)
            {
                SwapWeapons(-1);
            }
        }

        // Cooldowns
        scrollCoolDown = scrollCoolDown <= 0 ? 0 : scrollCoolDown - Time.deltaTime;
        fireRate = fireRate <= 0 ? 0 : (fireRate - Time.deltaTime);
    }

    void Fire()
    {
        fireRate = fireRateVal;
        GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.forward * 0.5f), transform.rotation);
        bullet.GetComponent<Bullet>().bulletDmg = weaponDmg;
        bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        magazine = magazine == 0 ? 0 : (magazine - 1);
    }
    void Reload()
    {
        magazine = magazineCap;
    }
    void SwapWeapons(int moveDir)
    {
        scrollCoolDown = scrollCoolDownVal;
        weaponIndex = (weaponIndex + moveDir) % weaponList.Length;
        weaponList[weaponIndex].SetActive(true);
        gameObject.SetActive(false);
    }
}
