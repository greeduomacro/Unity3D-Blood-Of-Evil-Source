using UnityEngine;
using System.Collections;

public class PlayerAttributeGUI<TModuleType> : AGUIWindow<TModuleType> where TModuleType : APlayer
{
	private PlayerAttribute<APlayer> playerAttri;
	
    void Awake () {
		this.playerAttri = base.ModuleManager.Attributes;

		this.GUIWindowInitialization(new Rect(0, 0, 0.105f, 0.095f), true, false);
	}

	public override void OnGUIDrawWindow(int windowID)
	{
		GUI.skin.box.wordWrap = true;
		if (Time.timeScale == 0)
			GUI.Box(MultiResolutions.Rectangle(this.InitPosition), "");

		GUI.backgroundColor = Color.red;
        GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.y, 0.1f, 0.030f), "<color=red>" + MultiResolutions.Font(12) + Mathf.RoundToInt(this.playerAttri.Life.Current) + " / " + Mathf.RoundToInt(this.playerAttri.Life.Max) + "</size></color>");

		GUI.backgroundColor = Color.blue;
		GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.x + 0.030f, 0.1f, 0.030f), "<color=blue>" + MultiResolutions.Font(12) + Mathf.RoundToInt(this.playerAttri.Mana.Current) + " / " + Mathf.RoundToInt(this.playerAttri.Mana.Max) + "</size></color>");

		GUI.backgroundColor = Color.yellow;
        //GUI.Button(MultiResolutions.Rectangle(this.InitPosition.x, this.InitPosition.x + 0.060f, 0.1f, 0.030f), "<color=yellow>" + MultiResolutions.Font(12) + Mathf.RoundToInt(this.playerAttri.Endurance.min) + " / " + Mathf.RoundToInt(this.playerAttri.Endurance.max) + "</size></color>");

		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}

}
