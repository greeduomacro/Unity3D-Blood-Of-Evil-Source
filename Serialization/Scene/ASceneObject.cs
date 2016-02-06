using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class ASceneObject {
    protected Vector3 position;
    protected Quaternion rotation;

    public ASceneObject() { }

    protected ASceneObject(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
