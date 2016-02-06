using UnityEngine;
using System.Collections;

public enum e_FillDirection
{
	BottomToTop,
	TopToBottom, //
	LeftToRight, //
	RightToLeft
};

public sealed class GUIExtension : MonoBehaviour 
{
	public static bool		CollideMousePositionWithRect(Rect rect)
	{
		return	Input.mousePosition.x >= rect.x &&
				Input.mousePosition.x <= rect.x + rect.width &&
				Screen.height - Input.mousePosition.y >= rect.y &&
				Screen.height - Input.mousePosition.y <= rect.y + rect.height;
	}

	public static Vector2 GetGUIMousePosition()
	{
		return Event.current.mousePosition;
	}

	public static Vector2 GetGUIMousePositionNormalize()
	{
		Vector2 mousePosition = Event.current.mousePosition;

		mousePosition.x /= MultiResolutions.Width();
		mousePosition.y /= MultiResolutions.Height();

		return mousePosition;
	}

	public static bool IsClicked(Rect rect)
	{
		return Input.GetMouseButtonDown(0) && Input.mousePosition.x >= rect.x &&
				Input.mousePosition.x <= rect.x + rect.width &&
				Screen.height - Input.mousePosition.y >= rect.y &&
				Screen.height - Input.mousePosition.y <= rect.y + rect.height;
	}
	//CA MARHCHE !!
	public static void OnHoverTextCenterWithMousePosition(Rect rectThatNeedToBeOnHover, Rect offsetAndSize, string label, GUIStyle style = null)
	{
		if (MultiResolutions.Rectangle(rectThatNeedToBeOnHover).Contains(GetGUIMousePosition()))
		{
			Vector2 mousePosition = GetGUIMousePositionNormalize();
			Rect rect = MultiResolutions.Rectangle(new Rect(
				offsetAndSize.x + mousePosition.x - (offsetAndSize.width * 0.5f),
				offsetAndSize.y + mousePosition.y - offsetAndSize.height,
				offsetAndSize.width,
				offsetAndSize.height));

			GUI.Box(rect, "", style);
			GUI.Box(rect, "", style);
			GUI.Box(rect, label, style);
		}
	}
	public static void OnHoverTextWithMousePosition(Rect colliderRect, Rect hoverRect, string label, string GUIStyle = null)
	{
		if (CollideMousePositionWithRect(colliderRect))
		{
			for (uint i = 0; i < 3; i++)
			{
				if (GUIStyle != "")
					GUI.Box(new Rect(colliderRect.x + hoverRect.x, colliderRect.y + hoverRect.y - hoverRect.height,
						hoverRect.width, hoverRect.height), label, GUIStyle);
				else
					GUI.Box(new Rect(colliderRect.x + hoverRect.x, colliderRect.y + hoverRect.y - hoverRect.height,
					hoverRect.width, hoverRect.height), label);
			}
		}
	}
	public static void OnHoverTextWithCenterMousePosition(Rect colliderRect, Rect baseRect, string label, string GUIStyle = null,
			float lineHeight = 0.0f, int whichLine = 0, float width = 0.15f, float heightFactor = 1.0f)
	{
		if (CollideMousePositionWithRect(colliderRect))
		{
			Rect hoverRect = new Rect(
				Input.mousePosition.x - (baseRect.width * 0.5f * MultiResolutions.Width()),
				(MultiResolutions.Height() - Input.mousePosition.y) - (baseRect.y + lineHeight * whichLine) * MultiResolutions.Height(),
				width * MultiResolutions.Width(),
				(baseRect.height) * MultiResolutions.Height() * 2 * heightFactor);

			for (uint i = 0; i < 3; i++)
			{
				if (GUIStyle != "")
					GUI.Box(new Rect(colliderRect.x + hoverRect.x, colliderRect.y + hoverRect.y - hoverRect.height,
						hoverRect.width, hoverRect.height), label, GUIStyle);
				else
					GUI.Box(new Rect(colliderRect.x + hoverRect.x, colliderRect.y + hoverRect.y - hoverRect.height,
					hoverRect.width, hoverRect.height), label);
			}
		}
	}
	public static void		OnHoverText(Rect colliderRect, Rect hoverRect, string label, string GUIStyle = null)
	{
		if (CollideMousePositionWithRect(colliderRect))
			for (uint i = 0; i < 3; i++)
			{
				if (GUIStyle != "")
					GUI.Box(new Rect(colliderRect.x + hoverRect.x, colliderRect.y + hoverRect.y - hoverRect.height,
						hoverRect.width, hoverRect.height), label, GUIStyle);
				else
					GUI.Box(new Rect(colliderRect.x + hoverRect.x, colliderRect.y + hoverRect.y - hoverRect.height,
					hoverRect.width, hoverRect.height), label);
			}
	}
	public static bool		ExitButton(Rect rect, bool calculateWhereIsTheRect = true)
	{
		bool	GUIState = false;

		if (calculateWhereIsTheRect)
			rect = MultiResolutions.Rectangle(rect.x + rect.width - 0.015f, rect.y, 0.015f, 0.03f);

		if (GUI.Button(rect, "<size=" + MultiResolutions.Font(30) + "<color=red><b>X</b></color>" + "</size>"))	
			GUIState = true;

		OnHoverText(rect, MultiResolutions.Rectangle(-0.018f, 0, 0.05f, 0.03f), "<size=" + MultiResolutions.Font(17) + "<b><i><color=red>Exit Menu</color></i></b></size>");

		return !GUIState;
	}

