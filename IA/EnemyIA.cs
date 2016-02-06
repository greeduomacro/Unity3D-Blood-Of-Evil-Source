//using UnityEngine;
//using System.Collections;

//public sealed class EnemyIA : MonoBehaviour {

//    [SerializeField]
//    private bool isStatic = false;
//    private bool isAtSpawnPosition;
//    private Vector3 staticPosition;
//    private Vector3 staticForward;
//    [SerializeField]
//    private bool onPatrol;
//    [SerializeField]
//    private Vector3[] path;
//    [SerializeField]
//    private int pathIndex = 0;

//    private float speed;
//    public float Speed
//    {
//        get { return speed; }
//        set
//        {
//            if (value < 0 || value < walkSpeed) speed = 0;
//            else if (value < runSpeed) speed = walkSpeed;
//            else speed = runSpeed;
//        }
//    }
//    [SerializeField]
//    private float walkSpeed;
//    [SerializeField]
//    private float runSpeed;
//    [SerializeField]
//    private Animator animator;
//    [SerializeField]
//    private float viewRange;
//    [SerializeField]
//    private float fireRange;
//    [SerializeField]
//    private float followRange;
//    [SerializeField]
//    private float fireDelay;
//    private float fireTimer;
//    private bool isIdling = false;
//    private float idleTime = 0;
//    [SerializeField]
//    private float idleDelay = 2;
//    [Range(0, 100), SerializeField]
//    private float idleChance;

//    [SerializeField]
//    private bool isFeared = false;
//    public bool IsFeared { get { return isFeared; } set { isFeared = value; fearTime = 0.0f; } }
//    float fearTime = 0.0f;
//    bool findFearPoint = false;

//    bool goBack;
//    Transform transf;
//    EnemyAttribute attributes = null;
//    Vector3 direction;
//    Vector3 fearDestination;

//    public Transform targetTransform = null;
//    CharacterController charaControl;

//    Transform shootPlaceHolder;

//    public GameObject bullet;

//    void Awake()
//    {
//        transf = transform;

//        if (onPatrol)
//            isStatic = false;

//        if (path.Length == 0)
//        {
//            isStatic = true;
//            onPatrol = false;
//        }

//        isAtSpawnPosition = false;
        
//        if (isStatic)
//        {
//            speed = 0;
//            idleChance = 100;
//            staticPosition = transf.position;
//            staticForward = transf.forward;
//            isAtSpawnPosition = true;
//            isIdling = true;
//        }


//        direction = path[pathIndex] - transf.position;
//        direction.Normalize();

//        //attributes = GetComponent<EnemyAttribute>();

//        fireTimer = fireDelay;
//        charaControl = GetComponent<CharacterController>();

//        FindShootPlaceHolder(transf);
//    }

//    bool FindShootPlaceHolder(Transform parentTransform)
//    {
//        foreach (Transform child in parentTransform)
//        {
//            if (child.tag == "ShootPlaceHolder")
//            {
//                shootPlaceHolder = child;
//                return true;
//            }
//            if (child.childCount > 0)
//            {
//                if (FindShootPlaceHolder(child))
//                    return true;
//            }
//        }

//        return false;
//    }

//    void Update()
//    {
//        if (attributes.Life.Current <= 0)
//        {
//            targetTransform = null;
//            return;
//        }

//        if (speed == runSpeed)
//        {
//            animator.SetBool("IsRunning", true);
//            animator.SetBool("IsWalking", false);
//            animator.SetBool("PrepareFire", false);
//            animator.SetBool("IsFiring", false);
//        }
//        else if (speed != 0)
//        {
//            animator.SetBool("IsRunning", false);
//            animator.SetBool("IsWalking", true);
//            animator.SetBool("PrepareFire", false);
//            animator.SetBool("IsFiring", false);
//        }
//        else
//        {
//            animator.SetBool("IsRunning", false);
//            animator.SetBool("IsWalking", false);
//        }

//        if (isFeared)
//            FearReaction();
//        else
//        {

//            if (!targetTransform || (targetTransform && Vector3.Distance(targetTransform.position, transf.position) > followRange))
//                DetectPlayer();
//            if (!isIdling)
//            {
//                Move();
//                Firing();
//            }
//            else
//                UpdateIdle();
//        }
//    }

