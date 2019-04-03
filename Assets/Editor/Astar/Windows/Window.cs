using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Astar.Editor.Windows
{
    public interface IWindow
    {
        void LoadWindow();
        void DrawWindow();
    }

    public class Window : Object, IWindow
    {
        protected bool Loaded { get; private set; }

        public virtual void LoadWindow()
        {
            Loaded = true;
        }
        public virtual void DrawWindow() { }
    }
}
