using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private WeaponPosition weaponPosition;

    public WeaponInfo weaponInfo;

    public GameObject ammoContaner;

    public Transform RemoveAmmoCantenarePoint;

    // Start is called before the first frame update
    void Start()
    {
        weaponPosition = GetComponentInParent<WeaponPosition>();
        weaponPosition.SetWeaponValue(weaponInfo);

    }
    public void ApplyValues()
    {
        weaponPosition.SetWeaponValue(weaponInfo);
    }
    public void OnRemoveAmmoCantenare ()
    {
        Instantiate(ammoContaner, RemoveAmmoCantenarePoint);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
[System.Serializable]
public class WeaponInfo 
{
    //bullet
    public GameObject bullet;
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public GameObject muzzleFlash;

    //Reference
    public Transform attackPoint;

    //Recoil
    public Rigidbody weaponRb;
    public float recoilForce;

    public Animator animator;
}

