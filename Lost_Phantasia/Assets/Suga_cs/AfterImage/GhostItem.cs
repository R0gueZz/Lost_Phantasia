
using UnityEngine;
using System.Collections;

/// <summary>
/// Destroy the residual image regularly
/// And with gradual blanking effect
/// </summary>
public class GhostItem : MonoBehaviour
{

    //duration 
    public float duration;
    //Destroy time
    public float deleteTime;
    // The MeshRenderer on the object is mainly to dynamically modify the alpha value of the material color to produce a fade effect
    public MeshRenderer meshRenderer;

    void Update()
    {
        float tempTime = deleteTime - Time.time;
        if (tempTime <= 0)
        {//Destroy when time
            GameObject.Destroy(this.gameObject);
        }
        else if (meshRenderer.material)
        {
            // Here is the effect of fading out the afterimage according to the ratio of the remaining time
            float rate = tempTime / duration;//calculate the ratio of life cycle
            Color cal = meshRenderer.material.GetColor("_GhostColor");
            cal.a *= rate;//Set transparent channel
            meshRenderer.material.SetColor("_GhostColor", cal);
        }

    }
}
