using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public class PlayerController : MonoBehaviour
//{
//    public PlayerAttribute playerAttribute;
//    public ItemManager itemMgr;
//    public ASkillManager skilMgr;
//    public PlayerControllerGUI gui;
//    public PlayerControllerGUIUpdater GUIUpdater;

//    Inventory playerInventory;

//    #region PlayerControl
//    [SerializeField]
//    private KeyCode forward;
//    [SerializeField]
//    private KeyCode backward;
//    [SerializeField]
//    private KeyCode left;
//    [SerializeField]
//    private KeyCode right;
//    [SerializeField]
//    private KeyCode crouch;
//    [SerializeField]
//    private KeyCode activeRun;
//    [SerializeField]
//    private KeyCode run;
//    [SerializeField]
//    private KeyCode jump;
//    [SerializeField]
//    private KeyCode take;
//    public KeyCode Take { get { return take; } }

//    #endregion

//    Transform transf;
//    CharacterController charaController;
//    float speed;
//    bool lockRunning = false;
//    Vector3 jumpForce;
//    Vector3 jumpDirection;
//    float speedAtJump;
//    Animator animator;

//    [SerializeField]
//    private Camera cam;

//    #region PlayerState
//    private bool isCrouch;
//    private bool isWalking;
//    private bool isJumping;
//    private bool isRunning;
//    #endregion

//    [SerializeField]
//    private GameObject playerLeftHand;
//    [SerializeField]
//    private GameObject playerRightHand;
//    Hand leftHand;
//    Hand rightHand;
//    [SerializeField]
//    private GameObject playerGameObject;

//    void Start()
//    {
//        transf = transform;
//        charaController = gameObject.GetComponent<CharacterController>();
//        isRunning = false;
//        isJumping = false;
//        isCrouch = false;
//        isWalking = false;
//        animator = transf.root.GetComponent<Animator>();
//        leftHand = playerLeftHand.GetComponent<Hand>();
//        rightHand = playerRightHand.GetComponent<Hand>();
//        playerInventory = itemMgr.Inventory;
//        gui = gameObject.GetComponent<PlayerControllerGUI>();
//        GUIUpdater = gameObject.GetComponent<PlayerControllerGUIUpdater>();
//    }

//    void Update()
//    {
//        if (Time.timeScale == 0)
//            return;

//        Movement();
//        Action();
//        ItemDetector();
//    }

//    void Movement()
//    {
//        Vector3 direction = Vector3.zero;
//        if (Input.GetKey(forward))
//            direction += transf.forward;
//        if (Input.GetKey(backward))
//            direction -= transf.forward;
//        if (Input.GetKey(left))
//            direction -= transf.right;
//        if (Input.GetKey(right))
//            direction += transf.right;
//        //if (Input.GetKeyDown(activeRun) && playerAttribute.Endurance.min > 0)
//        //{
//        //	isRunning = !isRunning;
//        //	lockRunning = !lockRunning;
//        //}
//        //if (Input.GetKeyDown(run) && playerAttribute.Endurance.min > 0)
//        //	isRunning = true;
//        if (Input.GetKeyUp(run) && !lockRunning)
//            isRunning = false;


//        //if (playerAttribute.Endurance.min <= 0)
//        //{
//        //    playerAttribute.EnduranceMin = 0;
//        //	lockRunning = false;
//        //	isRunning = false;
//        //}
//        //
//        //if (isRunning)
//        //	playerAttribute.EnduranceMin -= 50 * Time.deltaTime;

//        isWalking = (direction != Vector3.zero);

//        isCrouch = Input.GetKey(crouch);
//        direction.Normalize();
		
//        //float moveSpeed = playerAttribute.attributes

//        if (isRunning && !isCrouch)
//            speed = playerAttribute.MoveSpeed * 2.0f;
//        else if (isWalking && !isCrouch)
//            speed = playerAttribute.MoveSpeed;
//        else
//            speed = playerAttribute.MoveSpeed * 0.5f;

