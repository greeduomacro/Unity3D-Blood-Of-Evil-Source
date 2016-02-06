using UnityEngine;
using System.Collections;

public static class MathExtension {
	public static bool IsBetween(int value, int min, int max)	{
		return value >= min && value <= max;
	}

	public static int GenerateRandomIntBetweenFloat(MinMaxf numberOfItemGenerated)
	{
		float numberOfItemGenerate = numberOfItemGenerated.RandomBetweenValues();

		if (numberOfItemGenerate - Mathf.Floor(numberOfItemGenerate) > Random.Range(0.0f, 1.0f))
			numberOfItemGenerate = numberOfItemGenerate + 1 - (numberOfItemGenerate - Mathf.Floor(numberOfItemGenerate));

		return (int)numberOfItemGenerate;
	}
}
