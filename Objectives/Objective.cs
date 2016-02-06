using UnityEngine;
using System.Collections;

public enum e_Condition
{
    OnTriggerEnter,
    OnCollisionEnter,
    OnDestroy,
}

public class Objective : MonoBehaviour
{
	//[SerializeField]
	//private bool isFirst;
	//[SerializeField]
	//private Objective toUnlock;
	//[SerializeField]
	//private e_Condition condition;
	//PlayerAttribute playerAttribute;

	//void Start()
	//{
	//    StartCoroutine(FindPlayer());
	//}

	//IEnumerator FindPlayer()
	//{
	//    yield return new WaitForSeconds(0.2f);

	//    //playerAttribute = GameObject.FindObjectOfType<PlayerAttribute>();
	//    //if (isFirst)
	//    //    playerAttribute.CurrentObjective = this;
	//}

	//void OnTriggerEnter()
	//{
	////    if (condition == e_Condition.OnTriggerEnter)
	////        playerAttribute.CurrentObjective = toUnlock;
	//}

	//void OnCollisionEnter()
	//{
	//    //if (condition == e_Condition.OnCollisionEnter)
	//    //    playerAttribute.CurrentObjective = toUnlock;
	//}

	//void OnDestroy()
	//{
	//    //if (condition == e_Condition.OnDestroy && playerAttribute != null)
	//    //    playerAttribute.CurrentObjective = toUnlock;
	//}
}
