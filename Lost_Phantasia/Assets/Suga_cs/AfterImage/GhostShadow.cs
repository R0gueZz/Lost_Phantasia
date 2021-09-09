
using UnityEngine;
using System.Collections;

/// <summary>
/// Afterimage effect script
/// </summary>
public class GhostShadow : MonoBehaviour
{
    // Set the color of the afterimage
    [SerializeField]
    Color shadowColor;

    //Afterimage duration
    [SerializeField]
    float duration = 2f;
    //After meeting the requirements, what time interval will create the afterimage of the line
    [SerializeField]
    float interval = 0.1f;

    //Grid data owned by the model
    SkinnedMeshRenderer[] meshRender;

    // Use X-ray shader
    Shader ghostShader;

    void Start()
    {
        // Get all SkinnedMeshRenderers on the model
        meshRender = this.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        // and get X-ray shader
        ghostShader = Shader.Find("Custom/Xray");
    }

    // Timing, and position parameters
    private float lastTime = 0;
    private Vector3 lastPos = Vector3.zero;

    void Update()
    {

        if(Player_Move.rolling || Player_Move.dash)
        {
        //The afterimage is created when the character is displaced, otherwise it is not created
        if (lastPos == this.transform.position)
        {
            return;
        }

        lastPos = this.transform.position;

        // The update time is created at the time that meets the interval
        if (Time.time - lastTime < interval)
        {//Afterimage interval time
            return;
        }
        lastTime = Time.time;

        // If no SkinnedMeshRenderer returns, there is no need to create an afterimage
        if (meshRender == null)
        {

            return;
        }

            // Create all the afterimages of SkinnedMeshRenderer
            for (int i = 0; i < meshRender.Length; i++)
            {
                // Create Mesh and bake SkinnedMeshRenderer's mesh (mesh data)
                Mesh mesh = new Mesh();
                meshRender[i].BakeMesh(mesh);

                // Create an object and set it not to be displayed in the scene
                GameObject go = new GameObject();
                go.hideFlags = HideFlags.HideAndDontSave;

                // Mount the timing destruction script on the object and set the destruction time
                GhostItem item = go.AddComponent<GhostItem>();
                item.duration = duration;
                item.deleteTime = Time.time + duration;

                // Add the MeshFilter component to the object and give the mesh data to the MeshFilter
                MeshFilter filter = go.AddComponent<MeshFilter>();
                filter.mesh = mesh;

                // Add MeshRenderer to the object, assign the corresponding material to the object, mount the X_ray shader, and set the shader parameters
                MeshRenderer meshRen = go.AddComponent<MeshRenderer>();
                meshRen.material = meshRender[i].material;
                meshRen.material.shader = ghostShader;
                meshRen.material.SetFloat("_Pow", 2.0f);
                shadowColor.a = 1;
                meshRen.material.SetColor("_GhostColor", shadowColor);

                // Set the position rotation and ratio of the object, corresponding to the position rotation and ratio of the SkinnedMeshRenderer object
                go.transform.localScale = meshRender[i].transform.localScale;
                go.transform.position = meshRender[i].transform.position;
                go.transform.rotation = meshRender[i].transform.rotation;

                item.meshRenderer = meshRen;
            }       
        }
    }
}
