using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ObjectManager : AServiceInitializer
{
	#region Attributes
	[SerializeField] 
	private GameObject[] objects;
	private int[] objectsID;
	#endregion
	#region Properties
	private GameObject[] Objects
	{
		get { return objects; }
		set { objects = value; }
	}
	private int[] ObjectsID
	{
		get { return objectsID; }
		set { objectsID = value; }
	}
	#endregion
	#region Builder
	public override void Initialize()
	{
		IDManager.InitializeID(this.objects, ref this.objectsID);
		this.InstantiateInstantiatedObjects();
	}
	private void InstantiateInstantiatedObjects()
	{
		GameObject instantiatedObjects = this.Instantiate("Empty");
		instantiatedObjects.name = "InstantiatedObjects";
		instantiatedObjects.transform.parent = transform;
	}
	#endregion
	#region Functions
	public GameObject		Instantiate(string name, Vector3 pos, Quaternion rot)
	{
		GameObject objectToInstantiate = this.GetObject(name);

		if (null != objectToInstantiate)
			return GameObject.Instantiate(objectToInstantiate, pos, rot) as GameObject;

		Debug.LogWarning("Object Manager didn't find your object (" + name + ") , he will create one new instance of game object for you");

		return new GameObject();
	}

	public GameObject Instantiate(string name, string parentName)
	{
		return this.Instantiate(name, parentName, name);
	}

	public GameObject Instantiate(string name)
	{
		return this.Instantiate(name, Vector3.zero, Quaternion.identity);
	}

	public GameObject Instantiate(string name, string parentName, string newObjectName)
	{
		GameObject findedObject = this.Instantiate(name, Vector3.zero, Quaternion.identity);
		GameObject parentObject = GameObject.Find(parentName);

		if (null == parentObject)
		{
			parentObject = this.Instantiate("Empty");
			parentObject.name = parentName;
			parentObject.transform.parent = GameObject.Find("InstantiatedObjects").transform;
		}

		findedObject.transform.parent = parentObject.transform;
		findedObject.name = newObjectName;

		return findedObject;
	}

	public GameObject GetObject(string name)
	{
		int objectID = name.GetHashCode();

		for (short i =0; i < this.objectsID.Length; i++)
			if (this.objectsID[i] == objectID)
				return this.objects[i];

		Debug.LogWarning("you object " + name + " have not been found");

		return null;
	}
	#endregion
	#region Comment
	//private SortedDictionary<string, GameObject> meshes;

	//public GameObject GetMesh(string name)
	//{
	//	if (!meshes.ContainsKey(name))
	//	{
	//		Debug.LogWarning("The key of your sorted dictionary of meshes don't exist, I will return you a new gameobject");
	//		return new GameObject();
	//	}

	//	GameObject tmpObject;
	//	if (!meshes.TryGetValue(name, out tmpObject))
	//	{
	//		Debug.LogWarning("The value of your sorted dictionary of meshes don't exist, I will return you a new gameobject");
	//		return new GameObject();
	//	}

	//	return tmpObject;		
	//}

	//public GameObject InstantiateMesh(string name)
	//{
	//	return Instantiate(GetMesh(name), Vector3.zero, Quaternion.identity) as GameObject;
	//}
	#endregion
}
