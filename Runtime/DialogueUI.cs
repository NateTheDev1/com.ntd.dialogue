using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;

namespace DynamicDialogueManager.Core
{
    public class DialogueUI
    {
        private UIDocument root;

        private VisualElement dialogueRoot;

        private Dictionary<string, VisualElement> dialogueElements = new Dictionary<string, VisualElement>();

        private bool useTypingDelay = true;

        public void Create(ref UIDocument root)
        {
            this.root = root;

            dialogueRoot = CreateDiv("dialogueRoot");
            this.root.rootVisualElement.Add(dialogueRoot);

            CreateDialogueBox();
        }

        public void AddElements(VisualElement[] elements)
        {
            foreach (VisualElement element in elements)
            {
                dialogueRoot.Add(element);
            }
        }

        public async void StartDialogue(Action dialogueEndCallback = null)
        {
            VisualElement dialogueOptions = dialogueElements["dialogueOptions"] as VisualElement;
            dialogueOptions.Clear();
            dialogueOptions.RemoveFromClassList("fadeInOptions");

            dialogueRoot.style.display = DisplayStyle.Flex;
            dialogueRoot.AddToClassList("comeIn");
            Image characterAvatar = dialogueElements["characterAvatar"] as Image;
            characterAvatar.sprite = DialogueManagerAdapter.Instance.GetNPCMeta(DialogueManagerAdapter.Instance.currentTree.NPCID).avatar;

            Label dialogueText = dialogueElements["dialogueText"] as Label;

            if (useTypingDelay)
            {
                await Task.Delay(1000);
                useTypingDelay = false;
            }

            DisplayWithTypewriterEffect(dialogueText, DialogueManagerAdapter.Instance.currentTree.stages[0].text, () =>
            {
                dialogueOptions.AddToClassList("fadeInOptions");

                foreach (Types.DialogueBranch branch in DialogueManagerAdapter.Instance.currentTree.stages[0].branches)
                {
                    Button button = new Button();
                    button.text = branch.text;
                    button.clickable.clicked += () =>
                    {
                        if (branch.nextStage != null)
                        {
                            DialogueManagerAdapter.Instance.StartDialogue(branch.nextStage.ID);
                        }
                        else
                        {
                            dialogueEndCallback?.Invoke();
                            EndDialogue();
                        }
                    };
                    dialogueOptions.Add(button);
                }

                if (DialogueManagerAdapter.Instance.currentTree.allowCompleteExit)
                {
                    Button exitButton = new Button();
                    exitButton.text = "Exit";
                    exitButton.clickable.clicked += () =>
                    {
                        dialogueEndCallback?.Invoke();
                        EndDialogue();
                    };
                    dialogueOptions.Add(exitButton);
                }
            });
        }

        public void EndDialogue()
        {
            dialogueRoot.RemoveFromClassList("comeIn");
            dialogueRoot.style.display = DisplayStyle.None;
            useTypingDelay = true;
            VisualElement dialogueOptions = dialogueElements["dialogueOptions"] as VisualElement;
            dialogueOptions.Clear();
            Label dialogueText = dialogueElements["dialogueText"] as Label;
            dialogueText.text = "";
            Image characterAvatar = dialogueElements["characterAvatar"] as Image;
            characterAvatar.sprite = null;
        }

        private async void DisplayWithTypewriterEffect(Label element, string text, Action callback)
        {
            element.text = "";

            foreach (char c in text)
            {
                element.text += c;
                await Task.Delay(50);
            }
            callback.Invoke();
        }

        private void CreateDialogueBox()
        {
            var dialogueBox = CreateDiv("dialogueBox");
            dialogueRoot.Add(dialogueBox);

            var characterAvatar = new Image();
            dialogueBox.Add(characterAvatar);

            var dialogueText = new Label();
            dialogueBox.Add(dialogueText);

            var dialogueOptions = CreateDiv("dialogueOptions");
            dialogueBox.Add(dialogueOptions);

            dialogueElements.Add("characterAvatar", characterAvatar);
            dialogueElements.Add("dialogueBox", dialogueBox);
            dialogueElements.Add("dialogueText", dialogueText);
            dialogueElements.Add("dialogueOptions", dialogueOptions);
        }

        public static VisualElement CreateDiv(string name)
        {
            var div = new VisualElement { name = name };
            return div;
        }
    }
}