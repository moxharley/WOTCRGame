using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class Mask : VisualElement
    {
        [UnityEngine.Scripting.Preserve]
        public new class UxmlFactory : UxmlFactory<Mask, UxmlTraits>
        {
        }

        [UnityEngine.Scripting.Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement visualElement, IUxmlAttributes attributes,
                CreationContext creationContext)
            {
                base.Init(visualElement, attributes, creationContext);
            }
        }

        public Mask()
        {
            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent e)
        {
            UnregisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            style.overflow = Overflow.Hidden;
            parent.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
        }

        private void OnGeometryChanged(GeometryChangedEvent e) { UpdateSize(); }

        private void UpdateSize()
        {
            UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            foreach (var child in Children())
            {
                child.style.width = parent.resolvedStyle.width;
                child.style.height = parent.resolvedStyle.height;
            }
        }
    }
}