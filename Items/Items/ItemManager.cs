using UnityEngine;
using System.Collections;

[System.Serializable]
public sealed class ItemManager<TModuleType> : AModule<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private Inventory<TModuleType> inventory;
	private Equipment<TModuleType> equipment;
	private PlayerAttribute<APlayer> attribute;
	private AttributeInitializer<TModuleType> attributeInitializer;
	//private ItemManagerGUI itemManagerGUI;
	#endregion
	#region Properties
	public AttributeInitializer<TModuleType> AttributeInitializer
	{
		get { return attributeInitializer; } 
														private set { attributeInitializer = value; } }
	public Inventory<TModuleType> Inventory
	{
		get { return inventory; } 
									private set { inventory = value; } }
	public Equipment<TModuleType> Equipment
	{
		get { return equipment; } 
									private set { equipment = value; } }
	#endregion
	public ItemManager() { }
	public override void Update()	{ }
	public override void Initialize(TModuleType player)
	{
		base.SetModule(player, "Scripts/Behaviour/Items");

		this.inventory = new Inventory<TModuleType>();
		this.equipment = new Equipment<TModuleType>();
		this.attribute = base.ModuleManager.Attributes;
		this.attributeInitializer = new AttributeInitializer<TModuleType>();
		//this.itemManagerGUI = gameObject.GetComponent<ItemManagerGUI>() as ItemManagerGUI;
		this.attributeInitializer.InitializeByPlayerAttribute(this.attribute);
	}

	public void Sell(int itemSelectedIndex, byte itemFiltre)
	{
		//this.itemManagerGUI.ResetItemSelected();
		if (this.DoesItemExistInInventory(itemSelectedIndex, itemFiltre, "sell"))
		{
			if (itemFiltre == ItemExtension.FiltreWeapon)
			{
				AWeapon<TModuleType> item = this.inventory.Weapons[itemSelectedIndex];

				this.inventory.Gold += item.Price;

				if (item.Filtre == ItemExtension.FiltreWeapon)
					this.Unequip(item);

				this.inventory.RemoveItem(item);
			}
			else if (itemFiltre == ItemExtension.FiltreClothe)
			{
				AClothe<TModuleType> item = this.inventory.Clothes[itemSelectedIndex];

				this.inventory.Gold += item.Price;

				if (item.Filtre == ItemExtension.FiltreWeapon)
					this.Unequip(item);

				this.inventory.RemoveItem(item);
			}
			else if (itemFiltre == ItemExtension.FiltreConsommable)
			{
				AItem<TModuleType> item = this.inventory.Consommables[itemSelectedIndex];

				this.inventory.Gold += item.Price;

				this.inventory.RemoveItem(item);
			}
		}
	}
	public void Destruct(int itemSelectedIndex, byte itemFiltre)
	{
		//this.itemManagerGUI.ResetItemSelected();
		if (this.DoesItemExistInInventory(itemSelectedIndex, itemFiltre, "destruct"))
		{
			AItem<TModuleType> item = this.GetItemWithFiltre(itemSelectedIndex, itemFiltre);
			AStuff<TModuleType> stuff = item as AStuff<TModuleType>;

			if (null != stuff)
				this.Unequip(stuff);
			this.inventory.RemoveItem(item);
		}
	}

	public AItem<TModuleType> GetItemWithFiltre(int itemSelectedIndex, byte itemFiltre)
	{
		AItem<TModuleType> item = null;// inventory.items[itemSelectedIndex];

		if (itemFiltre == ItemExtension.FiltreClothe)
			item = this.inventory.Clothes[itemSelectedIndex];
		else if (itemFiltre == ItemExtension.FiltreWeapon)
			item = this.inventory.Weapons[itemSelectedIndex];
		else if (itemFiltre == ItemExtension.FiltreKeys)
			item = this.inventory.Keys[itemSelectedIndex];
		else if (itemFiltre == ItemExtension.FiltreConsommable)
			item = this.inventory.Consommables[itemSelectedIndex];

		return item;
	}

	public void ThrowOut(int itemSelectedIndex, byte itemFiltre)
	{
		//this.itemManagerGUI.ResetItemSelected();
		if (this.DoesItemExistInInventory(itemSelectedIndex, itemFiltre, "throw out"))
		{
			AItem<TModuleType> item = this.GetItemWithFiltre(itemSelectedIndex, itemFiltre);

			if (itemFiltre == ItemExtension.FiltreClothe)
				item = this.inventory.Clothes[itemSelectedIndex];
			if (itemFiltre == ItemExtension.FiltreWeapon)
				item = this.inventory.Weapons[itemSelectedIndex];

			GameObject mesh = item.GetMesh();
			GameObject droppedItem = null;

			if (mesh == null)
				droppedItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
			else
			{
				droppedItem = GameObject.Instantiate(mesh) as GameObject;
				droppedItem.AddComponent<BoxCollider>();
			}

			droppedItem.layer = 0;
			foreach (Transform child in droppedItem.transform)
				child.gameObject.layer = 0;

			droppedItem.transform.Rotate(0, 0, 90);
			droppedItem.AddComponent<Rigidbody>();
			droppedItem.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
			droppedItem.AddComponent<CollectItemFromGround>();
			//A CORRIGERdroppedItem.GetComponent<CollectItemFromGround>().Item = item;
			droppedItem.tag = "Stuff";

			var stuff = item as AStuff<TModuleType>;

			if (stuff != null)
			{
				if (stuff.equipped != e_equipmentEquipped.NOT_EQUIPPED)
					this.Unequip(stuff);
			}
			this.inventory.RemoveItem(item);
		}
	}

	public bool DoesItemExistInInventory(int itemSelectedIndex, byte itemFiltre, string interaction)
	{
		bool itemExistInInventory = false;//itemSelectedIndex <= inventory.items.Count;

		if (itemFiltre == ItemExtension.FiltreAll)
			itemExistInInventory = itemSelectedIndex <= this.inventory.Items.Count;
		else if (itemFiltre == ItemExtension.FiltreClothe)
			itemExistInInventory = itemSelectedIndex <= this.inventory.Clothes.Count;
		else if (itemFiltre == ItemExtension.FiltreWeapon)
			itemExistInInventory = itemSelectedIndex <= this.inventory.Weapons.Count;
		else if (itemFiltre == ItemExtension.FiltreKeys)
			itemExistInInventory = itemSelectedIndex <= this.inventory.Keys.Count;
		else if (itemFiltre == ItemExtension.FiltreConsommable)
			itemExistInInventory = itemSelectedIndex <= this.inventory.Consommables.Count;

		if (!itemExistInInventory)
			base.ModuleManager.ServiceLocator.ErrorDisplayStack.Add("You can't " + interaction + " an item that don't exist in your inventory", e_errorDisplay.Error);

		return itemExistInInventory;
	}

	public void Unequip(AStuff<TModuleType> stuff)
	{
		//this.itemManagerGUI.ResetItemSelected();
		e_equipmentEmplacement emplacement = stuff.equipmentEmplacement;
		bool worked = false;

		if (stuff != this.equipment.GetEquipmentSlot(emplacement).Item)
			base.ModuleManager.ServiceLocator.ErrorDisplayStack.Add("You try to unequip an item that you didnt equipped", e_errorDisplay.Error);
		else
		{
			if (emplacement == e_equipmentEmplacement.Both_Hand)
			{
				if (this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Right_Hand).Equipped && this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Left_Hand).Equipped)// &&
				//inventory.CanAddItem(equipmentSlots[((int)e_equipmentEmplacement.Right_Hand)].item, equipmentSlots[((int)e_equipmentEmplacement.Left_Hand)].item))
				{
					if (this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Left_Hand).Equipped)
						this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Left_Hand).UnequipInSlot(stuff, this.equipment, this);
					if (this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Right_Hand).Equipped)
						this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Right_Hand).UnequipInSlot(stuff, this.equipment, this);
				}
			}
			if (this.equipment.GetEquipmentSlot(emplacement).Equipped)
				worked = true;

			if (worked)
			{
				this.equipment.GetEquipmentSlot(emplacement).UnequipInSlot(stuff, this.equipment, this);
				this.inventory.UnselectAll();
			}
		}
	}

	//ca serait plus generique si equipe renvoyer un boolean pour dire si l'action a fonctionne ou pas, si elle na pas fonctionne ne pas supprimer lobjet par exemple
	public void Equip(AStuff<TModuleType> stuff)
	{
		//this.itemManagerGUI.ResetItemSelected();
		//envoyer lindex au lieu de stuff comme un gros degueulasse
		if (this.attribute.Level >= stuff.levelRequiered)
		{
			e_equipmentEmplacement emplacement = stuff.equipmentEmplacement;
			bool worked = true;
			bool unequip = false;

			if (this.equipment.GetEquipmentSlot(emplacement).Equipped)
				unequip = true;

			if (emplacement == e_equipmentEmplacement.Both_Hand)
			{
				this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Left_Hand).UnequipInSlot(equipment.GetEquipmentSlot(e_equipmentEmplacement.Left_Hand).Item, this.equipment, this);
				this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Right_Hand).UnequipInSlot(equipment.GetEquipmentSlot(e_equipmentEmplacement.Right_Hand).Item, this.equipment, this);
			}
			else if (emplacement == e_equipmentEmplacement.Left_Hand || emplacement == e_equipmentEmplacement.Right_Hand)
				this.equipment.GetEquipmentSlot(e_equipmentEmplacement.Both_Hand).UnequipInSlot(equipment.GetEquipmentSlot(e_equipmentEmplacement.Both_Hand).Item, equipment, this);

			if (unequip)
			{
				AStuff<TModuleType> equippedStuff = equipment.EquipmentSlots[((int)emplacement)].Item;

				if (equippedStuff != null && inventory.CanAddItem(equippedStuff))
					this.equipment.EquipmentSlots[((int)emplacement)].UnequipInSlot(equippedStuff, this.equipment, this);
				else
				{
					base.ModuleManager.ServiceLocator.ErrorDisplayStack.Add("You are too heavy to wear this item", e_errorDisplay.Warning);
					worked = false;
				}
			}

			if (worked)
			{
				this.equipment.AddItemWeight(stuff);
				this.equipment.EquipmentSlots[((int)emplacement)].EquipInSlot(stuff);

				this.inventory.UnselectAll();
				this.attributeInitializer.ModifyPlayerAttributesWithEquipment(stuff, 1);
			}
		}
		else
			base.ModuleManager.ServiceLocator.ErrorDisplayStack.Errors.Add(new ErrorDisplay("You can't wear this item because you don't have the level requiered to use it", e_errorDisplay.Warning));
	}


	public void UseConsommable(int itemIndexSelected)
	{
		//this.itemManagerGUI.ResetItemSelected();
		if (this.inventory.Consommables.Count > itemIndexSelected)
		{
			AConsommable<TModuleType> conso = this.inventory.Consommables[itemIndexSelected];

			//A CORRIGERif (conso.CanUse(this.attribute))
			//A CORRIGER{
			//A CORRIGER	conso.Use(this.attribute);
			//A CORRIGER	this.inventory.RemoveItem(conso);
			//A CORRIGER}
			//A FIXE else
			//A FIXE	ServiceLocator.Instance.ErrorDisplayStack.Add("You have to wait : " + Mathf.Ceil(conso.TimeRequieredToUseIt - this.attribute.TimerForPotion).ToString() + " seconds", e_errorDisplay.Warning);
		}
	}
}