//    void FearReaction()
//    {
//        speed = runSpeed;

//        if (fearTime == 0.0f)
//            direction = Vector3.zero;


//        while (direction == Vector3.zero || !findFearPoint)
//        {
//            direction = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
//            RaycastHit hit = new RaycastHit();

//            if (direction == Vector3.zero)
//                continue;

//            fearDestination = transf.position + direction;

//            direction.Normalize();

//            if (!Physics.Raycast(transf.position + Vector3.up, direction, out hit, Vector3.Distance(transf.position, fearDestination)))
//                findFearPoint = true;
//        }

//        transf.forward = fearDestination - transf.position;
//        charaControl.SimpleMove(direction * speed);

//        if (Vector3.Distance(transf.position, fearDestination) < 1.0f)
//        {
//            findFearPoint = false;
//            direction = Vector3.zero;
//        }

//        fearTime += Time.deltaTime;
//        if (fearTime >= 30)
//        {
//            fearTime = 0;
//            isFeared = false;
//            speed = walkSpeed;
//            findFearPoint = false;
//        }
//    }

//    void Firing()
//    {
//        if (!charaControl.isGrounded)
//            return;
//        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

//        if (targetTransform && Vector3.Distance(transf.position, targetTransform.position) <= fireRange)
//        {
//            animator.SetBool("PrepareFire", true);
//            speed = 0;
//        }
//        else if (targetTransform)
//        {
//            animator.SetBool("PrepareFire", false);
//            animator.SetBool("IsFiring", false);
//            speed = runSpeed;
//        }

//        if (targetTransform &&
//            Vector3.Distance(transf.position, targetTransform.position) <= fireRange &&
//            info.fullPathHash != Animator.StringToHash("Shot") &&
//            info.fullPathHash != Animator.StringToHash("Hit") &&
//            !animator.GetBool("IsHit") &&
//            fireTimer >= fireDelay)
//        {
//            animator.SetBool("IsFiring", true);
//            animator.SetTrigger("Shoot");
//            fireTimer = 0;
//            Shoot();
//        }
//        else
//            fireTimer += Time.deltaTime;

//        if (!targetTransform)
//        {
//            animator.SetBool("PrepareFire", false);
//            animator.SetBool("IsFiring", false);
//        }
//    }

//    void Shoot()
//    {
//        GameObject go = GameObject.Instantiate(bullet, shootPlaceHolder.position, Quaternion.identity) as GameObject;

//        go.transform.up = targetTransform.position - shootPlaceHolder.position;
//        go.transform.position += go.transform.up;
//        go.GetComponent<EnemyBullet>().Damage = this.attributes.Damage.RandomBetweenValues();
//    }

//    void Move()
//    {
//        if (isStatic && !isAtSpawnPosition && !targetTransform && Vector3.Distance(transf.position, staticPosition) < 1.0f)
//        {
//            transf.position = staticPosition;
//            transf.forward = staticForward;
//            idleTime = 0;
//            RandomizeIdle();
//            isIdling = true;
//            isAtSpawnPosition = true;
//        }

//        if (isStatic && isAtSpawnPosition)
//        {
//            idleTime += Time.deltaTime;
//            if (idleTime >= idleDelay)
//            {
//                idleTime = 0;
//                RandomizeIdle();
//                isIdling = true;
//            }
//        }

//        if (!targetTransform)
//        {
//            if (!isStatic)
//                direction = path[pathIndex] - (transf.position - Vector3.up * transf.position.y);
//            else
//                direction = staticPosition - (transf.position - Vector3.up * transf.position.y);
//        }
//        else
//            direction = targetTransform.position - Vector3.up * targetTransform.position.y - transf.position;

//        direction.Normalize();

//        if (direction != Vector3.zero)
//            transf.forward = direction;

//        if (!targetTransform && !isIdling && !isStatic &&
//            (Vector3.Distance(transf.position, path[pathIndex]) < 0.5f || transf.position == path[pathIndex]))
//        {
//            if (pathIndex + 1 == path.Length)
//                goBack = true;

//            ChangeIndex();



//            if (pathIndex < 0)
//            {
//                goBack = false;
//                pathIndex = 0;
//            }

//            RandomizeIdle();
//        }

