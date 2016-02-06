using UnityEngine;
using System.Collections;

[System.Serializable]
public class Keys<TModuleType> : AItem<TModuleType> where TModuleType : APlayer
{
	#region Attributes
	private int id;
	#endregion
	#region Properties
	public int ID {	get { return id; }
					set { id = value; } }
	#endregion

    public Keys()
    { 
        this.emplacement = e_itemEmplacement.Ground;
	    this.filtre = ItemExtension.FiltreKeys;
        this.weight = 1;
		this.mesh = "Key";
    }
}
