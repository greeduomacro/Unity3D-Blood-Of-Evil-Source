using UnityEngine;
using System.Collections;

public class SimpleHeal<TModuleType> : MonoBehaviour where TModuleType : APlayer
{

    [SerializeField]
	private MinMaxf care;
    public MinMaxf Care { get { return care; } set { care = value; } }
    [SerializeField]
	private AEntityAttribute<TModuleType> userAttribute;
	public AEntityAttribute<TModuleType> UserAttribute { get { return userAttribute; } set { if (value != null) userAttribute = value; } }

	void Awake()
	{
		Destroy(gameObject, 5);
	}

	void Update()
	{
		if (userAttribute.ManaCurrent > 1 && userAttribute.Life.Current < userAttribute.Life.Max)
		{
			userAttribute.LifeCurrent += Random.Range(care.min, care.max) * Time.deltaTime;
			userAttribute.ManaCurrent -= 11 * Time.deltaTime;
		}
	}
}
