using Windows.UI.Xaml;

namespace AdaptiveApp
{
    public class DeviceFamilyTrigger : StateTriggerBase
    {
        private string _deviceFamily;
        public string DeviceFamily
        {
            get { return _deviceFamily; }
            set
            {
                _deviceFamily = value;
                var qualifiers = Windows.ApplicationModel.Resources.Core
                    .ResourceContext.GetForCurrentView().QualifierValues;
                SetActive(qualifiers.ContainsKey("DeviceFamily") &&
                    qualifiers["DeviceFamily"] == _deviceFamily);
            }
        }
    }
}
