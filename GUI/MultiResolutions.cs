using UnityEngine;
using System.Collections;



public sealed class MultiResolutions : MonoBehaviour 
{
	private const float resolutionWidth = 1920; //1536 | 1920
	private const float resolutionHeight = 892; //864 | 892


	public static Matrix4x4 GetGUIMatrix() 
	{ Vector3 scale = Vector3.one; scale.x = Screen.width / resolutionWidth; scale.y = Screen.height / resolutionHeight; return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale); }
	public static Matrix4x4 GetSpecificGUIMatrix(float scaleXY)
	{
		Vector3 scale = new Vector3(
			(Screen.width / resolutionWidth) * scaleXY,
			(Screen.height / resolutionHeight) * scaleXY,
			1);

		return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
	}
	public static float Width()											{ return resolutionWidth; }
	public static float Height()										{ return resolutionHeight; }
	public static float CalculWidth(float width)						{ return width / resolutionWidth; }
	public static float CalculHeight(float height)						{ return height / resolutionHeight; }
	public static float CalculInverseHeight(float height)				{ return height * resolutionHeight; }
	public static float CalculInverseWidth(float width)					{ return width * resolutionWidth; }
	public static Rect CalcRect(Rect rect)								{ return new Rect(CalculWidth(rect.x), CalculHeight(rect.y), CalculWidth(rect.width), CalculHeight(rect.height)); }
	public static Rect Rectangle(Rect rect) { return Rectangle(rect.x, rect.y, rect.width, rect.height); }
	public static Rect Rectangle(ref Rect rect) { return Rectangle(rect.x, rect.y, rect.width, rect.height); }
	public static Rect Rectangle(float x, float y, float w, float h)	{ return new Rect(resolutionWidth * x, resolutionHeight * y, resolutionWidth * w, resolutionHeight * h); }
	public static Rect Unrectangle(Rect rect)							{ return Rectangle(rect.x, rect.y, rect.width, rect.height); }
	public static Rect Unrectangle(float x, float y, float w, float h)	{ return new Rect(resolutionWidth / x, resolutionHeight / y, resolutionWidth / w, resolutionHeight / h); }

	public static Vector2 Vec2(float w, float h)						{ return new Vector2(Screen.width * w, Screen.height * h); }
	public static string Font(float fontSize)							{ return "<size=" + fontSize.ToString() + ">";	}
}
