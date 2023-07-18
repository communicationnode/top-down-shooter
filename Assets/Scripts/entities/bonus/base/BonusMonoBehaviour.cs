using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private VisualProperties visualProperties = new VisualProperties();
    
    [System.Serializable]
    public class VisualProperties
    {
        public float    rotationSpeed   = 1.5f;
        public bool     lockRotation    = false;
    }

    private void FixedUpdate()
    {
        if (!visualProperties.lockRotation)
        transform.Rotate(visualProperties.rotationSpeed, 0, 0);
    }
}
