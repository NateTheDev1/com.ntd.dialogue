# com.ntd.dialogue

## Installation

In Unity navigate to the toolbar and select `Window -> Package Manager`. Click the plus icon and select `Add package from git URL`, then input `https://github.com/NateTheDev1/com.ntd.dialogue` and add it.

## Usage

1. Start by adding a new GameObject with type: `Modular Dialogue System -> Dialogue Manager`

2. To add custom styles use the UI Toolkit Debugger to view element ID's. Then add your stylesheets to the custom styles property in the dialogue manager.

3. In your `Resources` folder add dialogue trees using the scriptable object `DialogueTree`

4. Add the path to the Dialogue Manager ex: "Dialogue" which would be a folder called `Dialogue` in the `Resources` folder.

5. The other thing you will need to add to the dialogue manager is NPC Metadata. These are scriptable objects you can create anywhere in your project, just make sure they are in the list of NPC's of the dialogue manager.

6. You are done! Now to start a dialogue event just call DialogueManagerAdapter.Instance.StartDialogue(int treeID) passing the dialogue tree to

> **Make sure to add a PanelSettings object or the UI will not render.**

## Extras

If you would like to add more to the basic UI, call DialogueManagerAdapter.Instance.AddElements(VisualElement[] elements) to configure your own on top of it.
