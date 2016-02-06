using UnityEngine;
using System.Collections;



public class PlayerCharacteristicGUI<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
{
	private PlayerCharacteristics<APlayer> playerCharacteristics;

	void Awake()	{
		this.GUIWindowInitialization(new Rect(0.425f, 0.40f, .15f, 0.55f), true);
	}

	void Start()
	{
		this.playerCharacteristics = base.ModuleManager.Attributes.Characteristics;
	}

	public override void OnGUIDrawWindow(int windowID)
	{
		GUI.skin.box.wordWrap = true;

		for (short i =0; i < 3; i++)
			GUI.Box(MultiResolutions.Rectangle(this.initPosition),
			MultiResolutions.Font(20) + "<color=red><b>Caracteristic</b></color></size>");

		this.isActive = GUIExtension.ExitButton(this.initPosition);

		for (short i =0; i < (int)(e_playerCharacteristic.SIZE); i++)
		{
			GUI.Label(MultiResolutions.Rectangle(this.initPosition.x + 0.01f, this.initPosition.y + 0.1f + i * 0.05f, 0.10f, 0.05f),
			MultiResolutions.Font(16) + (e_playerCharacteristic)(i) + "</size>");

			if (GUIExtension.CollideMousePositionWithRect(MultiResolutions.Rectangle(this.initPosition.x + 0.01f + this.dragPosition.x / Screen.width, initPosition.y + 0.1f + i * 0.05f + this.dragPosition.y / Screen.height, 0.10f, 0.05f)))
				this.playerCharacteristics.Select(i);

			float displayPoint = this.playerCharacteristics.Characteristics[i].TotalPoint;
			switch ((e_playerCharacteristic)(i))
			{
				case e_playerCharacteristic.Strength: displayPoint += base.ModuleManager.Attributes.attributes[(int)(e_entityAttribute.Strength)]; break;
				case e_playerCharacteristic.Resistance: displayPoint += base.ModuleManager.Attributes.attributes[(int)(e_entityAttribute.Resistance)]; break;
				case e_playerCharacteristic.Vitality: displayPoint += base.ModuleManager.Attributes.attributes[(int)(e_entityAttribute.Vitality)]; break;
				case e_playerCharacteristic.Energy: displayPoint += base.ModuleManager.Attributes.attributes[(int)(e_entityAttribute.Energy)]; break;
				default: break;
			}
			GUI.Label(MultiResolutions.Rectangle(this.initPosition.x + 0.09f, this.initPosition.y + 0.1f + i * 0.05f, 0.015f, 0.03f),
			MultiResolutions.Font(16) + this.playerCharacteristics.Characteristics[i].Color +
			displayPoint + "</color></size>");

			//A FIXE 
			//if (this.playerAttribute.CharacteristicRemain > 0 &&
			//	GUI.Button(MultiResolutions.Rectangle(this.initPosition.x + 0.11f, this.initPosition.y + 0.1f + i * 0.05f, 0.015f, 0.03f),
			//	MultiResolutions.Font(16) + "<b><color=#00FF00FF>+</color></b></size>"))
			//	this.playerCharacteristics.SubstractCharacteristic(i);

			if (this.playerCharacteristics.Characteristics[i].PointLevel > 0 &&
				GUI.Button(MultiResolutions.Rectangle(this.initPosition.x + 0.127f, this.initPosition.y + 0.1f + i * 0.05f, 0.015f, 0.03f),
				MultiResolutions.Font(16) + "<b><color=#FF0000FF>-</color></b></size>"))
				this.playerCharacteristics.AddCharacteristic(i);

			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.Box(MultiResolutions.Rectangle(this.initPosition.x, this.initPosition.y + (int)(e_playerCharacteristic.SIZE) * 0.05f + 0.188f, this.initPosition.width, 0.1f),
				((this.playerCharacteristics.Characteristics[i].Selected) ?
				MultiResolutions.Font(15) + this.playerCharacteristics.Characteristics[i].Color + "<b>" +
				this.playerCharacteristics.GetCharacteristicText((e_playerCharacteristic)i) + "</b></color></size>" : ""));
			GUI.skin.box.alignment = TextAnchor.UpperCenter;
		}

		//A FIXEGUI.Box(MultiResolutions.Rectangle(this.initPosition.x, this.initPosition.y + (int)(e_playerCharacteristic.SIZE) * 0.05f + 0.125f, this.initPosition.width, 0.05f),
		//A FIXE	MultiResolutions.Font(18) + "<b><color=red>Usable Point</color></b> : " + playerAttribute.CharacteristicRemain + "</size>");

		if (GUI.Button(MultiResolutions.Rectangle(this.initPosition.x, this.initPosition.y + (int)(e_playerCharacteristic.SIZE) * 0.05f + 0.125f + 0.075f + 0.1f, initPosition.width * 0.5f, 0.05f),
		MultiResolutions.Font(16) + "<b><color=#00FF00FF>Apply</color></b></size>"))
			this.playerCharacteristics.ApplyCharacteristic();

		if (GUI.Button(MultiResolutions.Rectangle(this.initPosition.x + this.initPosition.width * 0.5f, this.initPosition.y + (int)(e_playerCharacteristic.SIZE) * 0.05f + 0.125f + 0.075f + 0.1f, this.initPosition.width * 0.5f, 0.05f),
		MultiResolutions.Font(16) + "<b><color=#FF0000FF>Cancel</color></b></size>"))
			this.playerCharacteristics.CancelCharacteristic();
		//valider change les caracteristique qui faut et enlève

		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
}
