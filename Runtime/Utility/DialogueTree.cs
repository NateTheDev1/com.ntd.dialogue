using System.Collections.Generic;
using UnityEngine;

namespace DynamicDialogueManager.Types
{
    public enum DialoguerType
    {
        NPC,
        Player
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "DialogueTree", menuName = "Modular Dialogue System/Dialogue Tree", order = 1)]
    public class DialogueTree : ScriptableObject
    {
        public int ID;
        public int NPCID;
        public List<DialogueNode> stages;
        public bool allowCompleteExit;
    }

    [System.Serializable]
    public struct DialogueNode
    {
        public DialoguerType speaker;
        public string text;
        public List<DialogueBranch> branches;
    }

    [System.Serializable]
    public struct DialogueBranch
    {
        public string text;
        public DialogueTree nextStage;
    }
}