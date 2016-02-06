using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class DraggableRect : MonoBehaviour {
	private bool		clickedOnLastEvent	= false;

	private Vector2		currentMousePos		= Vector2.zero;
	private Vector2		oldMousePos			= Vector2.zero;


	bool CollideMousePositionWithRect(Rect rect)
	{
		return Input.mousePosition.x >= rect.x &&
				Input.mousePosition.x <= rect.x + rect.width &&
				Screen.height - Input.mousePosition.y >= rect.y &&
				Screen.height - Input.mousePosition.y <= rect.y + rect.height;
	}
	bool CollisionWithSliderListOnWindow(List<Rect> slidersRect)
	{
		foreach (Rect sliderRect in slidersRect)
		{
			if (CollideMousePositionWithRect(sliderRect))
			{
				clickedOnLastEvent = false;
				return true;
			}
		}
		return false;
	}

	public Rect		Drag(Rect windowSize, List<Rect> slidersRect)
	{
		if (clickedOnLastEvent)
		{
			currentMousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y) - oldMousePos;

			windowSize.x += currentMousePos.x;
			windowSize.y += currentMousePos.y;
		}

		if (Input.GetMouseButton(0) &&
			Input.mousePosition.x >= windowSize.x &&
			Input.mousePosition.x <= windowSize.x + windowSize.width &&
			Screen.height - Input.mousePosition.y >= windowSize.y &&
			Screen.height - Input.mousePosition.y <= windowSize.y + windowSize.height)
		{
			if (CollisionWithSliderListOnWindow(slidersRect))
				return windowSize;

			clickedOnLastEvent = true;

			oldMousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		}
		else clickedOnLastEvent = false;

		return windowSize;
	}
}
