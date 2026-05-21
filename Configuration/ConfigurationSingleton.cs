using CSL_SimpleMetrics.Logging;
using System;
using System.IO;
using System.Xml.Serialization;

namespace CSL_SimpleMetrics.Configuration
{
    public class ConfigurationSingleton
    {
        private static ConfigurationSingleton _instance;

        private Models.Configuration _configuration;

        private ConfigurationSingleton()
        {
            Load();
        }

        public static ConfigurationSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigurationSingleton();
            }
            return _instance;
        }

        private void Save()
        {
            StreamWriter stream = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Models.Configuration));
                stream = new StreamWriter(ConfigurationConstants.ConfigurationFileName);
                serializer.Serialize(stream, _configuration);
                Logger.Log("Configuration saved");
            }
            catch (IOException ex)
            {
                Logger.LogException($"Error saving configuration", ex);
            }
            finally
            {
                stream?.Close();
            }
        }

        public void Load()
        {
            FileStream stream = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Models.Configuration));
                stream = new FileStream(ConfigurationConstants.ConfigurationFileName, FileMode.Open);
                _configuration = (Models.Configuration)serializer.Deserialize(stream);
                Logger.Log("Configuration loaded");
            }
            catch (Exception ex)
            {
                Logger.LogException($"Error loading configuration", ex);
            }
            finally
            {
                stream?.Close();
            }
        }

        public Models.Configuration GetConfiguration()
        {
            if (_configuration == null) Load();
            return _configuration;
        }

        public void Update(Models.Configuration newConfiguration)
        {
            _configuration = newConfiguration;
            Save();
        }
    }
}
