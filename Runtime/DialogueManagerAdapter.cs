using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections.Generic;
using DynamicDialogueManager.Utility;
using DynamicDialogueManager.Types;
using System.IO;

namespace DynamicDialogueManager.Core
{
    public class DialogueManagerAdapter : MonoSingleton<DialogueManagerAdapter>
    {
        private Dictionary<int, NPCMetadata> _npcMetaDatabase = new Dictionary<int, NPCMetadata>();
        private Dictionary<int, DialogueTree> _dialogueDB = new Dictionary<int, DialogueTree>();
        private UIDocument _uiDocument;
        private DialogueUI _dialogueUI;
        [HideInInspector]
        public DialogueTree currentTree;
        public bool useDefaultUI = true;
        public List<NPCMetadata> NPCMetaDatabase = new List<NPCMetadata>();
        public string dialogueResourcePath;
        public StyleSheet rootStyleSheet;

        private void Start()
        {
            if (dialogueResourcePath.Length == 0)
            {
                throw new System.Exception("Dialogue Resource Path is empty");
            }

            CreateDialogueDB();

            FormNPCMetaDatabase();

            if (useDefaultUI)
            {
                HandleUISetup();
            }
        }

        private void CreateDialogueDB()
        {
            List<DialogueTree> trees = new List<DialogueTree>(Resources.LoadAll<DialogueTree>(dialogueResourcePath));
            foreach (DialogueTree tree in trees)
            {
                _dialogueDB.Add(tree.ID, tree);
            }
        }

        public void StartDialogue(int treeID)
        {
            if (_dialogueDB.TryGetValue(treeID, out currentTree))
            {
                if (useDefaultUI)
                {
                    _dialogueUI.StartDialogue();
                }
            }
            else
            {
                Debug.LogError("Dialogue Tree with ID " + treeID + " not found");
            }
        }

        public void Continue(int branchIndex)
        {
            if (currentTree.stages[0].branches.Count > branchIndex)
            {
                currentTree = currentTree.stages[0].branches[branchIndex].nextStage;
                _dialogueUI.StartDialogue();
            }
            else
            {
                Debug.LogError("Branch with index " + branchIndex + " not found");
            }
        }

        public NPCMetadata GetNPCMeta(int id)
        {
            if (_npcMetaDatabase.TryGetValue(id, out NPCMetadata npc))
            {
                return npc;
            }
            else
            {
                Debug.LogError("NPC with ID " + id + " not found");
                return null;
            }
        }

        private void HandleUISetup()
        {
            _dialogueUI = new DialogueUI();
            _uiDocument = GetComponent<UIDocument>();

            if (_uiDocument == null)
            {
                Debug.LogError("UIDocument component not found on Dialogue Manager");
                return;
            }

            _dialogueUI.Create(ref _uiDocument);

            if (rootStyleSheet != null)
            {
                _uiDocument.rootVisualElement.styleSheets.Add(rootStyleSheet);
            }
        }

        private void FormNPCMetaDatabase()
        {
            foreach (NPCMetadata npc in NPCMetaDatabase)
            {
                _npcMetaDatabase.Add(npc.ID, npc);
            }
        }
    }
}