	public static void DrawTexutrePart(Rect rect, e_FillDirection direction, float fullPercent, Texture2D texture, Material material = null)
	{
		Vector2 offset = new Vector2(0.0f, 0.0f);

		switch (direction)
		{
			case e_FillDirection.TopToBottom:
				offset.y = rect.height * (1.0f - fullPercent);
				break;

			case e_FillDirection.BottomToTop:
				offset.y = -(rect.height * (1.0f - fullPercent));
				break;

			case e_FillDirection.LeftToRight :
				offset.x = -(rect.width * (1.0f - fullPercent));
				break;

			case e_FillDirection.RightToLeft :
				offset.x = rect.width * (1.0f - fullPercent);
				break;

			default:
				break;
		}

		if (direction == e_FillDirection.TopToBottom || direction == e_FillDirection.RightToLeft)
		{
			GUI.BeginGroup(MultiResolutions.Rectangle(rect.x + offset.x, rect.y + offset.y, rect.width - offset.x, rect.height - offset.y));
			if (null != material)
				Graphics.DrawTexture(MultiResolutions.Rectangle(-offset.x, -offset.y, rect.width, rect.height), texture, material);
			else
				GUI.DrawTexture(MultiResolutions.Rectangle(-offset.x, -offset.y, rect.width, rect.height), texture);
			GUI.EndGroup();
		}
		else
		{
			GUI.BeginGroup(MultiResolutions.Rectangle(rect.x, rect.y, rect.width + offset.x, rect.height + offset.y));
			if (null != material)
				Graphics.DrawTexture(MultiResolutions.Rectangle(0, 0, rect.width, rect.height), texture, material);
			else
				GUI.DrawTexture(MultiResolutions.Rectangle(0, 0, rect.width, rect.height), texture);
			GUI.EndGroup();
		}
	}

	public static bool NecromancerExitButton(Rect rect)
	{
		bool GUIState = false;
		Rect rectNormal = rect;
		rect = MultiResolutions.Rectangle(rectNormal);

		if (GUI.Button(rect, "<size=" + MultiResolutions.Font(30) + "<color=red><b>X</b></color>" + "</size>", "details"))
			GUIState = true;

		if (rect.Contains(Input.mousePosition))
		{
			Rect rectHover = new Rect(rectNormal.x - 0.018f, rectNormal.y, rectNormal.width + 0.05f, rectNormal.height + 0.03f);
			GUI.Box(MultiResolutions.Rectangle(ref rectHover), "<size=" + MultiResolutions.Font(17) + "<b><i><color=red>Exit Menu</color></i></b></size>", "details");
		}

		return !GUIState;
	}

	public static string GetColorOfValue(float difference)
	{
		string color = "<color=white>";

		if (difference < 0)
			color = "<color=red>";
		else if (difference > 0)
			color = "<color=#00FF00FF>";

		return color;
	}

