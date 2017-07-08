namespace pointcache.ReferenceContainer {

    using System.Collections.Generic;
    //using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class ReferenceContainer : MonoBehaviour {


        [System.Serializable]
        public class Reference {
            public string name;
            public GameObject go;
        }

        public List<Reference> m_references = new List<Reference>();
        private Dictionary<string, GameObject> m_dict;
        private bool m_initialized;


        void Awake() {
            Initialize();
        }

        void Initialize() {
            if (m_initialized)
                return;
            int count = m_references.Count;
            m_dict = new Dictionary<string, GameObject>(count);
            for (int i = 0; i < count; i++) {
                m_dict.Add(m_references[i].name, m_references[i].go);
            }
            m_initialized = true;
        }

        void OnDestroy() {
            ReferenceContainerHandler.dict.Remove(gameObject);
        }

        public GameObject Get(string name) {
            Initialize();

            GameObject go;
            m_dict.TryGetValue(name, out go);
            return go;
        }

        public T GetFrom<T>(string name) {
            Initialize();
            GameObject go;
            m_dict.TryGetValue(name, out go);
            return go.GetComponent<T>();
        }

        public void SetText(string name, string text) {
            Get(name).GetComponent<Text>().text = text;
        }

        //   public void SetTextMeshPro(string name, string text) {
        //       Get(name).GetComponent<TextMeshProUGUI>().text = text;
        //   }

        public void SetInputFieldText(string name, string text) {
            Get(name).GetComponent<InputField>().text = text;
        }

        public void SetImage(string name, Sprite image) {
            Get(name).GetComponent<Image>().sprite = image;
        }

        public void SetActive(string name, bool active) {
            Get(name).SetActive(active);
        }


    }

    public static class ReferenceContainerHandler {

        public static Dictionary<GameObject, ReferenceContainer> dict = new Dictionary<GameObject, ReferenceContainer>();

        public static ReferenceContainer RefContainer(this GameObject go) {

            ReferenceContainer refc;
            dict.TryGetValue(go, out refc);
            if (((object)refc) == null) {
                refc = go.GetComponent<ReferenceContainer>();
                dict.Add(go, refc);
            }

            return refc;
        }

        public static GameObject RefContainer(this GameObject go, string name) {
            return go.RefContainer().Get(name);
        }
    }
}