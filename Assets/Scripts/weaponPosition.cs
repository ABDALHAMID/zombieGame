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

    //hand poses
    [SerializeField]
    private Transform weaponLeftHandPose, weaponRightHandPose, publicLeftHandPose, publicRightHandPose;

    private void Start()
    {
        
    }

    private void Update()
    {
        publicLeftHandPose.position = weaponLeftHandPose.position;
        publicRightHandPose.position = weaponRightHandPose.position;
        publicLeftHandPose.rotation = weaponLeftHandPose.rotation;
        publicRightHandPose.rotation = weaponRightHandPose.rotation;
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
        weaponLeftHandPose = GameObject.FindGameObjectWithTag("weaponLeftHandPose").transform;
        weaponRightHandPose = GameObject.FindGameObjectWithTag("weaponRightHandPose").transform;
        //PositionHands();
    }
    public void PositionHands()
    {
        publicLeftHandPose.position = weaponLeftHandPose.position;
        publicRightHandPose.position = weaponRightHandPose.position;
        publicLeftHandPose.rotation = weaponLeftHandPose.rotation;
        publicRightHandPose.rotation = weaponRightHandPose.rotation;
    }

}
