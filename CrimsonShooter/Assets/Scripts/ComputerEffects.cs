using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEffects : MonoBehaviour
{
    //public Texture noTex;
    //public void LoseTexturesProps()
    //{
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("EffectSet1");
    //    foreach(GameObject go in gos)
    //    {
    //        if (go.GetComponent<MeshRenderer>())
    //        {
    //            foreach (Material m in go.GetComponent<MeshRenderer>().materials)
    //            {
    //                m.SetTexture("_BaseMap", noTex);
    //                m.SetColor("_BaseColor", Color.white);
    //            }
    //        }
    //    }
    //}
    //
    //public void LoseTexturesFurniture()
    //{
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("EffectSet2");
    //    foreach (GameObject go in gos)
    //    {
    //        if (go.GetComponent<MeshRenderer>())
    //        {
    //            foreach(Material m in go.GetComponent<MeshRenderer>().materials)
    //            {
    //                m.SetTexture("_BaseMap", noTex);
    //                m.SetColor("_BaseColor", Color.white);
    //            }
    //        }
    //    }
    //}
    //
    //public void LoseTexturesEnvironment()
    //{
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("EffectSet3");
    //    foreach (GameObject go in gos)
    //    {
    //        if (go.GetComponent<MeshRenderer>())
    //        {
    //            foreach (Material m in go.GetComponent<MeshRenderer>().materials)
    //            {
    //                m.SetTexture("_BaseMap", noTex);
    //                m.SetColor("_BaseColor", Color.white);
    //            }
    //        }
    //    }
    //}
    //
    //public void LoseTextureCharacters()
    //{
    //    GameObject[] gos = GameObject.FindGameObjectsWithTag("EffectSet4");
    //    foreach (GameObject go in gos)
    //    {
    //        if (go.GetComponent<SkinnedMeshRenderer>())
    //        {
    //            foreach (Material m in go.GetComponent<SkinnedMeshRenderer>().materials)
    //            {
    //                m.SetTexture("_BaseMap", noTex);
    //                m.SetColor("_BaseColor", Color.white);
    //            }
    //        }
    //    }
    //}

    public void WireframeTag(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<Wireframe>())
                go.GetComponent<Wireframe>().UseWireframe = true;
        }
    }
}