//        if (playerAttribute.MoveSpeed * 2 == speed && direction != Vector3.zero)
//        {
//            animator.SetBool("Run", true);
//            animator.SetBool("Walk", false);
//        }
//        else if (playerAttribute.MoveSpeed == speed && direction != Vector3.zero)
//        {
//            animator.SetBool("Run", false);
//            animator.SetBool("Walk", true);
//        }
//        else if (direction == Vector3.zero)
//        {
//            animator.SetBool("Run", false);
//            animator.SetBool("Walk", false);
//        }

//        if (Input.GetKey(jump) && charaController.isGrounded)
//        {
//            jumpForce = Vector3.up * 2;
//            jumpDirection = direction;
//            isJumping = true;
//            speedAtJump = speed;
//        }

//        if (!isJumping)
//            charaController.SimpleMove(direction * speed);
//        else
//        {
//            jumpForce.y -= 5.0f * Time.deltaTime;
//            charaController.Move((jumpForce + jumpDirection) * speedAtJump * Time.deltaTime);
//            isJumping = !charaController.isGrounded;
//        }

//        RaycastHit hit = new RaycastHit();
//        Physics.Raycast(transf.position, Vector3.down, out hit);

//        if (isCrouch)
//        {
//            transf.position = Vector3.Lerp(transf.position, new Vector3(transf.position.x, hit.point.y + 0.4f, transf.position.z), 0.1f);
//            transf.localScale = new Vector3(1, 0.25f, 1);
//        }
//        else
//        {
//            transf.position = Vector3.Lerp(transf.position, new Vector3(transf.position.x, hit.point.y + 1, transf.position.z), 0.1f);
//            transf.localScale = new Vector3(1, 1, 1);
//        }
//    }

//    void    Action()
//    {
//        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

//        if (Input.GetMouseButtonDown(0))
//        {
//            if (!rightHand.WeaponObject && (info.tagHash == Animator.StringToHash("Idle") || info.tagHash == Animator.StringToHash("Run") || info.tagHash == Animator.StringToHash("Walk")))
//            {
//                animator.SetTrigger("RPunch");
//                animator.SetBool("Run", false);
//                animator.SetBool("Walk", false);
//                HitEnemy(5);
//            }
//            else if (rightHand.WeaponObject && rightHand.Weapon && rightHand.Weapon.WeaponType == EStuffType.SWORD)
//            {

//                if ((info.tagHash == Animator.StringToHash("Idle") || info.tagHash == Animator.StringToHash("Run") || info.tagHash == Animator.StringToHash("Walk")) ||
//                    (info.tagHash == Animator.StringToHash("CanDoCombo") && !animator.GetBool("RSwordAttack")))
//                {
//                    animator.SetTrigger("RSwordAttack");
//                    animator.SetBool("Run", false);
//                    animator.SetBool("Walk", false);
//                    HitEnemy(rightHand.Weapon.Range);
//                }
//            }
//            else if (rightHand.WeaponObject && rightHand.Weapon && rightHand.Weapon.WeaponType == EStuffType.DOUBLE_HANDED_SWORD)
//            {
//                if ((info.tagHash == Animator.StringToHash("Idle") || info.tagHash == Animator.StringToHash("Run") || info.tagHash == Animator.StringToHash("Walk")) ||
//                    (info.tagHash == Animator.StringToHash("CanDoCombo") && !animator.GetBool("DoubleHandedSwordAttack")))
//                {
//                    animator.SetTrigger("DoubleHandedSwordAttack");
//                    animator.SetBool("Run", false);
//                    animator.SetBool("Walk", false);
//                    HitEnemy(rightHand.Weapon.Range);
//                }
//            }
//            //else if (rightHand.WeaponObject && rightHand.Cast)
//            //    rightHand.Cast.UseCast();
//        }
//        if (Input.GetMouseButtonDown(1))
//        {
//            if (!leftHand.WeaponObject && !rightHand.WeaponObject && (info.tagHash == Animator.StringToHash("Idle") || info.tagHash == Animator.StringToHash("Run") || info.tagHash == Animator.StringToHash("Walk")))
//            {
//                animator.SetTrigger("LPunch");
//                animator.SetBool("Run", false);
//                animator.SetBool("Walk", false);
//                HitEnemy(5);
//            }
//            else if (leftHand.WeaponObject && leftHand.Weapon && leftHand.Weapon.WeaponType == EStuffType.SWORD)
//            {
//                if ((info.tagHash == Animator.StringToHash("Idle") || info.tagHash == Animator.StringToHash("Run") || info.tagHash == Animator.StringToHash("Walk")))
//                {
//                    animator.SetTrigger("LSwordAttack");
//                    animator.SetBool("Run", false);
//                    animator.SetBool("Walk", false);
//                    HitEnemy(leftHand.Weapon.Range);
//                }
//            }
//            else if (!leftHand.WeaponObject && rightHand.WeaponObject && rightHand.Weapon && (rightHand.Weapon.WeaponType == EStuffType.SWORD ||
//                        rightHand.Weapon.WeaponType == EStuffType.DOUBLE_HANDED_SWORD))
//            {
//                animator.SetBool("Defense", true);
//                animator.SetBool("Run", false);
//                animator.SetBool("Walk", false);
//            }
//            //else if (leftHand.WeaponObject && leftHand.Cast)
//            //    leftHand.Cast.UseCast();

