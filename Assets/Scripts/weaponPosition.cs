using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponPosition : MonoBehaviour
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

    public Animator animator;


    //hand poses
    [SerializeField]
    private Transform weaponLeftHandPose, weaponRightHandPose, weaponGrapPose, publicLeftHandPose, publicRightHandPose, publicGrapPose;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        PositionHands();
    }

    public void SetWeaponValue(WeaponInfo weaponInfo)
    {
        weaponRb = weaponInfo.weaponRb;
        attackPoint = weaponInfo.attackPoint;
        muzzleFlash = weaponInfo.muzzleFlash;
        allowButtonHold = weaponInfo.allowButtonHold;
        bulletsPerTap = weaponInfo.bulletsPerTap;
        magazineSize = weaponInfo.magazineSize;
        bullet = weaponInfo.bullet;
        shootForce = weaponInfo.shootForce;
        upwardForce = weaponInfo.upwardForce;
        timeBetweenShooting = weaponInfo.timeBetweenShooting;
        spread = weaponInfo.spread;
        reloadTime = weaponInfo.reloadTime;
        timeBetweenShots = weaponInfo.timeBetweenShots;
        recoilForce = weaponInfo.recoilForce;
        weaponLeftHandPose = GameObject.FindGameObjectWithTag("weaponLeftHandPose").transform;
        weaponRightHandPose = GameObject.FindGameObjectWithTag("weaponRightHandPose").transform;
        weaponGrapPose = GameObject.FindGameObjectWithTag("grapPose").transform;
        animator = weaponInfo.animator;
        //PositionHands();
    }
    public void PositionHands()
    {
        publicLeftHandPose.position = weaponLeftHandPose.position;
        publicRightHandPose.position = weaponRightHandPose.position;
        publicLeftHandPose.rotation = weaponLeftHandPose.rotation;
        publicRightHandPose.rotation = weaponRightHandPose.rotation;
        Vector3 offcet = weaponGrapPose.position;
        weaponGrapPose.position = publicGrapPose.position;
        weaponGrapPose.rotation = publicGrapPose.rotation;

    }

}
