using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class weaponMnager : MonoBehaviour
{
    private int weaponIndex = 0;
    private WeaponState weaponState;
    private Animator anim;

    public int weapons;

    public Rig rightHandPoseStatic;
    public Rig rightHandPoseInMove;
    public Rig leftHandPose;

    [SerializeField] private CinemachineVirtualCamera playerAimCamera;
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    private ThirdPersonController thirdPersonController;
    [SerializeField] private Rig aimLayerRig;
       public MultiParentConstraint inMoveLayerRig;
    public float weaponPoseTime = 0.2f;


    Vector3 targetPoint;
    [SerializeField] private Transform aimPoint;
    private WeaponPosition weaponPosition;
    //input system  
    [SerializeField] private InputActionReference shoot, switchWeapon;
    //bullet 
    private GameObject bullet;

    //bullet force
    private float shootForce, upwardForce;

    //Gun stats
    private float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, bulletsLeft;
    private bool allowButtonHold;

    private int  bulletsShot;

    //Recoil
    private Rigidbody weaponRb;
    private float recoilForce;

    //bools
    private bool shooting, readyToShoot;
    public bool reloading;

    //Reference
    [SerializeField] private Camera fpsCam;
    private Transform attackPoint;

    //Graphics
    private GameObject muzzleFlash;
    //public TextMeshProUGUI ammunitionDisplay;

    //bug fixing :D
    private bool allowInvoke = true;
    private bool isAimming;

    private Animator weaponAnimator;

    private void Awake()
    {
        weaponIndex = 0;
        weaponState = GetComponentInChildren<WeaponState>();
        anim = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        input = GetComponent<StarterAssetsInputs>();
        weaponPosition = GetComponentInChildren<WeaponPosition>();
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
    }

    private void FixedUpdate()
    {

        //Find the exact hit position using a raycast
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = fpsCam.ScreenPointToRay(screenCenterPoint); //Just a ray through the middle of your current view

        //check if ray hits something
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(750); //Just a point far away from the player

        aimPoint.position = targetPoint;

        if (input.aim)
        {
            thirdPersonController.SetRotateOnMove(false);
            playerAimCamera.gameObject.SetActive(true);
            RototeToTarget();
            aimLayerRig.weight = Mathf.Lerp(aimLayerRig.weight, 1, weaponPoseTime);
            inMoveLayerRig.weight = Mathf.LerpAngle(inMoveLayerRig.weight, 0, weaponPoseTime);
            isAimming = true;
        }
        else
        {
            thirdPersonController.SetRotateOnMove(true);
            playerAimCamera.gameObject.SetActive(false);
            aimLayerRig.weight = Mathf.Lerp(aimLayerRig.weight, 0, weaponPoseTime);
            inMoveLayerRig.weight = Mathf.LerpAngle(inMoveLayerRig.weight, 1, weaponPoseTime);
            isAimming = false;
        }
        MyInput();


        weaponRb = weaponPosition.weaponRb;
        attackPoint = weaponPosition.attackPoint;
        muzzleFlash = weaponPosition.muzzleFlash;
        allowButtonHold = weaponPosition.allowButtonHold;
        bulletsPerTap = weaponPosition.bulletsPerTap;
        magazineSize = weaponPosition.magazineSize;
        bullet = weaponPosition.bullet;
        shootForce = weaponPosition.shootForce;
        upwardForce = weaponPosition.upwardForce;
        timeBetweenShooting = weaponPosition.timeBetweenShooting;
        spread = weaponPosition.spread;
        reloadTime = weaponPosition.reloadTime;
        timeBetweenShots = weaponPosition.timeBetweenShots;
        recoilForce = weaponPosition.recoilForce;
        weaponAnimator = weaponPosition.animator;
    }
    private void RototeToTarget()
    {
        Vector3 worldAimTarget = targetPoint;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 50f);
    }
    private void OnEnable()
    {
        shoot.action.performed += setShootTrue => { shooting = true;};
        shoot.action.canceled += setShootfalse => { shooting = false;};
        switchWeapon.action.performed += switchWeapons => { 
            if (switchWeapons.ReadValue<float>() > 0) 
            {
                weaponIndex++;
            }
            else if (switchWeapons.ReadValue<float>() < 0)
            {
                weaponIndex--;
            }
            if (weaponIndex >= weapons) weaponIndex = 0;
            if (weaponIndex <= -1) weaponIndex = weapons -1;
            weaponState.ChangeWeapon(weaponIndex);

        };

        }

    //Reloading 
        private void OnReload()
    {
        if (bulletsLeft < magazineSize && !reloading) Reload();
    }
    private void MyInput()
    {

        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && isAimming)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        if(!allowButtonHold) shooting = false;

        RototeToTarget();
        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        weaponAnimator.SetTrigger("shot");

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
        {
            GameObject currentMuzzleFlash = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            currentMuzzleFlash.transform.forward = directionWithSpread.normalized;
        }

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke(nameof(ResetShot),
                   timeBetweenShooting);
            allowInvoke = false;

        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke(nameof(Shoot),
                   timeBetweenShots);
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        anim.SetLayerWeight(1, 1);
        anim.SetTrigger("Reload");
        rightHandPoseStatic.weight = 0f;
        rightHandPoseInMove.weight = 1f;
        leftHandPose.weight = 0f;
    reloading = true;
        shooting = false;
        Invoke(nameof(ReloadFinished),reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    public void ReloadFinished()
    {
        anim.SetLayerWeight(1, 0);
        rightHandPoseStatic.weight = 1f;
        rightHandPoseInMove.weight = 0f;
        leftHandPose.weight = 1f;
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
