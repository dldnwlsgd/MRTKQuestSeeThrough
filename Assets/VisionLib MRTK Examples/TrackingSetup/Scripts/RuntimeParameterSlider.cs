#if VL_HL_MRTK

using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine.Events;
using Visometry.Helpers;
using Visometry.VisionLib.SDK.Core;
using Visometry.VisionLib.SDK.Core.Details;

namespace Visometry.VisionLib.SDK.MRTK.Examples
{
    /// <summary>
    /// Adjusts the given float runtime parameter through an MRTK pinch slider.
    /// Please use the provided prefab `VLRuntimeParameterPinchSlider`.
    /// </summary>
    /// @ingroup Examples
    [AddComponentMenu("VisionLib/HoloLens/MRTK/Runtime Parameter Slider")]
    [RequireComponent(typeof(PinchSlider))]
    [HelpURL(DocumentationLink.APIReferenceURI.Examples + "runtime_parameter_slider.html")]
    public class RuntimeParameterSlider : MonoBehaviour
    {
        public string parameterName;

        [SerializeField]
        private UnityEvent<float> onValueChanged = new UnityEvent<float>();

        public float minValue = 0f;
        public float maxValue = 1f;

        [SerializeField]
        private TextMeshPro valueText = null;
        [SerializeField]
        private TextMeshPro nameText = null;

        private PinchSlider slider;
        private PinchSlider Slider
        {
            get
            {
                if (this.slider == null)
                {
                    this.slider = GetComponent<PinchSlider>();
                }
                return this.slider;
            }
        }

        private void Awake()
        {
            this.nameText.text = this.parameterName;
        }

        private void OnEnable()
        {
            this.Slider.OnValueUpdated.AddListener(OnSliderValueChanged);
        }

        private void OnDisable()
        {
            this.Slider.OnValueUpdated.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(SliderEventData eventData)
        {
            // The SliderValue (between 0 and 1) will be mapped to the corresponding value in the
            // parameters range.
            var mappedValue = MathHelper.Remap(
                this.Slider.SliderValue,
                0f,
                1f,
                this.minValue,
                this.maxValue);
            
            SetParameterValue(mappedValue);
        }

        private void SetParameterValue(float newValue)
        {
            if (!TrackingManager.DoesTrackerExistAndIsInitialized())
            {
                return;
            }

            this.valueText.text = newValue.ToFourDecimalsWithPointInvariant();
            this.onValueChanged.Invoke(newValue);
        }

        public void SetValue(float newValue)
        {
            SetParameterValue(newValue);

            // The newValue will be mapped to a value between 0 and 1 depending on min and max of
            // the parameter.
            this.Slider.SliderValue = MathHelper.Remap(
                newValue,
                this.minValue,
                this.maxValue,
                0f,
                1f);
        }
    }
}

#endif