//        }
//        if (Input.GetMouseButtonUp(1))
//        {
//            animator.SetBool("Defense", false);
//            animator.SetBool("Run", false);
//            animator.SetBool("Walk", false);
//        }
//    }
    
//    void ItemDetector()
//    {
//        Vector3 pos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
//        Ray ray = cam.ScreenPointToRay(pos);
//        RaycastHit hit = new RaycastHit();
//        bool touch = Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Default"));

//        GUIUpdater.InteractionGUI(hit);

//        if (Input.GetKeyDown(take))
//        {
//            if (touch)
//            {
//                if (hit.collider.transform.root.tag == "Door")
//                {
//                    ADoor door = hit.collider.transform.root.GetComponent<ADoor>();
//                    door.Interaction(playerInventory);
//                }
//                else if (hit.collider.transform.root.tag == "Stuff" ||
//                    hit.collider.transform.root.tag == "Key" ||
//                    hit.collider.transform.root.tag == "Cast")
//                    hit.collider.transform.root.GetComponent<CollectItemFromGround>().CollectItem();

//                else if (hit.collider.transform.root.tag == "Chest")
//                {
//                    SimpleChest chest = hit.collider.transform.root.GetComponent<ChestGUI>().Chest;
//                    ChestGUI chestGUI = hit.collider.transform.root.GetComponent<ChestGUI>();

//                    chest.entityAttribute = playerAttribute;
//                    chest.IsOpen = true;
//                    chestGUI.ShowGUI = true;
//                }
//                else if (hit.collider.transform.root.tag == "Gold")
//                {
//                    itemMgr.Inventory.Gold += hit.collider.transform.root.GetComponent<Gold>().quantity;

//                    Destroy(hit.collider.transform.root.gameObject);
//                }
//            }
//        }
//    }
    
//    List<EnemyAttribute> FindHitableEnemies(float distance)
//    {
//       // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
//        List<EnemyAttribute> hitableEnemies = new List<EnemyAttribute>();

//        //foreach (var enemy in enemies)
//        //{
//        //    //EnemyAttribute attribute = enemy.GetComponent<EnemyAttribute>();
//        //    //if (Vector3.Distance(transf.position, enemy.transform.position) <= distance &&
//        //    //    Vector3.Angle(transf.forward, (enemy.transform.position - transf.position)) <= 45 &&
//        //    //    attribute != null)
//        //    //    hitableEnemies.Add(attribute);
//        //}

//        return hitableEnemies;
//    }

//    void HitEnemy(float range)
//    {
//        List<EnemyAttribute> hitColliders = FindHitableEnemies(range);
        
//        foreach (var enemy in hitColliders)
//        {
//            float dmg = playerAttribute.Damage.RandomBetweenValues();

//            enemy.TargetAttribute = playerAttribute;
//            enemy.GetDamaged(dmg);
//            //enemy.AI.targetTransform = transf;
//        }
//    }
//}
