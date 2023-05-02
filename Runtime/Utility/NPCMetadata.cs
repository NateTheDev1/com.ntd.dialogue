using UnityEngine;
using UnityEditor;

namespace DynamicDialogueManager.Types
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NPCMetadata", menuName = "Modular Dialogue System/NPCMetadata", order = 1)]
    public class NPCMetadata : ScriptableObject
    {
        public int ID;
        public string characterName;
        public Sprite avatar;
    }
}