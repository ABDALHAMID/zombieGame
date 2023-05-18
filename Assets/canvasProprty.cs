using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using UnityEngine.InputSystem;


public class canvasProprty : MonoBehaviour
{
    public HealthSystem _playerHealtSystem;
    public weaponMnager _playerWeaponManager;
    public Slider _healtBar;
    public TextMeshProUGUI _healtPersontage, _amo;
    public RawImage _healtBarImage;
    public Gradient _healtBarColorGradient;
    public GameObject _reloading, _miniMap;
    public Camera _miniMapCamera;
    public float _time, _miniMapMinSize, _miniMapMaxSize, _cameraMinSize, _cameraMaxSize;
    public StarterAssetsInputs _InputSystem;
    

    private float maxHealt, currentHealt, bulletsLeft, bulletsPerTap, magazineSize;

    private void Update()
    {
        
        maxHealt = _playerHealtSystem.GetMaxHealt();
        currentHealt = _playerHealtSystem.GetCurruntHealth();
        bulletsLeft = _playerWeaponManager.bulletsLeft;
        bulletsPerTap = _playerWeaponManager.bulletsPerTap;
        magazineSize = _playerWeaponManager.magazineSize;
        _amo.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        float healtPercet = currentHealt * 100 / maxHealt;
        _healtPersontage.SetText(healtPercet + "%");
        if (_playerWeaponManager.reloading) _reloading.SetActive(true);
        else _reloading.SetActive(false);
        _healtBar.maxValue = maxHealt;
        _healtBar.value = currentHealt;
        _healtBarImage.color = _healtBarColorGradient.Evaluate(_healtBar.normalizedValue);
        if (_InputSystem.Map)
        {
            _miniMap.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(_miniMap.GetComponent<RectTransform>().rect.width, _miniMapMaxSize, _time * Time.deltaTime), Mathf.Lerp(_miniMap.GetComponent<RectTransform>().rect.height, _miniMapMaxSize, _time * Time.deltaTime));
            _miniMapCamera.orthographicSize = Mathf.Lerp(_miniMapCamera.orthographicSize, _cameraMaxSize, _time * Time.deltaTime);
        }
        else
        {
            
            _miniMap.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(_miniMap.GetComponent<RectTransform>().rect.width, _miniMapMinSize, _time * Time.deltaTime), Mathf.Lerp(_miniMap.GetComponent<RectTransform>().rect.height, _miniMapMinSize, _time * Time.deltaTime));
            _miniMapCamera.orthographicSize = Mathf.Lerp(_miniMapCamera.orthographicSize, _cameraMinSize, _time * Time.deltaTime);
            
        }

    }

}