	public static string IfNegativeReturnRedIfPositiveReturnGreen(float val, bool whiteParentesis = false)
	{
		if (val < 0)
		{
			if (whiteParentesis)
				return "<color=white>(</color><color=red>" + val.ToString() + "</color><color=white>)</color>";
			return "<color=red>" + val.ToString() + "</color>";
		}
		if (val > 0)
		{
			if (whiteParentesis)
				return "<color=white>(</color><color=#00FF00FF>+" + val.ToString() + "</color><color=white>)</color>";
			return "<color=#00FF00FF>+" + val.ToString() + "</color>";
		}

		return "";
	}
	public static string GreenRedWhiteColorDependValue(float val, bool whiteParentesis = false)
	{
		if (val < 0)
		{
			if (whiteParentesis)
				return "<color=white>(</color><color=red>" + val.ToString() + "</color><color=white>)</color>";
			return "<color=red>" + val.ToString() + "</color>";
		}
		if (val > 0)
		{
			if (whiteParentesis)
				return "<color=white>(</color><color=#00FF00FF>" + val.ToString() + "</color><color=white>)</color>";
			return "<color=#00FF00FF>" + val.ToString() + "</color>";
		}

		return "0";
	}

	public static bool ButtonAndHoverToChangeText(Rect rect, string buttonText, string hoverText)
	{
		bool haveClickedOnButton = false;

		if (CollideMousePositionWithRect(rect))
			haveClickedOnButton = GUI.Button(rect, hoverText);
		else
			haveClickedOnButton = GUI.Button(rect, buttonText);

		return haveClickedOnButton;
	}

