using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Helpers
{
    public static class CanvasHelper
    {
        public static void AddListenerToButton(this Canvas canvas, string buttonName, UnityAction call)
        {
            canvas.GetComponentsInChildren<Button>().First(x => x.name == buttonName)
                .onClick.AddListener(call);
        }
    }
}