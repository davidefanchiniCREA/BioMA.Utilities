using System;

using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// Utility to read the modeling solutions configuration file ('ModelingSolutionsConfig.xml' , stored int he application install directory).
    /// </summary>
    public static class ParametersComponentDescriptionsConfigReader
    {

        private static string configFilePath = "ParametersComponentDescriptionsConfig.xml";

      

        /// <summary>
        /// Returns all the content of the Modeling solutions configuration file
        /// </summary>
        /// <returns></returns>
        public static ParametersComponentDescriptionsConfig GetParametersComponentDescriptionsConfigObj()
        {
            StreamReader reader = new StreamReader(configFilePath);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ParametersComponentDescriptionsConfig));
                ParametersComponentDescriptionsConfig show = (ParametersComponentDescriptionsConfig)serializer.Deserialize(reader);
                return show;
            }
            catch (Exception e)
            {
                throw new Exception("Error reading the configuration file '" + configFilePath + "' :" + e.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        //public static string GetControlClassForComponentDescription(IComponentDescription description)
        //{
        //    ParametersComponentDescriptionsConfig configuration = GetParametersComponentDescriptionsConfigObj();
        //    ParametersComponentDescriptionsConfigComponentDescription selection = configuration.ComponentDescription.Where(a => a.ComponentDescriptorType.Equals(description.GetType().FullName)).FirstOrDefault();
        //    if (selection == null) return null;
        //    string controlClass = selection.ParameterEditorType;
        //    return controlClass;
        //}


    }
}