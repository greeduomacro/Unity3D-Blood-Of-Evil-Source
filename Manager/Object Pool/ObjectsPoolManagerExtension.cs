using UnityEngine;
using System.Collections;

public static class ObjectPoolManagerExtension
{
	public static void DestroyAPS(this GameObject myobject)								{ ServiceLocator.Instance.ObjectsPoolManager.DestroyObject(myobject); }
	public static void PlayEffect(this GameObject particleObject, int particlesAmount)	{ ServiceLocator.Instance.ObjectsPoolManager.LoadParticuleSystem(particleObject, particlesAmount); }
	public static void PlaySound(this GameObject soundObject)							{ ServiceLocator.Instance.ObjectsPoolManager.LoadAudioSource(soundObject); }
}