namespace pointcache.ReferenceContainer.Editor {

    using UnityEngine;
    using UnityEditor;
    using pointcache.ReferenceContainer_ReorderableList;
    using UnityEditor.SceneManagement;

    [CustomEditor(typeof(ReferenceContainer), true)]
    public class ReferenceContainerInspector : Editor {

        public SerializedProperty _references;
        void OnEnable() {
            _references = serializedObject.FindProperty("m_references");
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();

            serializedObject.Update();
            ReorderableListGUI.Title("References");
            ReorderableListGUI.ListField<ReferenceContainer.Reference>(((ReferenceContainer)target).m_references, ReferenceContainerReferenceDrawer, ReorderableListFlags.ShowIndices);
            // Were any changes made to the state of `GUI.changed`?
            if (EditorGUI.EndChangeCheck()) {
                // Determine whether changes were made to a specific list item.
                if (ReorderableListGUI.IndexOfChangedItem != -1) {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
                else {
                    // Changes were made outside of an item drawer.
                    // for example, an item was added, removed, moved, etc.
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
        private ReferenceContainer.Reference ReferenceContainerReferenceDrawer(Rect position, ReferenceContainer.Reference item) {
            if (item.name == null)
                item.name = "";
            Rect init = position;
            position.width = position.width / 2f;
            item.name = EditorGUI.TextField(position, item.name);
            Rect gopos = new Rect(position);
            gopos.x = position.width + 70f;
            gopos.width = init.width / 2f;
            item.go = EditorGUI.ObjectField(gopos, item.go, typeof(GameObject), true) as GameObject;

            return item;
        }
    }
}