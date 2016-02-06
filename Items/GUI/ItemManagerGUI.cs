using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemManagerGUI<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
{
	private ItemManager<TModuleType> itemMgr;
	private Inventory<TModuleType> inventory;
	private Equipment<TModuleType> equipment;

	private ObjectAttributeToString<TModuleType> objectAttributeToString;

	private AEntityAttribute<TModuleType> player;
	private GameObject meshToDisplay;

	private Vector2 scrollPosition;

	[HideInInspector]
	private bool itemSelected;
	private byte itemSelectedIndex;
	private byte itemSelectedFiltre;

	private bool rightClick;
	private byte filtre;

	void Awake()
	{
		this.player = base.ModuleManager.Attributes as AEntityAttribute<TModuleType>;
		this.itemMgr = base.ModuleManager.Items as ItemManager<TModuleType>;

		this.objectAttributeToString = new ObjectAttributeToString<TModuleType>() as ObjectAttributeToString<TModuleType>;

		this.GUIWindowInitialization(new Rect(0, 0, 1, 1));

		this.scrollPosition = Vector2.zero;
		this.filtre = ItemExtension.FiltreAll;
		this.meshToDisplay = null;
	}

	public void  ResetItemSelected()
	{
		this.itemSelected = false;
		this.itemSelectedIndex = 0;

		// AREVOIR 5.0
		//if (null != this.meshToDisplay)
		//	Destroy(this.meshToDisplay);
	}

    void Start()
    {
		this.inventory = this.itemMgr.Inventory;
		this.equipment = this.itemMgr.Equipment;

    }

	public override void Update()
	{
		this.rightClick = Input.GetMouseButton(1);

		//A RVOIR 5.0
		//if (!this.IsActive && null != this.meshToDisplay)
		//	Destroy(this.meshToDisplay);
	}

	public override void OnGUIDrawWindow(int windowID)
	{
		this.Filter();
		this.Liste();
		this.Modifieur();

		if (this.itemSelected)
		{
			if (itemSelectedFiltre == ItemExtension.FiltreWeapon || itemSelectedFiltre == ItemExtension.FiltreClothe)
				this.ShowItemAttributes(itemSelectedIndex);
			else if (itemSelectedFiltre == ItemExtension.FiltreConsommable)
				this.ShowConsommableInteraction(itemSelectedIndex);
			this.ShowItemMesh(itemSelectedIndex);
		}
	}

	void Filter()
	{
		GUI.Box(MultiResolutions.Rectangle(0, 0, 0.20f, 1.0f), "");
		GUI.Label(MultiResolutions.Rectangle(0.08f, 0.30f, 1f, 1f), MultiResolutions.Font(20.0f) + "<b><color=red>Filtres</color></b></size>");

		if (GUI.Button(MultiResolutions.Rectangle(0, 0.35f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((this.filtre & ItemExtension.FiltreAll) != 0) ? "<b><color=red>TOUS</color></b>" : "TOUS") + "</size>"))
			this.filtre = ItemExtension.FiltreAll;
		if (GUI.Button(MultiResolutions.Rectangle(0, 0.40f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((this.filtre & ItemExtension.FiltreWeapon) != 0) ? "<b><color=red>ARMES</color></b>" : "ARMES") + "</size>"))
			this.filtre = ItemExtension.FiltreWeapon;
		if (GUI.Button(MultiResolutions.Rectangle(0, 0.45f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((this.filtre & ItemExtension.FiltreClothe) != 0) ? "<b><color=red>TENUES</color></b>" : "TENUES") + "</size>"))
			this.filtre = ItemExtension.FiltreClothe;
		if (GUI.Button(MultiResolutions.Rectangle(0, 0.50f, 0.2f, 0.05f), MultiResolutions.Font(15.0f) + (((this.filtre & ItemExtension.FiltreKeys) != 0) ? "<b><color=red>CLES</color></b>" : "CLES") + "</size>"))
			this.filtre = ItemExtension.FiltreKeys;
	}

	private byte CountFiltreLength()
	{
		byte filtreLength = 0;

		for (short i =0; i < inventory.Items.Count; i++)
			if ((((this.filtre & ItemExtension.FiltreAll) != 0) || (this.filtre & this.inventory.Items[i].Filtre) != 0) && this.inventory.Items[i].Mesh != "Cast")
				++filtreLength;

		return filtreLength;
	}

	private void BeginScrollViewList(byte filtreLength)
	{
		this.scrollPosition = GUI.BeginScrollView(MultiResolutions.Rectangle(0.22f, 0.35f, 0.17f, 0.45f), this.scrollPosition,
			MultiResolutions.Rectangle(0f, 0f, 0.18f, 0.05f * filtreLength));
	}

	void Liste()
	{
		GUI.Box(MultiResolutions.Rectangle(0.21f, 0, 0.20f, 1.0f), "");
		GUI.Label(MultiResolutions.Rectangle(0.28f, 0.3f, 1f, 1f), MultiResolutions.Font(20) + "<b><color=red>Liste</color></b></size>");
		GUI.Box(MultiResolutions.Rectangle(0.22f, 0.35f, 0.17f, 0.45f), "");

		this.BeginScrollViewList(CountFiltreLength());

		this.DisplayItemList();
	}

	private void SelectAndMaybeEquipItem(AStuff<TModuleType> stuff, byte itemIndexDefinedByFiltre)
	{
		if (this.itemSelectedFiltre == ItemExtension.FiltreWeapon)
		{
			if (stuff.Selected && stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED)
			{
				if (stuff.equipmentEmplacement != e_equipmentEmplacement.Both_Hand)
				{
					if (this.rightClick)
						stuff.equipmentEmplacement = e_equipmentEmplacement.Left_Hand;
					else
						stuff.equipmentEmplacement = e_equipmentEmplacement.Right_Hand;
				}

				this.itemMgr.Equip(stuff);
				stuff.Selected = false;
			}
		}
		else if (itemSelectedFiltre == ItemExtension.FiltreClothe)
		{
			if (stuff.Selected && stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED)
			{
				this.itemMgr.Equip(stuff);
				stuff.Selected = false;
			}
		}

		this.inventory.Select(itemIndexDefinedByFiltre, ref this.itemSelectedFiltre, stuff);

		// A REVOIR 5.0if (null != this.meshToDisplay)
		// A REVOIR 5.0	Destroy(this.meshToDisplay);
	}

	private void SelectThisItem(byte i, byte filtreToSelect)
	{
		this.itemSelected = true;
		this.itemSelectedIndex = i;
		this.itemSelectedFiltre = filtreToSelect;
	}

	public void DisplayWeapon(ref byte offset, ref byte weaponIndex)
	{
		AWeapon<TModuleType> weapon = this.inventory.Weapons[weaponIndex];

		if (GUI.Button(MultiResolutions.Rectangle(0, offset * 0.05f, 0.17f, 0.05f), StuffGUI<TModuleType>.GetEquippedStuffName(weapon)))
			this.SelectAndMaybeEquipItem(weapon, weaponIndex);
			
		if (inventory.Weapons[weaponIndex].Selected)
			this.SelectThisItem(weaponIndex, ItemExtension.FiltreWeapon);

		++weaponIndex;
		++offset;
	}

	public void DisplayClothe(ref byte offset, ref byte clotheIndex)
	{
		AClothe<TModuleType> clothe = this.inventory.Clothes[clotheIndex];

		if (GUI.Button(MultiResolutions.Rectangle(0, offset * 0.05f, 0.17f, 0.05f), StuffGUI<TModuleType>.GetEquippedStuffName(clothe)))
			this.SelectAndMaybeEquipItem(clothe, clotheIndex);

		if (inventory.Clothes[clotheIndex].Selected)
			this.SelectThisItem(clotheIndex, ItemExtension.FiltreClothe);

		++clotheIndex;
		++offset;
	}

	public void DisplayKey(ref byte offset, ref byte keyIndex)
	{
		Keys<TModuleType> key = this.inventory.Keys[keyIndex];

		if (GUI.Button(MultiResolutions.Rectangle(0, offset * 0.05f, 0.17f, 0.05f), StuffGUI<TModuleType>.GetKeyName(key)))
			this.SelectThisItem(keyIndex, ItemExtension.FiltreKeys);

		++keyIndex;
		++offset;
	}

	public void DisplayConsommable(ref byte offset, ref byte consommableIndex)
	{
		AConsommable<TModuleType> consommable = this.inventory.Consommables[consommableIndex];

		if (GUI.Button(MultiResolutions.Rectangle(0, offset * 0.05f, 0.17f, 0.05f), MultiResolutions.Font(16)  + "<color=purple>" + consommable.Name + "</color></size>"))
			this.SelectThisItem(consommableIndex, ItemExtension.FiltreConsommable);

		++consommableIndex;
		++offset;
	}

	public void DisplayItemList()
	{
		byte offset = 0, weaponIndex = 0, clotheIndex = 0, keyIndex = 0, consommableIndex = 0;

		for (byte i = 0; i < this.inventory.Items.Count; i++)
		{
			AItem<TModuleType> item = this.inventory.Items[i];

			if (item.Filtre == ItemExtension.FiltreWeapon && (this.filtre == ItemExtension.FiltreWeapon || this.filtre == ItemExtension.FiltreAll))
				this.DisplayWeapon(ref offset, ref weaponIndex);
			else if (item.Filtre == ItemExtension.FiltreClothe && (this.filtre == ItemExtension.FiltreClothe || this.filtre == ItemExtension.FiltreAll))
				this.DisplayClothe(ref offset, ref clotheIndex);
			else if (item.Filtre == ItemExtension.FiltreKeys && (this.filtre == ItemExtension.FiltreKeys || this.filtre == ItemExtension.FiltreAll))
				this.DisplayKey(ref offset, ref keyIndex);
			else if (item.Filtre == ItemExtension.FiltreConsommable && (this.filtre == ItemExtension.FiltreConsommable || this.filtre == ItemExtension.FiltreAll))
				this.DisplayConsommable(ref offset, ref consommableIndex);
		}

		GUI.EndScrollView();
	}

	void Modifieur()
	{
		GUI.Box(MultiResolutions.Rectangle(0, 0.85f, 1.0f, 0.15f), "");
		GUI.Label(MultiResolutions.Rectangle(0.7f, 0.85f, 1, 1), MultiResolutions.Font(24) +
			"<color=red><b>Poids</b></color> : " + this.inventory.CurrentWeight + "/" + this.inventory.MaxWeight +
			"\t\t<color=red><b>Or</b></color> : " + this.inventory.Gold + "</size>");
	}

	void ShowConsommableInteraction(int itemSelectedIndex)
	{
		AConsommable<TModuleType> consommable = this.inventory.Consommables[itemSelectedIndex];
		string text = MultiResolutions.Font(13) + "<color=red>" + consommable.Name + "\n" + consommable.Description + "\n</color></size>\n";
		Rect rect = StuffGUI<TModuleType>.GetRectOfMyGUIContent(text, 0.41f, 0.85f);
		float posX = rect.x + rect.width;
		float posY = rect.y;

		StuffGUI<TModuleType>.DisplayItemContent(MultiResolutions.Rectangle(rect), text);

		for (byte i = 0; i < 3; i++)
			GUI.Box(MultiResolutions.Rectangle(posX, posY - 0.050f, 0.06f, 0.025f * 4), "");

		if (GUI.Button(MultiResolutions.Rectangle(posX, posY - 0.050F, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Sell</b></color></size>"))
			this.itemMgr.Sell(this.itemSelectedIndex, this.itemSelectedFiltre);
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY + 0.025f - 0.050F, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Throw</b></color></size>"))
			this.itemMgr.ThrowOut(this.itemSelectedIndex, this.itemSelectedFiltre);
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY + 0.025f * 2 - 0.050F, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Destruct</b></color></size>"))
			this.itemMgr.Destruct(this.itemSelectedIndex, this.itemSelectedFiltre);
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY + 0.025f * 3 - 0.050F, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Use</b></color></size>"))
			this.itemMgr.UseConsommable(this.itemSelectedIndex);
			
		//StuffGUI.DisplayItemInteractionBorder(posX, posY, stuff);
	}

	void ShowItemAttributes(int itemSelectedIndex)
	{
		if (this.itemSelectedFiltre == ItemExtension.FiltreWeapon)
		{
			AStuff<TModuleType> stuff = this.inventory.Weapons[itemSelectedIndex];
			string text = this.GetStuffContent(stuff);

			this.ItemInteraction(text, stuff);
		}
		else if (this.itemSelectedFiltre == ItemExtension.FiltreClothe)
		{
			AStuff<TModuleType> stuff = this.inventory.Clothes[itemSelectedIndex];
			string text = GetStuffContent(stuff);

			this.ItemInteraction(text, stuff);
		}
	}

	public string GetStuffContent(AStuff<TModuleType> stuff)
	{
		AStuff<TModuleType> equippedStuff = this.equipment.EquipmentSlots[((int)stuff.equipmentEmplacement)].Item;
		string text = "";
		text = StuffGUI<TModuleType>.GetStuffTitle(stuff, this.inventory, this.itemSelectedIndex);

		//if (stuff.equipmentEmplacement == e_equipmentEmplacement.Both_Hand && 
		//	(null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item || null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item))
		//{
		//	//if (null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item && null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item)
		//	//	text += objectAttributeToString.ComparedDefinedToString(stuff,
		//	//	objectAttributeToString.AdditionListOfObjectAttributeCompared(
		//	//	stuff.GetTheWhiteAttributesCompared(equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item, player),
		//	//	stuff.GetTheWhiteAttributesCompared(equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item, player))) + "</size>";
		//	//if (null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item)
		//	//	text += objectAttributeToString.GetTheWhiteAttributesCompared(stuff, equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item, player);
		//	//else if (null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item)
		//	//	text += objectAttributeToString.GetTheWhiteAttributesCompared(stuff, equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item, player);
		//}
		if (equippedStuff != null && stuff.equipmentCategory == equippedStuff.equipmentCategory)
			text += this.objectAttributeToString.GetTheWhiteAttributesCompared(stuff, equippedStuff, player);
		else
			text += this.objectAttributeToString.SimpleDefinedToString(stuff, stuff.GetTheWhiteAttributes()) + "</size>";
	
		//if (stuff.equipmentEmplacement == e_equipmentEmplacement.Both_Hand &&
		//	(null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item || null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item))
		//{
		//	//if (null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item && null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item)
		//	//	text += objectAttributeToString.ComparedRandomToString(stuff,
		//	//	objectAttributeToString.AdditionListOfObjectAttributeCompared(
		//	//	stuff.GetTheBlueAttributesCompared(equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item.GetheBlueAttributes()),
		//	//	stuff.GetTheBlueAttributesCompared(equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item.GetheBlueAttributes()))) + "</size>";
		//	//if (null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item)
		//	//	text += objectAttributeToString.ComparedRandomToString(stuff, stuff.GetTheWhiteAttributesCompared(equipment.equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item, player));
		//	//else if (null != equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item)
		//	//	text += objectAttributeToString.ComparedRandomToString(stuff, stuff.GetTheWhiteAttributesCompared(equipment.equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item, player));
		//}
		if (equippedStuff != null && stuff.equipmentEmplacement == equippedStuff.equipmentEmplacement && stuff != equippedStuff && equippedStuff.equipped != e_equipmentEquipped.NOT_EQUIPPED)
			text += this.objectAttributeToString.GetTheBlueAttributesCompared(stuff, equippedStuff);
		else
			text += StuffGUI<TModuleType>.GetTheBlueAttributes(stuff);

		return text;
	}

	public void ShowItemMesh(int itemIndex)
	{
		Rect rectToPutMesh = new Rect(0.6f, 0.00f, 0.17f, 0.50f);
		if (this.itemSelectedFiltre == ItemExtension.FiltreWeapon)
		{
			var stuff = this.inventory.Weapons[itemIndex];

			this.meshToDisplay = this.CreateAndRotateItemMesh(this.meshToDisplay, stuff.Mesh, StuffGUI<TModuleType>.GetStuffName(stuff), rectToPutMesh, 5f);
		}
		else if (this.itemSelectedFiltre == ItemExtension.FiltreClothe)
		{
			var stuff = this.inventory.Clothes[itemIndex];

			this.meshToDisplay = this.CreateAndRotateItemMesh(this.meshToDisplay, stuff.Mesh, StuffGUI<TModuleType>.GetStuffName(stuff), rectToPutMesh, 5f);
		}
		else if (itemSelectedFiltre == ItemExtension.FiltreKeys)
		{
			var stuff = this.inventory.Keys[itemIndex];

			this.meshToDisplay = this.CreateAndRotateItemMesh(this.meshToDisplay, stuff.Mesh, StuffGUI<TModuleType>.GetKeyName(stuff), rectToPutMesh, 5f);
		}
		//else if (itemSelectedFiltre == ItemExtension.FiltreConsommable)
		//{
		//	var stuff = inventory.consommables[itemIndex];

		//	meshToDisplay = CreateAndRotateItemMesh(meshToDisplay, stuff.mesh, "Consommable", rectToPutMesh, 5f);
		//}
		//Debug.Log(itemIndex);
	}

	private GameObject CreateAndRotateItemMesh(GameObject mesh, string prefabName, string title, Rect displayRect, float rotateSpeed)
	{
		mesh.RotateItemMeshInGUI(rotateSpeed, title, displayRect);

		if (null == meshToDisplay)
			mesh = GameObjectExtension.CreateItemMesh(prefabName, displayRect);
		
		return mesh;
	}

	void ItemInteraction(string text, AStuff<TModuleType> stuff)
	{
		Rect rect = StuffGUI<TModuleType>.GetRectOfMyGUIContent(text, 0.41f, 0.85f);
		float posX = rect.x + rect.width;
		float posY = rect.y;

		StuffGUI<TModuleType>.DisplayItemContent(MultiResolutions.Rectangle(rect), text);
		StuffGUI<TModuleType>.DisplayItemInteractionBorder(posX, posY, stuff);

		this.ItemInteractionBehaviour(ref posX, ref posY, stuff);
	}

	void EquipInteraction(ref float posX, ref float posY, AStuff<TModuleType> stuff)
	{
		if (stuff.equipmentEmplacement == e_equipmentEmplacement.Left_Hand || stuff.equipmentEmplacement == e_equipmentEmplacement.Right_Hand)
		{
			if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Equiper(L)</b></color>" + "</size>"))
			{
				stuff.equipmentEmplacement = e_equipmentEmplacement.Left_Hand;
				this.itemMgr.Equip(stuff);
			}
			posY += 0.025f;
			if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Equiper(R)</b></color>" + "</size>"))
			{
				stuff.equipmentEmplacement = e_equipmentEmplacement.Right_Hand;
				this.itemMgr.Equip(stuff);
			}
			posY += 0.025f;
		}
		else
		{
			if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Equiper</b></color>" + "</size>"))
				this.itemMgr.Equip(stuff);
			posY += 0.025f;
		}
	}

	void UnEquipInteraction(ref float posX, ref float posY, AStuff<TModuleType> stuff)
	{
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=#00FF00><b>Déséquiper</b></color>" + "</size>"))
			this.itemMgr.Unequip(stuff);
		posY += 0.025f;
	}

	void ThrowInteraction(ref float posX, ref float posY, AItem<TModuleType> stuff)
	{
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=red><b>Jeter</b></color>" + "</size>"))
			this.itemMgr.ThrowOut(this.itemSelectedIndex, this.itemSelectedFiltre);
		posY += 0.025f;
	}

	void SellInteraction(ref float posX, ref float posY, AItem<TModuleType> stuff)
	{
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=red><b>Sell</b></color>" + "</size>"))
			this.itemMgr.Sell(itemSelectedIndex, itemSelectedFiltre);
		posY += 0.025f;
	}

	void DestroyInteraction(ref float posX, ref float posY, AItem<TModuleType> stuff)
	{
		if (GUI.Button(MultiResolutions.Rectangle(posX, posY, 0.06f, 0.025f), MultiResolutions.Font(14) + "<color=red><b>Détruire</b></color>" + "</size>"))
			this.itemMgr.Destruct(itemSelectedIndex, itemSelectedFiltre);
	}

	void ItemInteractionBehaviour(ref float posX, ref float posY, AStuff<TModuleType> stuff)
	{
		AItem<TModuleType> item = this.inventory.Items[itemSelectedIndex];

		if (item.Filtre == ItemExtension.FiltreClothe || item.Filtre == ItemExtension.FiltreWeapon)
		{
			if (stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED)
				this.EquipInteraction(ref posX, ref posY, stuff);
			else
				this.UnEquipInteraction(ref posX, ref posY, stuff);
		}

		this.ThrowInteraction(ref posX, ref posY, stuff);

		if (stuff.equipped == e_equipmentEquipped.NOT_EQUIPPED)
		{
			this.SellInteraction(ref posX, ref posY, stuff);
			this.DestroyInteraction(ref posX, ref posY, stuff);
		}
	}
}