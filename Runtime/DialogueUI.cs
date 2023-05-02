using UnityEngine.UIElements;

namespace DynamicDialogueManager.Core
{
    public class DialogueUI
    {
        private UIDocument root;

        public void Create(ref UIDocument root)
        {
            this.root = root;
            this.root.rootVisualElement.Add(new Label
            {
                text = "Hello World!",
            });
        }
    }
}