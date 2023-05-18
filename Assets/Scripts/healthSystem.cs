using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float _maxHealth, _curruntHealth;
    private bool isDead = false;
    [SerializeField] private bool _haveEvent = false;

    [SerializeField] private UnityEvent _takeDamageEvent;
    [SerializeField] private UnityEvent _dieEvent;

    private void Awake()
    {
        _curruntHealth = _maxHealth;
    }
    public void TakeDamage(float damageTeken)
    {
        _curruntHealth -= damageTeken;
        if (_haveEvent) _takeDamageEvent.Invoke();
        Die();

    }
    //die function if the curruntHealth is 0 or less
    private void Die()
    {
        if(_curruntHealth <= 0)
        {
            //die code here
            isDead = true;
            if (_haveEvent) _dieEvent.Invoke();
        }
    }
    public bool GetIsDead() => isDead;
    public float GetMaxHealt() => _maxHealth;
    public float GetCurruntHealth() => _curruntHealth;
}
