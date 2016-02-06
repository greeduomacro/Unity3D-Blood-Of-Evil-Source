using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class AItem<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	protected e_itemEmplacement emplacement;
	protected byte filtre;
	protected byte filtreSkill;
	protected int price;
	protected float weight;
	protected string name;
	protected string description;
	protected bool selected;
	protected string mesh;
	#endregion
	#region Properties
	public e_itemEmplacement Emplacement {	get { return emplacement; }
											private set { emplacement = value; } } 
	public byte Filtre {	get { return filtre; }
							private set { filtre = value; } }
	public byte FiltreSkill	{	get { return filtreSkill; }
								private set { filtreSkill = value; }	}
	public int Price {	get { return price; } 
						set { if (value >= 0) price = value; } } 
	public float Weight {	get { return weight; }
							private set { if (value >= 0) weight = value; } }
	public string Name {	get { return name; }
							set { name = value; } }
	public string Description {	get { return description;} 
								private set { description = value; } }
	public bool Selected {	get { return selected; }
							set { selected = value; } }
	public string Mesh {	get { return mesh; }
							set { mesh = value; }  }
	//public byte FiltreSk
	#endregion
	public AItem()	{
		this.name = "objet";
		this.description = "super objet";
		this.mesh = "Cube";
	}

	public void ModifyItemCategoryIndexes(ref int[] indexes)	{
		if (this is AStuff<TModuleType>)
			indexes[((int)e_itemCategory.Stuff)]++;
		if (this is Keys<TModuleType>)
            indexes[((int)e_itemCategory.Key)]++;
	}

	public GameObject GetMesh()
	{
		if (this.mesh == "Sword") return		ServiceLocator.Instance.ObjectManager.GetObject("Sword");
		if (this.mesh == "LongSword") return	ServiceLocator.Instance.ObjectManager.GetObject("LongSword");
		if (this.mesh == "Key") return			ServiceLocator.Instance.ObjectManager.GetObject("Key");
		if (this.mesh == "Cast") return			ServiceLocator.Instance.ObjectManager.GetObject("Cast");
		if (this.mesh == "Consommable") return	ServiceLocator.Instance.ObjectManager.GetObject("Consommable");
		return null;
	}

}
