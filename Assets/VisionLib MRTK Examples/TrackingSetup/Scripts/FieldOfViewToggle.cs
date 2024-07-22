#if VL_HL_MRTK

using System;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using Visometry.VisionLib.SDK.Core;

namespace Visometry.VisionLib.SDK.MRTK.Examples
{
    /// <summary>
    /// Toggles the "Field Of View" parameter (wide/narrow) through an MRTK button.
    /// Please use the provided prefab `VLFieldOfViewToggle`.
    /// </summary>
    /// @ingroup Examples
    [AddComponentMenu("VisionLib/HoloLens/MRTK/Field Of View Parameter Toggle")]
    [RequireComponent(typeof(PressableButtonHoloLens2))]
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "field_of_view_toggle.html")]
    public class FieldOfViewToggle : MonoBehaviour
    {
        public enum FieldOfViewType
        {
            Wide,
            Narrow
        }

        //Display references
        [SerializeField]
        private TextMeshPro valueText = null;
        [SerializeField]
        private TextMeshPro nameText = null;

        private Interactable toggleSwitch;
        private Interactable ToggleSwitch
        {
            get
            {
                if (this.toggleSwitch == null)
                {
                    this.toggleSwitch = GetComponent<Interactable>();
                }
                return this.toggleSwitch;
            }
        }

        private void Awake()
        {
            this.nameText.text = "Field of View";
            SetToggleState(this.ToggleSwitch.IsToggled);
        }

        private void OnEnable()
        {
            this.ToggleSwitch.OnClick.AddListener(ApplyToggleStateToParameter);
        }

        private void OnDisable()
        {
            this.ToggleSwitch.OnClick.RemoveListener(ApplyToggleStateToParameter);
        }

        private void ApplyToggleStateToParameter()
        {
            SetToggleState(this.ToggleSwitch.IsToggled);
        }

        private void SetToggleState(bool isOn)
        {
            if (!TrackingManager.DoesTrackerExistAndIsInitialized())
            {
                return;
            }

            SetParameterValue(isOn ? FieldOfViewType.Wide :  FieldOfViewType.Narrow);
        }

        private void SetParameterValue(FieldOfViewType newValue)
        {
            var fovString = newValue.ToString().ToLowerInvariant();
            this.valueText.text = fovString;
            TrackingManager.Instance.SetFieldOfView(fovString);
        }

        /// <summary>
        /// Currently supported values (case sensitive): "narrow", "wide" 
        /// </summary>
        public void SetValue(string newValue)
        {
            if (!Enum.TryParse(newValue, out FieldOfViewType fovType))
            {
                throw new ArgumentException(
                    $"Unsupported field of view type \"{newValue}\"");
            }
            SetValue(fovType);
        }
        
        public void SetValue(FieldOfViewType newValue)
        {
            if (newValue != FieldOfViewType.Narrow && newValue != FieldOfViewType.Wide)
            {
                throw new ArgumentException(
                    $"Unsupported field of view type \"{newValue.ToString()}\"");
            }
            SetParameterValue(newValue);
            this.ToggleSwitch.IsToggled = newValue == FieldOfViewType.Wide;
        }
    }
}

#endif
