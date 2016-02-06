using UnityEngine;
using System.Collections;

public class InitializeKey<TModuleType> : MonoBehaviour
{
	#region Attributes
	[SerializeField]	private int keyID;
	[SerializeField]    private string keyName;
	#endregion
	#region Properties
	public int KeyID {	get { return keyID; }
						private set { keyID = value; } }
	public string KeyName {	get { return keyName; }
							private set { keyName = value; } } 
	#endregion

	void Start ()
    {
        CollectItemFromGround collect = gameObject.GetComponent<CollectItemFromGround>() as CollectItemFromGround;
		Keys<APlayer> key = new Keys<APlayer>();
        key.ID = this.keyID;
		key.Name = this.keyName;
		//ACORRIGERcollect.Item = key;
	}
}