using UnityEngine;
using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public class UIManager : MonoBehaviour
    {
        internal VisualElement Root { get; private set; }

        private void Start()
        {
            Root = GetComponent<UIDocument>().rootVisualElement;
            var controllers = GetComponentsInChildren<BaseController>();
            foreach (var controller in controllers) controller.Bind(Root);
        }
    }
}