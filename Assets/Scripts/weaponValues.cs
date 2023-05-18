using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponValues : MonoBehaviour
{
    private WeaponPosition weaponPosition;

    
    
    //bullet
    public GameObject bullet;
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public  int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public GameObject muzzleFlash;

    //Reference
    public Transform attackPoint;

    //Recoil
    public Rigidbody weaponRb;
    public float recoilForce;
    // Start is called before the first frame update
    void Start()
    {
        weaponPosition = GetComponentInParent<WeaponPosition>();
        //weaponPosition.SetWeaponValue(WeaponInfo weaponInfo);
    }
}
