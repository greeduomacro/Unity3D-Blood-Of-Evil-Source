using UnityEngine;
using System.Collections;

public static class GameObjectExtension
{
	public static GameObject FindNearestGameObjectWithTag(this GameObject nearest, Vector3 position, string tag)
	{
		GameObject[] entities = GameObject.FindGameObjectsWithTag(tag);

		if (entities.Length > 0)
		{
			nearest = entities[0];

			foreach (GameObject entity in entities)
				if (Vector3.Distance(nearest.transform.position, position) >
					Vector3.Distance(entity.transform.position, position))
					nearest.transform.position = entity.transform.position;
			return nearest; ;
		}

		return null;
	}

	//this.gameObject, "Scripts/Behaviour/Camera", "/" >> créra un sous GameObject Behaviour this.gameObject/Scripts/Behaviour/Camera
	////this.gameObject, "Scripts/Behaviour/Camera", "/", ObjectManager.Instantiate("Camera MMORPG")
	public static GameObject AddSubGameObjectsWithSeparator(GameObject currentParent, string format, string separator, GameObject lastNode = null)
	{
		GameObject currentObject = currentParent;
		GameObject sonObject = null;
		string[] subObjects = format.Split(separator.ToCharArray());

		for (short i =0; i < subObjects.Length; i++)
		{
			Transform trans = currentObject.transform.Find(StringExtension.FindStringBeforeSeparator(subObjects[i], separator));

			if (null == trans)
			{
				GameObject obj = new GameObject();
				sonObject = obj;
			}
			else
				sonObject = trans.gameObject;

			if (null != lastNode && i + 1 == subObjects.Length)
			{
				GameObject.Destroy(sonObject);
				lastNode.transform.parent = currentObject.transform;
				lastNode.name = subObjects[i];
			}
			else
			{
				sonObject.transform.parent = currentObject.transform;
				sonObject.name = subObjects[i];
			}

			currentObject = sonObject;
		}

		if (null != lastNode)
			sonObject = lastNode;

		return sonObject;
	}

	public static Bounds CombineBounds(this GameObject mesh)
	{
		if (null == mesh.GetComponent<Renderer>())
			mesh.AddComponent<MeshRenderer>();

		Bounds combinedBounds = mesh.GetComponent<Renderer>().bounds;
		Renderer[] renderers = mesh.transform.GetComponentsInChildren<Renderer>();

		foreach (var render in renderers)
			if (render != mesh.GetComponent<Renderer>())
				combinedBounds.Encapsulate(render.bounds);

		return combinedBounds;
	}

	public static void ReduceMeshSize(this GameObject mesh, float newSize)
	{
		MeshRenderer renderer = mesh.GetComponent<MeshRenderer>();

		if (null != renderer)
		{
			Vector3 size = mesh.transform.localScale;
			mesh.transform.localScale = new Vector3(newSize * size.x, newSize * size.y, newSize * size.z);
		}
	}

	//je lui donne un posX, un posY et il me calcule l'offset forward et right
	public static GameObject CreateItemMesh(string objectName, Rect rect)
	{
		Transform cameraTransform = Camera.main.transform;
		GameObject meshToDisplay = ServiceLocator.Instance.ObjectManager.Instantiate(objectName);

		meshToDisplay.gameObject.layer = LayerMask.NameToLayer("StuffMeshGUI");

		foreach (Transform tr in meshToDisplay.transform)
			tr.gameObject.layer = LayerMask.NameToLayer("StuffMeshGUI");
		
		Bounds combinedBounds = meshToDisplay.CombineBounds();
		float val = combinedBounds.size.GetOnePerMaxOfVectorComponent();

		meshToDisplay.ReduceMeshSize(val * 0.1f);

		meshToDisplay.transform.rotation = cameraTransform.rotation;
		meshToDisplay.transform.parent = cameraTransform;
		meshToDisplay.transform.position = cameraTransform.position + cameraTransform.forward * 4 +
		 cameraTransform.right * 1.3f * rect.x * Screen.width * 0.001f;//0.56f = 0.8f
		meshToDisplay.transform.Rotate(new Vector3(0, 90, 0));

		return meshToDisplay;
	}

	//public static void CreateAndRotateItemMesh(GameObject mesh, string prefabName, string title, Rect rect, float rotateSpeed)
	//{
	//    if (null == mesh)
	//        mesh = GameObjectExtension.CreateItemMesh(prefabName, 4, 1.3f);
	//        mesh.RotateItemMeshInGUI(rotateSpeed, title);
	//}
	//Ce que je veux, j'aimerai pouvoir avoir une fonction qui appele la création de l'objet si besoin et le rotate aussi
	//a l'endroit voulu
	public static void RotateItemMeshInGUI(this GameObject mesh, float rotateSpeed, string title, Rect rect)
	{
		GUI.Box(MultiResolutions.Rectangle(ref rect), "");
		if (GUI.RepeatButton(MultiResolutions.Rectangle(rect.x + rect.width - 0.02f, rect.y + (rect.height * 0.5f) - 0.02f, 0.02f, 0.04f), MultiResolutions.Font(16) + "<color=#00FF00FF>></color></size>"))
			mesh.transform.Rotate(new Vector3(0, rotateSpeed, 0));
		if (GUI.RepeatButton(MultiResolutions.Rectangle(rect.x, rect.y + (rect.height * 0.5f) - 0.02f, 0.02f, 0.04f), MultiResolutions.Font(16) + "<color=#00FF00FF><</color></size>"))
			mesh.transform.Rotate(new Vector3(0, -rotateSpeed, 0));

		GUIExtension.OnHoverText(MultiResolutions.Rectangle(rect.x + rect.width - 0.02f, rect.y + (rect.height * 0.5f) - 0.02f, 0.02f, 0.04f),
		MultiResolutions.Rectangle(-0.0275f, 0f, 0.07f, 0.032f), MultiResolutions.Font(16) + "<color=yellow>Rotate Right</color></size>");

		GUIExtension.OnHoverText(MultiResolutions.Rectangle(rect.x, rect.y + rect.height - 0.02f, 0.02f, 0.04f),
		MultiResolutions.Rectangle(-0.0275f, 0f, 0.07f, 0.032f), MultiResolutions.Font(16) + "<color=yellow>Rotate Left</color></size>");

		for (byte i = 0; i < 3; i++)
			GUI.Box(MultiResolutions.Rectangle(rect.x, rect.y, rect.width, 0.035f), title);
	}

	public static GameObject CreateAndRotateItemMesh(GameObject mesh, string prefabName, string title, Rect displayRect, float rotateSpeed)
	{
		mesh.RotateItemMeshInGUI(rotateSpeed, title, displayRect);

		if (null == mesh)
			mesh = GameObjectExtension.CreateItemMesh(prefabName, displayRect);

		return mesh;
	}
}
