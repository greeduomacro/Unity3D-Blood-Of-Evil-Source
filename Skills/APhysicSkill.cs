using UnityEngine;
using System.Collections;

public abstract class APhysicSkill<TModuleType> : MonoBehaviour where TModuleType : APlayer
{
	public AEntityAttribute<TModuleType> user { get { return user; } set { if (value != null) user = value; } }
    public string targetTag { get { return targetTag; } set { if (value != "") targetTag = value; } }
    protected Transform trans;
    public float damage { get { return damage; } set { if (value > 0) damage = value; } }

	public void MyAwake()
	{
		trans = transform;
	}

	protected void UpdateEntityAttribute(GameObject entityObject)
	{
		//PlayerAttribute playerAttribute = user as PlayerAttribute;
		////AEntityAttribute targetAttribute = entityObject.GetComponent<AEntityAttribute>();

		//if (null != playerAttribute)
		//{
		//    EnemyAttribute enemyAttribute = targetAttribute as EnemyAttribute;
		//    if (null != enemyAttribute)
		//    {
		//        enemyAttribute.TargetAttribute = playerAttribute;
		//        //enemyAttribute.AI.targetTransform = playerAttribute.Trans;
		//    }
		//}

		//targetAttribute.GetDamaged(damage);
	}
}