//        if (targetTransform && Vector3.Distance(transf.position, targetTransform.position) > fireRange)
//            charaControl.SimpleMove(direction * speed);
//        else if (!targetTransform)
//            charaControl.SimpleMove(direction * speed);
//    }

//    void UpdateIdle()
//    {
//        if (idleTime >= idleDelay)
//        {
//            isIdling = false;
//            idleTime = 0;
//        }
//        else
//            idleTime += Time.deltaTime;

//        speed = 0;
//    }

//    void RandomizeIdle()
//    {
//        isIdling = Random.Range(0, 100) < idleChance;

//        if (!isIdling)
//            return;

//        int idleNum = Random.Range(1, 4);

//        animator.SetTrigger("Idle" + idleNum);
//        speed = 0;
//    }

//    void DetectPlayer()
//    {
//        targetTransform = null;
//        Collider[] colliders = Physics.OverlapSphere(transf.position, viewRange);

//        foreach (var collider in colliders)
//        {
//            if (Vector3.Angle(transf.forward, collider.transform.position - transf.position) < 90 && collider.transform.root.tag == "Player")
//            {
//                RaycastHit hit = new RaycastHit();

//                if (Physics.Raycast(transf.position, collider.transform.position - transf.position, out hit) &&
//                    hit.collider.transform.root.tag == "Player")
//                {
//                    targetTransform = hit.collider.transform.root;
//                    targetTransform = targetTransform.FindChild("Body");
//                    speed = runSpeed;
//                    isAtSpawnPosition = false;
//                    isIdling = false;
//                    break;
//                }
//            }
//        }

//        if (!targetTransform && !isIdling && !isAtSpawnPosition)
//            speed = walkSpeed;
//    }

//    void ChangeIndex()
//    {
//        if (goBack && !onPatrol)
//            pathIndex--;
//        else if (pathIndex < path.Length - 1)
//            pathIndex++;
//        else if (pathIndex == path.Length - 1 && onPatrol)
//            pathIndex = 0;
//    }
//}

////enum e_EnemyMove
////    {
////        IN_CIRCLE,
////        IN_SQUARE,
////        TARGET,
////        BACK_TO_DESTINATION
////    }

////    private GameObject			IAPoint;
////    private Transform			IAPointTrans;
////    private Transform			_Trans;
////    private Vector3				_InitPos;

////    private e_EnemyMove			_EWhereToGo	= e_EnemyMove.IN_CIRCLE;

////    public float		range		= 10.0f;
////    public float		moveSpeed	= 5.0f;
////    public float		idleTime	= 1.6f;
////    public float		idleChance	= 25.00f;

////    private GameObject	_Target;
////    private Transform	TargetTrans;
////    public float		followRange = 20.0f;
////    public float		rangeToBackToDestination = 50.0f;
////    private EnemyAttribute enemyAttribute;

////    void Start()
////    {
////        IAPoint		= null;
////        _Trans		= transform;
////        enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
////        _InitPos	= _Trans.position;

////        //StartCoroutine("MoveOnPath");
////        StartCoroutine("FindNearestGameObjectWithTag", "Player");
////    }

////    void	Update()
////    {
////        /*if		(Vector3.Distance(_Trans.position, _InitPos) > rangeToBackToDestination)
////            _EWhereToGo = e_EnemyMove.BACK_TO_DESTINATION;
////        else if (null != _Target && Vector3.Distance(_Trans.position, TargetTrans.position) < followRange)
////            _EWhereToGo = e_EnemyMove.TARGET;
////        if		((_EWhereToGo == e_EnemyMove.BACK_TO_DESTINATION && _Trans.position == _InitPos))
////        {
////            if (Random.Range(0, 2 + 1) % 2 == 0)	_EWhereToGo = e_EnemyMove.IN_CIRCLE;
////            else									_EWhereToGo = e_EnemyMove.IN_SQUARE;
////        }
////        */
////    }

////    IEnumerator		FindNearestGameObjectWithTag(string tag)
////    {
////        do		
////        {
////            _Target = GameObjectExtension.FindNearestGameObjectWithTag(_Trans.position, tag);
////            if (null != _Target)
////                TargetTrans = _Target.transform;

////            yield return new WaitForSeconds(0.1f);
////        }
////        while (true);
////    }

