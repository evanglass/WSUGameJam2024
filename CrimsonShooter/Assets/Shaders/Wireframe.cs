/*
 * Shader modified from the one at https://github.com/MinaPecheux/unity-tutorials
 */

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(MeshFilter))]
public class Wireframe : MonoBehaviour
{
    [SerializeField]
    private int firstMaterialIndex;

    [SerializeField]
    private Material wireframeMaterial;

    [SerializeField]
    private bool useWireframe = false;

    private List<Material> materials;
    [SerializeField] private bool[] materialsEnabled;
    private bool materialSelectionEnabled = false;

    public void SetSpecificMats(int[] mats, bool enable)
    {
        if(!materialSelectionEnabled)
        {
            materialsEnabled = new bool[materials.Count];
            materialSelectionEnabled = true;
            for(int i = 0; i < materialsEnabled.Length; ++i)
            {
                materialsEnabled[i] = false;
            }
        }
        foreach(int i in mats)
        {
            materialsEnabled[i] = enable;
        }
        UpdateWireframe();
    }

    public bool UseWireframe
    {
        get => useWireframe;
        set
        {
            useWireframe = value;
            UpdateMesh();
            UpdateWireframe();
        }
    }

    private static Color[] _COLORS = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
        };

#if UNITY_EDITOR
    private void OnValidate()
    {
        // (called whenever the object is updated)
        UpdateMesh();
    }
#endif

    private void Awake()
    {
        materials = new List<Material>(GetComponent<Renderer>().materials);
        UpdateMesh();
        UpdateWireframe();
    }

    [ContextMenu("Update Wireframe")]
    private void UpdateWireframe()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material[] renderMaterials = renderer.materials;
        for (int i = firstMaterialIndex; i < renderMaterials.Length; i++)
        {
            if(materialsEnabled != null && materialsEnabled.Length > 0)
            {
                if (useWireframe && materialsEnabled[i])
                {
                    renderMaterials[i] = wireframeMaterial;
                }
                else
                {
                    renderMaterials[i] = materials[i];
                }
            }
            else
            {
                if (useWireframe)
                {
                    renderMaterials[i] = wireframeMaterial;
                }
                else
                {
                    renderMaterials[i] = materials[i];
                }
            }
        }
        renderer.materials = renderMaterials;
    }

    [ContextMenu("Update Mesh")]
    public void UpdateMesh()
    {
        if (!gameObject.activeSelf || !GetComponent<Renderer>().enabled)
            return;

        Mesh m = GetComponent<MeshFilter>().sharedMesh;
        if (m == null) return;

        // compute and store vertex colors for the
        // wireframe shader
        Color[] colors = _SortedColoring(m);

        if (colors != null)
            m.SetColors(colors);
    }

    private Color[] _SortedColoring(Mesh mesh)
    {
        int n = mesh.vertexCount;
        int[] labels = new int[n];

        List<int[]> triangles = _GetSortedTriangles(mesh.triangles);
        triangles.Sort((int[] t1, int[] t2) =>
        {
            int i = 0;
            while (i < t1.Length && i < t2.Length)
            {
                if (t1[i] < t2[i]) return -1;
                if (t1[i] > t2[i]) return 1;
                i += 1;
            }
            if (t1.Length < t2.Length) return -1;
            if (t1.Length > t2.Length) return 1;
            return 0;
        });

        foreach (int[] triangle in triangles)
        {
            List<int> availableLabels = new List<int>() { 1, 2, 3 };
            foreach (int vertexIndex in triangle)
            {
                if (availableLabels.Contains(labels[vertexIndex]))
                    availableLabels.Remove(labels[vertexIndex]);
            }
            foreach (int vertexIndex in triangle)
            {
                if (labels[vertexIndex] == 0)
                {
                    if (availableLabels.Count == 0)
                    {
                        Debug.LogError("Could not find color");
                        return null;
                    }
                    labels[vertexIndex] = availableLabels[0];
                    availableLabels.RemoveAt(0);
                }
            }
        }

        Color[] colors = new Color[n];
        for (int i = 0; i < n; i++)
            colors[i] = labels[i] > 0 ? _COLORS[labels[i] - 1] : _COLORS[0];

        return colors;
    }

    private List<int[]> _GetSortedTriangles(int[] triangles)
    {
        List<int[]> result = new List<int[]>();
        for (int i = 0; i < triangles.Length; i += 3)
        {
            List<int> t = new List<int> { triangles[i], triangles[i + 1], triangles[i + 2] };
            t.Sort();
            result.Add(t.ToArray());
        }
        return result;
    }

}