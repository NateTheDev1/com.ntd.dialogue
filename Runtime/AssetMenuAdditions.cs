using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using System.IO;

namespace DynamicDialogueManager.Core
{
    public class AssetMenuAdditions
    {
        [MenuItem("GameObject/Modular Dialogue System/Dialogue Manager")]
        public static void CreateMyScriptableObject()
        {
            GameObject dialogue_manager = new GameObject("Dialogue Manager");
            dialogue_manager.AddComponent<DialogueManagerAdapter>();
            dialogue_manager.AddComponent<UIDocument>();
        }
    }
}