	public static void AddSpikes(int winX, float height)
	{
		float spiky =  Mathf.Floor(winX - 152)/22;
		//float spikeCount = Mathf.Floor(winX - 152)/22;
		GUILayout.BeginArea(MultiResolutions.Rectangle(0.0215f, height * 0.09f, 1f, 1f));
		GUILayout.BeginHorizontal();
		GUILayout.Label ("", "SpikeLeft");//-------------------------------- custom
		for (var i = 0; i < spiky; i++)
				GUILayout.Label ("", "SpikeMid");//-------------------------------- custom
		GUILayout.Label ("", "SpikeRight");//-------------------------------- custom
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	public static void FancyTop(int topX)
	{
		float leafOffset = (topX * 0.5f) - 64;
		float frameOffset = (topX * 0.5f) -  27;
		float skullOffset = (topX * 0.5f) - 20;
		GUI.Label(new Rect(leafOffset, 18, 0, 0), "", "GoldLeaf");//-------------------------------- custom	
		GUI.Label(new Rect(frameOffset, 3, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(skullOffset, 12, 0, 0), "", "Skull");//-------------------------------- custom	
		//GUI.Box(new Rect(leafOffset, 18, 0, 0), base.ModuleManager.ServiceLocator.TextureManager.GetGUITexture("GoldLeaf"), null);//-------------------------------- custom	
		//GUI.Box(new Rect(frameOffset, 3, 0, 0), base.ModuleManager.ServiceLocator.TextureManager.GetGUITexture("IconFrame"), null);//-------------------------------- custom	
		//GUI.Box(new Rect(skullOffset, 12, 0, 0), base.ModuleManager.ServiceLocator.TextureManager.GetGUITexture("Skull"), null);//-------------------------------- custom	

	}

	public static void WaxSeal(int x, int y)
	{
		int WSwaxOffsetX = x - 120;
		int WSwaxOffsetY = y - 115;
		int WSribbonOffsetX = x - 114;
		int WSribbonOffsetY = y - 83;
	
		GUI.Label(new Rect(WSribbonOffsetX, WSribbonOffsetY, 0, 0), "", "RibbonBlue");//-------------------------------- custom	
		GUI.Label(new Rect(WSwaxOffsetX, WSwaxOffsetY, 0, 0), "", "WaxSeal");//-------------------------------- custom	
	}

	public static void MyDeathBadge(int x, int y)
	{
		x += 2;
		y += 12;
	
		int RibbonOffsetX = x - 115;
		int FrameOffsetX = x - 115;
		int SkullOffsetX = x - 108;
		int RibbonOffsetY = y- 95;
		int FrameOffsetY = y - 120;
		int SkullOffsetY = y - 110;
	
		GUI.Label(new Rect(RibbonOffsetX, RibbonOffsetY, 0, 0), "", "RibbonRed");//-------------------------------- custom	
		GUI.Label(new Rect(FrameOffsetX, FrameOffsetY, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(SkullOffsetX, SkullOffsetY, 0, 0), "", "Skull");//-------------------------------- custom	
	}

	public static void DeathBadge(int x, int y)
	{
		int RibbonOffsetX = x;
		int FrameOffsetX = x+3;
		int SkullOffsetX = x+10;
		int RibbonOffsetY = y+22;
		int FrameOffsetY = y;
		int SkullOffsetY = y+9;
	
		GUI.Label(new Rect(RibbonOffsetX, RibbonOffsetY, 0, 0), "", "RibbonRed");//-------------------------------- custom	
		GUI.Label(new Rect(FrameOffsetX, FrameOffsetY, 0, 0), "", "IconFrame");//-------------------------------- custom	
		GUI.Label(new Rect(SkullOffsetX, SkullOffsetY, 0, 0), "", "Skull");//-------------------------------- custom	
	}

	public static bool CreatNecromancerWindow(ref Rect rect, string title)
	{
		Font oldFont = GUI.skin.label.font;
		GUI.skin.label.font = ServiceLocator.Instance.FontManager.Get("Necromancer");

		GUI.DrawTexture(MultiResolutions.Rectangle(0, 0, rect.width, rect.height), ServiceLocator.Instance.TextureManager.GetGUITexture("window"));
		GUIExtension.AddSpikes((int)((rect.width) * Screen.width), rect.height);
		GUIExtension.FancyTop((int)((/*this.initPosition.x + */rect.width) * Screen.width));
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;

		Rect labelRect = new Rect(rect.x + rect.width * 0.1f, rect.y + 0.1f, rect.width * 0.8f, 0.04f);
		GUI.Label(MultiResolutions.Rectangle(labelRect), "<b>" + title + "</b>");
		
		GUILayout.BeginArea(MultiResolutions.Rectangle(new Rect(labelRect.x, labelRect.y + 0.045f, labelRect.width, 0.05f)));
		GUILayout.Label("", "Divider");
		GUILayout.EndArea();

		GUI.skin.label.font = oldFont;

		return true;
		//return !GUI.Button(MultiResolutions.Rectangle(new Rect(
		//    rect.x + rect.width - rect.width * 0.08f,
		//    rect.y + 0.015f,
		//    rect.width * 0.03f,
		//    rect.height * 0.05f)), "<color=#FF0000FF><b>X</b></color>", "details");
	}

	public static bool YesButton(ref Rect rect, GUIStyle style, LanguageManager languageManager)
	{
		for (byte i = 0; i < 3; i++)
			GUI.Box(MultiResolutions.Rectangle(rect), "" , style);

		return GUI.Button(MultiResolutions.Rectangle(rect), 
			"<b><color=#FF0000>" + MultiResolutions.Font(18) + languageManager.GetText("YES") + 
			"</size></color></b>",
			style);
	}

	public static bool NoButton(ref Rect rect, GUIStyle style, LanguageManager languageManager)
	{
		for (byte i = 0; i < 3; i++)
			GUI.Box(MultiResolutions.Rectangle(rect), "", style);

		return GUI.Button(MultiResolutions.Rectangle(rect),
			"<b><color=#00FF00>" + MultiResolutions.Font(18) + languageManager.GetText("NO") +
			"</size></color></b>",
			style);
	}

	public static void CreateNecromancerWindowWithTitle(string title, Rect rect, APlayer player, ref Rect initPosition)
	{
		Font oldFont = GUI.skin.label.font;
		GUI.skin.label.font = player.ServiceLocator.FontManager.Get("Necromancer");

		GUI.DrawTexture(MultiResolutions.Rectangle(0, 0, initPosition.width, initPosition.height), player.ServiceLocator.TextureManager.GetGUITexture("window"));
		GUIExtension.AddSpikes((int)((initPosition.width) * Screen.width), initPosition.height);
		GUIExtension.FancyTop((int)((/*this.initPosition.x + */initPosition.width) * Screen.width));

		GUILayout.BeginArea(MultiResolutions.Rectangle(ref rect));
		GUILayout.Space(Screen.height * 0.1f);
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUILayout.Label("<b>" + player.LanguageManager.GetText(title) + "</b>");
		GUILayout.Label("", "Divider");
		GUILayout.EndArea();

		GUI.skin.label.font = oldFont;
	}
}