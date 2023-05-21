using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseEnemyControler : MonoBehaviour
{

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _screamDetectionRadius = 10f;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private UnityEvent _onDetectPlayerEvent;
    private EnemyAnimationControler animationConroler;

    private bool _isScreaming;

    public GameObject ui;
    public bool playerDetected { get; private set; }
    private bool seePlayer;
    [SerializeField] private int _numberOfYRaycasts, numberOfXRaycasts, raycastYAngle, raycastXAngle;
    [SerializeField] private float raycastLength;
    [SerializeField] private Transform scherchPoint;
    private GameObject player;

    [SerializeField] private float _chasingSpeed;

    //Patroling
    private Vector3 walkPoint;
    private bool walkPointSet = false;
    [SerializeField] public float _walkPointRange = 12f;
    [SerializeField] public float _walkingSpeed = 1f, timeToStopChasing;

    public bool isAttacking { get; set; }

    //idel
    private bool isIdel;
    private bool chasing;
    private bool hitting;


    //UI
    public HealthSystem HealtSystem;
    public Slider _healtBar;
    public Gradient _healtBarColorGradient;
    private float maxHealt, currentHealt;
    public RawImage _healtBarImage;
    public Transform Ui;
    private void Start()
    {
        _isScreaming = false;
        player = GameObject.FindGameObjectWithTag("Player");
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        animationConroler = GetComponent<EnemyAnimationControler>();
        HealtSystem = GetComponent<HealthSystem>();
        Patroling();
    }


    
   

    private void Update()
    {

        maxHealt = HealtSystem.GetMaxHealt();
        currentHealt = HealtSystem.GetCurruntHealth();
        _healtBar.maxValue = maxHealt;
        _healtBar.value = currentHealt;
        _healtBarImage.color = _healtBarColorGradient.Evaluate(_healtBar.normalizedValue);
        Ui.LookAt(player.transform.position + new Vector3(0, 1, 0));
        // If the enemy not chasing the player
        if (!playerDetected)
        {

            //check if the enemey see the player
            for (int j = 0; j < _numberOfYRaycasts; j++)
            {
                for (int i = 0; i < numberOfXRaycasts; i++)
                {
                    // Create a ray in the forward direction of the zombie
                    Vector3 raycastDirection = scherchPoint.forward;
                    // Rotate the raycast direction based on the angle and number of raycasts
                    raycastDirection = Quaternion.AngleAxis((i - numberOfXRaycasts / 2) * raycastYAngle, scherchPoint.up) * raycastDirection;
                    raycastDirection = Quaternion.AngleAxis((j - _numberOfYRaycasts / 2) * raycastXAngle, scherchPoint.right) * raycastDirection;
                    Ray ray = new Ray(scherchPoint.position, raycastDirection);

                    // Perform the raycast
                    if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
                    {
                        // If the raycast hits the player, start navigating to the player
                        if (hit.collider != null && hit.collider.CompareTag("Player"))
                        {
                            playerDetected = true;
                            DetectPlayer();
                            CallEnemys();
                        }
                    }
                }
            }
        }
        if(chasing) _navMeshAgent.SetDestination(player.transform.position);

            //Walkpoint reached
        if (Vector3.Distance(transform.position, walkPoint) <= _navMeshAgent.stoppingDistance && walkPointSet && !isIdel)
        {
            _navMeshAgent.SetDestination(transform.position);
            walkPointSet = false;
            BeIdel();
        }

    }

    private void Patroling()
    {
        if (chasing)
            return;
        if (!walkPointSet) BeIdel();
        else
        {
            _navMeshAgent.SetDestination(walkPoint);
            isIdel = false;
            _navMeshAgent.speed = _walkingSpeed;
            animationConroler.PlayPatrolAnimation();

        }
    }
    private void BeIdel()
    {
        if (chasing)
            return;
        if (!isIdel)
        {
            isIdel = true;
            animationConroler.PlayIdleAnimation();
            Invoke(nameof(SearchWalkPoint), Random.Range(3f, 10f));
        }
    }
    private void SearchWalkPoint()
    {
        if (chasing || hitting)
            return;
        walkPoint = new Vector3(transform.position.x + Random.Range(-_walkPointRange, _walkPointRange),
                                transform.position.y,
                                transform.position.z + Random.Range(-_walkPointRange, _walkPointRange));
        if (NavMesh.SamplePosition(walkPoint, out NavMeshHit hit, _walkPointRange, _navMeshAgent.areaMask))
        {
            walkPoint = hit.position;
            walkPointSet = true;
            Patroling();
        }
        else
        {
            SearchWalkPoint();
        }
    }

    /// <summary>
    /// Start chasing the player
    /// </summary>
    public void DetectPlayer()
    {
        // Play scream animation
        animationConroler.PlayScreamAnimation();
        _isScreaming = true;
        _navMeshAgent.SetDestination(transform.position);
    }

    public void StartChasingThePlayer()
    {
        chasing = true;
        _isScreaming = false;
        _navMeshAgent.speed = _chasingSpeed;
        animationConroler.PlayChaseAnimation();
    }

    public void CallEnemys()
    {
        // Inform nearby zombies to detect player
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, _screamDetectionRadius);
        foreach (Collider nearbyCollider in nearbyColliders)
        {
            BaseEnemyControler nearbyEnemy = nearbyCollider.GetComponentInParent<BaseEnemyControler>();
            if (nearbyEnemy != null && nearbyEnemy != this)
            {
                if (Vector3.Distance(nearbyEnemy.transform.position, player.transform.position) <= 50)
                    nearbyEnemy.Invoke(nameof(DetectPlayer), 1f);
            }
        }
    }
    public void OnAttackAnimationStart()
    {
        _navMeshAgent.SetDestination(transform.position);
        isAttacking = true;
        chasing = false;
    }
    public void OnAttackAnimationEnd()
    {
        StartChasingThePlayer();
        isAttacking = false;
    }

    public void OnHit()
    {
        if (isAttacking || _isScreaming)
            return;
        _navMeshAgent.speed = 0;
        hitting = true;
        animationConroler.PlayHitAnimation();
        chasing = false;
        StartCoroutine(ReturnFromHit());
    }
    IEnumerator ReturnFromHit()
    {
        yield return new WaitForSeconds(1);
        float randomState = Random.Range(0, 10);
        hitting = false;
        if (randomState <= 7)
            StartChasingThePlayer();
        else
            DetectPlayer();


    }
    public void OnDie()
    {
        _navMeshAgent.isStopped = true;
        Destroy(_navMeshAgent);
        ui.SetActive(false);
        Ui.gameObject.SetActive(false);
        Destroy(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _screamDetectionRadius);
        Gizmos.color = Color.blue;
        for (int j = 0; j < _numberOfYRaycasts; j++)
        {
            for (int i = 0; i < numberOfXRaycasts; i++)
            {
                // Create a ray in the forward direction of the zombie
                Vector3 raycastDirection = scherchPoint.forward;
                // Rotate the raycast direction based on the angle and number of raycasts
                raycastDirection = Quaternion.AngleAxis((i - numberOfXRaycasts / 2) * raycastYAngle, scherchPoint.up) * raycastDirection;
                raycastDirection = Quaternion.AngleAxis((j - _numberOfYRaycasts / 2) * raycastXAngle, scherchPoint.right) * raycastDirection;
                Ray ray = new Ray(scherchPoint.position, raycastDirection);

                Gizmos.DrawRay(scherchPoint.position, raycastDirection);
            }
        }
    }
}
