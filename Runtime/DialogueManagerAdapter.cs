using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using DynamicDialogueManager.Utility;

namespace DynamicDialogueManager.Core
{
    public class DialogueManagerAdapter : MonoSingleton<DialogueManagerAdapter>
    {
        private Dictionary<int, NPCMetadata> _npcMetaDatabase = new Dictionary<int, NPCMetadata>();
        private UIDocument _uiDocument;
        private DialogueUI _dialogueUI;

        public List<NPCMetadata> NPCMetaDatabase = new List<NPCMetadata>();

        private void Start()
        {
            FormNPCMetaDatabase();
            HandleUISetup();
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
        }

        private void FormNPCMetaDatabase()
        {
            foreach (NPCMetadata npc in NPCMetaDatabase)
            {
                _npcMetaDatabase.Add(npc.GetInstanceID(), npc);
            }
        }
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "NPCMetadata", menuName = "Modular Dialogue System", order = 1)]
    public class NPCMetadata : ScriptableObject
    {
        public string name;
        public Sprite avatar;
    }
}