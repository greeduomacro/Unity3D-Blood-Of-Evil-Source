using UnityEngine;
using System.Collections;

public sealed class CollectItemFromGround : MonoBehaviour {
	//[SerializeField]
	//private AItem<APlayer> item;

	//#region Property
	//public AItem<APlayer> Item	{ get { return item; }
	//                      set { if (null != value) item = value; } }
	//#endregion

	//void Awake()	{
	//    this.item = null;
	//}

	//public void CollectItem()	{
	//    Inventory inventory = (GameObject.FindGameObjectWithTag("PlayerInfo").GetComponent<APlayer>().Items as ItemManager).Inventory;

	//    if (inventory.Items.Count < inventory.Size &&
	//        inventory.CurrentWeight + this.item.Weight <= inventory.MaxWeight)
	//    {
	//        inventory.AddItem(this.item);
	//        Destroy(gameObject);
	//        inventory.UnselectAll();
	//    }
	//}

	//void OnGUI()
	//{
	////	//centrer un texte sur un gameobject
	////	if (null != item)
	////	{ 
	////		offset = new Vector2(item.name.Length * -0.0025f, -0.05f);
	////		Vector3 position = Camera.main.GetComponent<Camera>().WorldToViewportPoint(trans.position);
	////		Rect rect = MultiResolutions.Rectangle(position.x + offset.x, 1 - position.y + offset.y, item.name.Length * 0.005f, 0.03f);
	////		var stuff = item as AStuff;

	////		/*GUI.Box(rect, "");
	////		if (GUI.Button(rect, MultiResolutions.Font(12) + stuff.GetItemColor() + item.name + "</color></size>"))
	////			CollectItem();*/
	////	}
	////	//Afficher le nom de l'objet, si stuff affiché la qualité d'une certaine couleur
	//}
}
