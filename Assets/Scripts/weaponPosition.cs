using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPosition : MonoBehaviour
{
    public Camera fpsCam;
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

    private void Update()
    {
        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 lookDir = targetPoint - transform.position;
        Quaternion q = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime);
    }

    public void setWeaponValue(GameObject _bullet, float _shootForce, float _upwardForce, float _timeBetweenShooting, float _spread,
                                   float _reloadTime, float _timeBetweenShots, int _magazineSize, int _bulletsPerTap, bool _allowButtonHold,
                                   GameObject _muzzleFlash, Transform _attackPoint, Rigidbody _weaponRb, float _recoilForce)
    {
        weaponRb = _weaponRb;
        attackPoint = _attackPoint;
        muzzleFlash = _muzzleFlash;
        allowButtonHold = _allowButtonHold;
        bulletsPerTap = _bulletsPerTap;
        magazineSize = _magazineSize;
        bullet = _bullet;
        shootForce = _shootForce;
        upwardForce = _upwardForce;
        timeBetweenShooting = _timeBetweenShooting;
        spread = _spread;
        reloadTime = _reloadTime;
        timeBetweenShots = _timeBetweenShots;
        recoilForce = _recoilForce;
    }

}
