using UnityEngine.UIElements;

namespace Helion4x.Runtime.Gui
{
    public static class VisualElementExtensions
    {
        public static void Hide(this VisualElement element)
        {
            element.style.visibility = Visibility.Hidden;
        }

        public static void Show(this VisualElement element)
        {
            element.style.visibility = Visibility.Visible;
        }
    }
}