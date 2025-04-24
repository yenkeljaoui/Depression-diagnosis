using UnityEngine;

public class FlowerInteraction : MonoBehaviour
{
    public MeshRenderer flowerPartRenderer; // MI_LOD0_002
    public MeshRenderer leavesRenderer;     // MI_LOD0_001
    public MeshRenderer branchRenderer;     // MI_LOD0

    public Material bloomingFlowerMaterial;
    public Material wiltedFlowerMaterial;

    public Material bloomingLeavesMaterial;
    public Material wiltedLeavesMaterial;

    public Material bloomingBarkMaterial;
    public Material wiltedBarkMaterial;

    private bool isWilted = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("נכנס: " + other.name);

        if (other.CompareTag("Player") && other.transform.root.gameObject.name == "XR Origin (XR Rig)")
        {
            Debug.Log("יד נגעה בפרח");
            ToggleMaterials();
        }
    }

    void ToggleMaterials()
    {
        // פרחים
        if (flowerPartRenderer != null)
        {
            flowerPartRenderer.material = isWilted ? bloomingFlowerMaterial : wiltedFlowerMaterial;
        }

        // עלים
        if (leavesRenderer != null)
        {
            leavesRenderer.material = isWilted ? bloomingLeavesMaterial : wiltedLeavesMaterial;
        }

        // גזע
        if (branchRenderer != null)
        {
            branchRenderer.material = isWilted ? bloomingBarkMaterial : wiltedBarkMaterial;
        }

        isWilted = !isWilted;
    }
}


