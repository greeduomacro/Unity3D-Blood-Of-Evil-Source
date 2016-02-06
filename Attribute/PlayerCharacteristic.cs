using UnityEngine;
using System.Collections;

public enum e_playerCharacteristic
{
	Strength,
	Resistance,
	Vitality,
	Energy,
	SIZE
}

[System.Serializable]
public class PlayerCharacteristic
{
	private e_playerCharacteristic characteristic;
	private int totalPoint;
	private int pointLevel;
	private bool selected;

	private string color;
	#region Properties
	public e_playerCharacteristic Characteristic {	get { return characteristic; } 
													set { if (value == e_playerCharacteristic.Strength)
															color = "<color=orange>";
														  else if (value == e_playerCharacteristic.Resistance)
															color = "<color=green>";
														  else if (value == e_playerCharacteristic.Vitality)
															color = "<color=red>";
														  else if (value == e_playerCharacteristic.Energy)
															color = "<color=blue>";
														  characteristic = value; } }
	public int TotalPoint {	get { return totalPoint; }
							set { if (totalPoint >= 0) totalPoint = value; } }
	public int PointLevel {	get { return pointLevel; }
							set { if (value >= 0) pointLevel = value; } }
	public bool Selected {	get { return selected; }
							set { selected = value; } }
	public string Color {	get { return color; }
							private set { color = value; } }
	#endregion
}
