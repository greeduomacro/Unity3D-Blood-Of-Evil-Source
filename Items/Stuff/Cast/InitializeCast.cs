using UnityEngine;
using System.Collections;

public class InitializeCast<TModuleType> : MonoBehaviour where TModuleType : APlayer
{
    [SerializeField]
    private e_skillCategory category;
    [SerializeField]
    private string castName;

	void Start ()
    {
		//ACORRIGER
		//PhysicalCast<TModuleType> physicalCast = gameObject.GetComponent<PhysicalCast>() as PhysicalCast<TModuleType>;
		//if (this.category == e_skillCategory.Power)
		//    physicalCast.Cast = new Cast(castName, new WarScreamSkill<TModuleType>());
		//if (this.category == e_skillCategory.Destruction)
		//    physicalCast.Cast = new Cast<TModuleType>(castName, new FireballSkill<TModuleType>());
		//if (this.category == e_skillCategory.Heal)
		//    physicalCast.Cast = new Cast<TModuleType>(castName, new SimpleHealSkill<TModuleType>());

		//CollectItemFromGround collect = gameObject.GetComponent<CollectItemFromGround>() as CollectItemFromGround;
		//collect.Item = physicalCast.Cast;
    }
}
