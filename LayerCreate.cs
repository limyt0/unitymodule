using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[InitializeOnLoad]
public class LayerCreate
{

    //STARTUP
    static LayerCreate()
    {
        CreateLayer();
    }


    static void CreateLayer()
    {
        //list or array
        List<string> tags = new List<string> { "LayerA", "LayerB", "LayerC" };
        // Buit-in Layer skip
        int layerskipnum = 8;

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        
#if !UNITY_4
        SerializedProperty layersProp = tagManager.FindProperty("layers");
#endif
        foreach (string tag in tags)
        {
            // check if layer is present
            bool found = false;
            for (int i = 0; i < layersProp.arraySize; i++)
            {
#if UNITY_4
                    string nm = "User Layer " + i;
                    SerializedProperty sp = manager.FindProperty(nm);
#else
                SerializedProperty sp = layersProp.GetArrayElementAtIndex(i);
#endif
                if (sp != null && tag.Equals(sp.stringValue))
                {
                    found = true;
                    break;
                }
            }

            // not found, add into 1st open slot
            if (!found)
            {
                SerializedProperty slot = null;

                // Buit-in Layer skip
                for (int i = layerskipnum; i < layersProp.arraySize; i++)
                {
#if UNITY_4
                        string nm = "User Layer " + i;
                        SerializedProperty sp = manager.FindProperty(nm);
#else
                    SerializedProperty sp = layersProp.GetArrayElementAtIndex(i);
#endif
                    if (sp != null && string.IsNullOrEmpty(sp.stringValue))
                    {
                        slot = sp;
                        break;
                    }
                }

                if (slot != null)
                {
                    slot.stringValue = tag;
                }
                else
                {
                    Debug.Log("Could not find an open Layer Slot for: " + tag);
                }
            }
            
        }

        // save
        tagManager.ApplyModifiedProperties();


    }
}
