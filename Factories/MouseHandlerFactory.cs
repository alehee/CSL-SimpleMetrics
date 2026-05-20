using ColossalFramework.UI;
using CSL_SimpleMetrics.Helpers;
using CSL_SimpleMetrics.Logging;
using System;

namespace CSL_SimpleMetrics.Factories
{
    public class MouseHandlerFactory
    {
        private UITabstrip _mainTabstrip = null;

        public MouseHandlerFactory()
        {
            GetUITabstrip();
        }

        private void GetUITabstrip()
        {
            if (_mainTabstrip == null)
            {
                _mainTabstrip = GameObjectHelper.GetUIViewGameObject().FindUIComponent<UITabstrip>("MainToolstrip");
            }
        }

        public MouseEventHandler CreateMouseClickHandler()
        {
            if (_mainTabstrip == null) 
                GetUITabstrip();

            if (_mainTabstrip == null)
                Logger.Log("Could not find MainToolstrip UITabstrip to attach mouse click handler.", LogLevelEnum.Error);

            // TODO
            throw new NotImplementedException();
        }
    }
}