////    IEnumerator MoveOnPath()
////    {
////        do
////        {
////            if		(_EWhereToGo == e_EnemyMove.IN_CIRCLE)
////                yield return StartCoroutine(MoveToDestination(_InitPos + new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * range));
////            if (_EWhereToGo == e_EnemyMove.IN_SQUARE)
////                yield return StartCoroutine(MoveToDestination(_InitPos + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)) * range));
////            else if (_EWhereToGo == e_EnemyMove.TARGET)
////                yield return StartCoroutine(MoveToDestination(TargetTrans.position));
////            else if (_EWhereToGo == e_EnemyMove.BACK_TO_DESTINATION)
////                yield return StartCoroutine(MoveToDestination(_InitPos));
////        }

////        while (true);
////    }

////    IEnumerator MoveToDestination(Vector3 target)
////    {
////        if (_EWhereToGo != e_EnemyMove.TARGET)
////        {
////            if (_EWhereToGo == e_EnemyMove.IN_CIRCLE || _EWhereToGo == e_EnemyMove.IN_SQUARE)
////                CreateIAPoint(target);

////            while (Vector3.Distance(transform.position, target) > 2)
////            {
////                if (_EWhereToGo == e_EnemyMove.IN_CIRCLE || _EWhereToGo == e_EnemyMove.IN_SQUARE)
////                    target = IAPointTrans.position;

////                if (_EWhereToGo != e_EnemyMove.BACK_TO_DESTINATION && Vector3.Distance(_Trans.position, target) > rangeToBackToDestination)
////                {
////                    CreateIAPoint(target);
////                    target = IAPointTrans.position;
////                }	
				
////                yield return 0;
////            }

////            if (Random.Range(0, 100) <= idleChance) yield return new WaitForSeconds(idleTime);
////        }
////        else
////        {
////            if (Vector3.Distance(_Trans.position, target) < enemyAttribute.attributes[(int)(e_entityAttribute.Range)] &&
////                enemyAttribute.attackSpeed.CanDoMyAction())
////                AttackPlayer();
////        }
////        yield return null;
////    }

////    void CreateIAPoint(Vector3 target)
////    {
////        if (null != IAPoint)
////            Destroy(IAPoint);
////        IAPoint = base.ModuleManager.ServiceLocator.ObjectManager.Instantiate("IAPoint", target + Vector3.up * 2, Quaternion.identity);
////        IAPointTrans = IAPoint.transform;
////    }

////    IEnumerator MoveToIAPoint(Vector3 target)
////    {
////        CreateIAPoint(target);
////        while (Vector3.Distance(transform.position, IAPointTrans.position) > 2)
////        {
////            Debug.Log(Vector3.Distance(transform.position, IAPointTrans.position) > range + 2f);
////            //while (Vector3.Distance(transform.position, IAPointTrans.position) > range + 2f)
////            //	CreateIAPoint(target);

////            yield return null;
////        }

////        if (Random.Range(0, 100) <= idleChance) 
////            yield return new WaitForSeconds(idleTime);
////    }

////    IEnumerator MoveSetDestination(Vector3 target)
////    {
////        //if (Time.timeScale != 0)
////        //{
////            Debug.Log("fuck yiou");
////        //}

////        yield return 1;
////    }
	
////    IEnumerator MoveToPlayer(Vector3 target)
////    {
////        Vector3 destination = target - transform.forward * enemyAttribute.attributes[(int)(e_entityAttribute.Range)];

////        if (Vector3.Distance(_Trans.position, target) < 3f && enemyAttribute.attackSpeed.CanDoMyAction())
////            AttackPlayer();
		
////        yield return null;
////    }

////    IEnumerator MoveToPosition(Vector3 target)
////    {
////        while (Vector3.Distance(transform.position, target) > 2)
////            yield return MoveSetDestination(target);
		
////        yield return null;
////    }

////    void AttackPlayer()
////    {
////        PlayerAttribute playerAttribute = _Target.GetComponent<PlayerAttribute>();
////        float trueDamage = Random.Range(enemyAttribute.damage.min, enemyAttribute.damage.max + 1) - playerAttribute.defence * 0.15f;

////        if (trueDamage > 0)
////            playerAttribute.life.min -= Random.Range(enemyAttribute.damage.min, enemyAttribute.damage.max + 1);
////    }