using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Inventory<TModuleType> : AInventory<TModuleType> where TModuleType : APlayer
{
	public Inventory()
	{
		this.size = 100;
		this.maxWeight = 500;
	}
}
[System.Serializable]
public abstract class AInventory<TModuleType> : AItemContainer<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected List<AClothe<TModuleType>> clothes;
	protected List<AWeapon<TModuleType>> weapons;
	protected List<Keys<TModuleType>> keys;
	protected List<AConsommable<TModuleType>> consommables;
	protected int gold;
	#endregion
	#region Properties
	public int Gold {	get { return gold; }
						set { if (value >= 0) gold = value; } }
	public List<AClothe<TModuleType>> Clothes
	{
		get { return clothes; }
									private set { clothes = value; } }
	public List<AWeapon<TModuleType>> Weapons
	{
		get { return weapons; }
									private set { weapons = value; } }
	public List<Keys<TModuleType>> Keys
	{
		get { return keys; }
									private set { keys = value; } }
	public List<AConsommable<TModuleType>> Consommables
	{
		get { return consommables; }
												private set { consommables = value; } }
	#endregion


	public AInventory()
	{
		this.weapons = new List<AWeapon<TModuleType>>();
		this.clothes = new List<AClothe<TModuleType>>();
		this.keys = new List<Keys<TModuleType>>();
		this.consommables = new List<AConsommable<TModuleType>>();
	}

	public override bool AddItem(AItem<TModuleType> item)	
	{
		bool result = base.AddItem(item);

		if (result)
		{
			var stuff = item as AStuff<TModuleType>;
			var key = item as Keys<TModuleType>;
			var conso = item as AConsommable<TModuleType>;

			if (null != stuff)
			{
				var clothe = stuff as AClothe<TModuleType>;
				var weapon = stuff as AWeapon<TModuleType>;

				if (null != clothe)
					this.clothes.Add(clothe);

				if (null != weapon)
					this.weapons.Add(weapon);
			}
			else if (null != key)
				this.keys.Add(key);
			else if (null != conso)
				this.consommables.Add(conso);
		}

		return result;
	}

	public override void RemoveItem(AItem<TModuleType> item)
	{
		base.RemoveItem(item);

		var stuff = item as AStuff<TModuleType>;
		var key = item as Keys<TModuleType>;
		var conso = item as AConsommable<TModuleType>;

		if (null != stuff)
		{
			var clothe = stuff as AClothe<TModuleType>;
			var weapon = stuff as AWeapon<TModuleType>;

			if (null != clothe)
				this.clothes.Remove(clothe);

			if (null != weapon)
				this.weapons.Remove(weapon);
		}
		else if (null != key)
			this.keys.Remove(key);
		else if (null != conso)
			this.consommables.Remove(conso);
	}


	public void Select(int i)
	{
		this.UnselectAll();

		this.items[i].Selected = true;
	}

	public void Select(int i, ref byte itemFiltre, AItem<TModuleType> stuff)
	{
		this.UnselectAll();

		stuff.Selected = true;

		if (stuff.Filtre == ItemExtension.FiltreWeapon)
			itemFiltre = ItemExtension.FiltreWeapon;
		else if (stuff.Filtre == ItemExtension.FiltreClothe)
			itemFiltre = ItemExtension.FiltreClothe;
		else if (stuff.Filtre == ItemExtension.FiltreKeys)
			itemFiltre = ItemExtension.FiltreKeys;
		else if (stuff.Filtre == ItemExtension.FiltreConsommable)
			itemFiltre = ItemExtension.FiltreConsommable;
	}

	public void UnselectAll()
	{
		for (int x = 0; x < this.items.Count; x++)
			this.items[x].Selected = false;
	}
}
