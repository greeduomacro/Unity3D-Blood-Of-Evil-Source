using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//On n'a pas encore hashé la string, il faut s'en occupé
public sealed class ObjectsPoolManager : AServiceInitializer
{
	#region Attributes
	[SerializeField]
	private GameObject[] objectPool; //mettre dedans les ObjectPool dont on veut leur assigner un object pool et la taille de cette pool*
	[SerializeField]
	private List<GameObject>[] objectsPools; //la liste des objets de ma pool
	#endregion
	#region Data Attributes
	//[SerializeField]
	private bool extandable;
	#endregion
	#region Properties
	private GameObject[] ObjectPool
	{
		get { return ObjectPool; }
		set { ObjectPool = value; }
	}
	private List<GameObject>[] ObjectsPools
	{
		get { return objectsPools; }
		set { objectsPools = value; }
	}
	#endregion
	#region Data Properties
	private bool Extandable
	{
		get { return extandable; }
		set { extandable = value; }
	}
	#endregion
	#region Builder
	public override void Initialize()
	{
		this.extandable = true;

		this.objectsPools = new List<GameObject>[this.objectPool.Length];

		for (short i =0; i < this.objectPool.Length; i++)
		{
			this.objectsPools[i] = new List<GameObject>();

			int poolSize = 1;

			for (int x = 0; x < poolSize; x++)
			{
				GameObject newObject = GameObject.Instantiate(this.objectPool[i]) as GameObject;

				newObject.SetActive(false);
				this.objectsPools[i].Add(newObject);
			}
		}
	}
	#endregion
	#region Functions
	public GameObject InstantiateObjectInPool(string prefabName, Vector3 position, Quaternion rotation){
		if (this.GetPoolPrefab(prefabName) != null)
		{
			GameObject newObject = GetPoolPrefab(prefabName);
			newObject.transform.position = position;
			newObject.transform.rotation = rotation;
	
			newObject.SetActive(true);
			return newObject;
		}

		else	{ Debug.LogError("instantiate in object pool failled");	return null; }
	}
	public GameObject InstantiateObjectInPool(string prefabName, Vector3 position, Quaternion rotation, GameObject parent)
	{
		GameObject newObject = this.InstantiateObjectInPool(prefabName, position, rotation);

		if(newObject != null)
		{
			newObject.transform.parent = parent.transform;
			return newObject;
		}
		else	return null;
	}
	public GameObject GetPoolPrefab(string prefabName)
	{
		for (int y = 0; y < this.objectPool.Length; y++)
		{
			if (this.objectPool[y].name == prefabName)
			{
				for (int x = 0; x < this.objectsPools[y].Count; x++)
					if (!this.objectsPools[y][x].activeInHierarchy)
						return this.objectsPools[y][x];

				if (this.extandable)
				{
					GameObject newObject	=  GameObject.Instantiate(ObjectPool[y]) as GameObject;
					newObject.SetActive(false);
					this.objectsPools[y].Add(newObject);
					return newObject;
				}

				break;
			}
		}

		return null;
	}
	public void LoadParticuleSystem(GameObject particleObject, int particlesAmount)
	{
		if (particleObject.GetComponent<ParticleSystem>())
			particleObject.GetComponent<ParticleSystem>().Emit(particlesAmount);
	}
	public void LoadAudioSource(GameObject soundObject)
	{
		if (soundObject.GetComponent<AudioSource>())
			soundObject.GetComponent<AudioSource>().PlayOneShot(soundObject.GetComponent<AudioSource>().clip);
	}
	public void DestroyObject(GameObject obj)
	{
		obj.SetActive(false);
	}
	#endregion
}



/*public static ObjectPool	Current;
public GameObject			PooledObject;
public int					pooledAmount;
public bool					extandable;

public List<GameObject>		PooledObjects;

void	Awake()	{
	Current = this;
}

// Use this for initialization
void	Start () {
	PooledObjects = new List<GameObject>();
		
	for (var i = 0; i < pooledAmount; i++)
	{
		GameObject	NewObject = Instantiate(PooledObject) as GameObject;

		NewObject.SetActive(false);
		PooledObjects.Add(NewObject);
	}
}
public GameObject	GetPooledObject()	{
	for (var i = 0; i < pooledAmount; i++)
		if (!PooledObjects[i].activeInHierarchy)
			return PooledObjects[i];

	if (extandable)
	{
		GameObject NewObject = Instantiate(PooledObject) as GameObject;
			
		PooledObjects.Add(NewObject);
		return NewObject;
	}

	return null;
}

void InstantiateInPoolObject(string prefabName, Vector3 position, Quaternion rotation)
{
	GameObject NewObject = ObjectPool.Current.GetPooledObject();

	if (NewObject == null) return;

	NewObject.transform.position = position;
	NewObject.transform.rotation = rotation;
	NewObject.SetActive(true);
}*/