using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC_EMAX
{
    public static class Configurations
    {
        /// <summary>
        /// 환경 설정값을 가져옵니다.
        /// </summary>
        /// <param name="key">키를 입력</param>
        /// <returns>설정값을 반환.</returns>
        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 환경 설정값을 셋팅합니다.
        /// </summary>
        /// <param name="key">키를 입력</param>
        /// <param name="value">설정값을 입력</param>
        public static void SetConfig(string key, string value)
        {

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection cfgCollection = config.AppSettings.Settings;

            cfgCollection.Remove(key);
            cfgCollection.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }

        /// <summary>
        /// 환경 설정값을 제거합니다.
        /// </summary>
        /// <param name="key">키를 입력</param>
        public static void RemoveConfig(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection cfgCollection = config.AppSettings.Settings;

            try
            {
                cfgCollection.Remove(key);

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch (Exception ex)
            {
                //Log.getInstance().Error(ex.Message, ex);
            }
        }
    }
}
