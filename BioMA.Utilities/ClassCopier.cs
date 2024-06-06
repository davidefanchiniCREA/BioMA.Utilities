using System;
using System.Collections.Generic;
using System.Reflection;

namespace JRC.IPSC.MARS.Utilities
{
    /// <summary>
    /// This class can be used to copy valueType(s) (properties or fields) from a source object
    /// to a target object, regardless of each one's type, as long as the fields (properties) to
    /// be copied have the same name and are public.
    /// This works even if we copy a field to a property or viceversa.
    /// The copy can be fail-fast (meaning it immediately throw Exception if something
    /// goes wrong with a field copy) or not, ignoring the simple copy that goes
    /// wrong and tracing the problem into a log.
    /// They can be also configured to copy only a list of fields (properties) with specified
    /// names.
    /// Finally, if the types of the source field (property) and target field (property) are
    /// different, a conversion between the two types is searched in the
    /// IValueConverters registered with the ValueConverterRegistry
    /// singleton.
    /// The IValueConverter can be registered both for convert or
    /// reverseConvert directions (i.e.: if we need a conversion from
    /// string to int, also a converter from
    /// int to string will do), because the suitable conversion is
    /// searched in both directions.
    /// </summary>
    public class ClassCopier
    {
        private static ValueConverterRegistry _registry =
            ValueConverterRegistry.getInstance();

        private string messageForException = "";

        /// <summary>
        /// Build an instance with a message to be prepended to each traced message and
        /// message of Exceptions raised.
        /// </summary>
        /// <param name="messageForException">Message to be prepended to each traced message and
        /// message of Exceptions raised.</param>
        public ClassCopier(string messageForException)
        {
            this.messageForException = messageForException;
        }

        /// <summary>
        /// Build an instance.
        /// </summary>
        public ClassCopier() { }

        /// <summary>
        /// Copies fields/properties' values from the source to the target object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <param name="fieldsToCopy">A list of fields/properties' names whose values must
        /// be copied from the source to the target object. If null is passed,
        /// the method tries to copy all public source fields and properties to the target.</param>
        /// <param name="ignoreExceptionInCopyingOneField">If true, an error in
        /// copying one field/property is ignored leaving the corresponding field/property
        /// in the target object set to its default value. If false, in case
        /// of problems during the copy of one field/property an ArgumentException
        /// is immediately raised.</param>
        /// <exception cref="ArgumentException">If a property/field with the name of the one
        /// to be copied from the source is not found in the target object, or if there's a more generic problem with
        /// the copy of a single field/property (if ignoreExceptionInCopyingOneField
        /// == true), for example if the types source and target field/property
        /// types are different and a suitable IValueConverter is not found
        /// in the ValueConverterRegistry singleton.</exception>
        public void CopyFields(object source,
                               object target,
                               IList<string> fieldsToCopy,
                               bool ignoreExceptionInCopyingOneField)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            foreach (PropertyInfo propInfo in sourceType.GetProperties())
            {   
                string propName = propInfo.Name;
                if (fieldsToCopy == null || fieldsToCopy.Contains(propName))
                {
                    object valueToSet = propInfo.GetValue(source, null);
                    TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1,
                       messageForException + " copying property: " + propInfo.Name);
                    PropertyInfo targetPropInfo = targetType.GetProperty(propName);
                    // try setting a property...
                    if (targetPropInfo != null)
                    {
                        if (valueToSet == null || valueToSet == DBNull.Value)
                        {
                            targetPropInfo.SetValue(target, null, null);
                        }
                        else
                        {
                            IValueConverter converter =
                                _registry.getConverter(propInfo.PropertyType, targetPropInfo.PropertyType);
                            if (!(converter is DoNothingConverter))
                            {
                                try
                                {
                                    targetPropInfo.SetValue(target, converter.convert(valueToSet), null);
                                }
                                catch (Exception e)
                                {
                                    string s = messageForException +
                                        " error in setting property " + propName + " in class " + targetType.Name;
                                    TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                    if (!ignoreExceptionInCopyingOneField)
                                    {
                                        throw new ArgumentException(s);
                                    }
                                }
                            }
                            else
                            {
                                converter =
                                _registry.getConverter(targetPropInfo.PropertyType, propInfo.PropertyType);
                                if (!(converter is DoNothingConverter))
                                {
                                    try
                                    {
                                        targetPropInfo.SetValue(target, converter.reverseConvert(valueToSet), null);
                                    }
                                    catch (Exception e)
                                    {
                                        string s = messageForException +
                                            " error in setting property " + propName + " in class " + targetType.Name;
                                        TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                        if (!ignoreExceptionInCopyingOneField)
                                        {
                                            throw new ArgumentException(s);
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        targetPropInfo.SetValue(target, valueToSet, null);
                                    }
                                    catch (Exception e)
                                    {
                                        string s = messageForException +
                                            " error in setting property " + propName + " in class " + targetType.Name;
                                        TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                        if (!ignoreExceptionInCopyingOneField)
                                        {
                                            throw new ArgumentException(s);
                                        }
                                    }
                                }
                            }
                        }
                    }// end try setting a property...
                    else
                    {
                        FieldInfo targetFieldInfo = targetType.GetField(propName);
                        // try setting a field
                        if (targetFieldInfo != null)
                        {
                            if (valueToSet == null || valueToSet == DBNull.Value)
                            {
                                targetFieldInfo.SetValue(target, null);
                            }
                            else
                            {
                                IValueConverter converter =
                                    _registry.getConverter(propInfo.PropertyType, targetFieldInfo.FieldType);
                                if (!(converter is DoNothingConverter))
                                {
                                    try
                                    {
                                        targetFieldInfo.SetValue(target, converter.convert(valueToSet));
                                    }
                                    catch (Exception e)
                                    {
                                        string s = messageForException +
                                            " error in setting field " + propName + " in class " + targetType.Name;
                                        TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                        if (!ignoreExceptionInCopyingOneField)
                                        {
                                            throw new ArgumentException(s);
                                        }
                                    }
                                }
                                else
                                {
                                    converter =
                                    _registry.getConverter(targetFieldInfo.FieldType, propInfo.PropertyType);
                                    if (!(converter is DoNothingConverter))
                                    {
                                        try
                                        {
                                            targetFieldInfo.SetValue(target, converter.reverseConvert(valueToSet));
                                        }
                                        catch (Exception e)
                                        {
                                            string s = messageForException +
                                                " error in setting field " + propName + " in class " + targetType.Name;
                                            TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                            if (!ignoreExceptionInCopyingOneField)
                                            {
                                                throw new ArgumentException(s);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            targetFieldInfo.SetValue(target, valueToSet);
                                        }
                                        catch (Exception e)
                                        {
                                            string s = messageForException +
                                                " error in setting field " + propName + " in class " + targetType.Name;
                                            TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                            if (!ignoreExceptionInCopyingOneField)
                                            {
                                                throw new ArgumentException(s);
                                            }
                                        }
                                    }
                                }
                            }
                        }// end try setting a field...
                        else
                        {
                            string s = messageForException + 
                                " no public property or field named " + propName + " in class " + targetType.Name;
                            TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s);
                            if (!ignoreExceptionInCopyingOneField)
                            {
                                throw new ArgumentException(s);
                            }
                        }
                    }
                }
            }// end foreach property in source

            foreach (FieldInfo fieldInfo in sourceType.GetFields())
            {
                string fieldName = fieldInfo.Name;
                object valueToSet = fieldInfo.GetValue(source);
                if (fieldsToCopy == null || fieldsToCopy.Contains(fieldName))
                {
                    TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Verbose, 1,
                        messageForException + " copying field: " + fieldInfo.Name);
                    PropertyInfo targetPropInfo = targetType.GetProperty(fieldName);
                    // try setting a property...
                    if (targetPropInfo != null)
                    {
                        if (valueToSet == null || valueToSet == DBNull.Value)
                        {
                            targetPropInfo.SetValue(target, null, null);
                        }
                        else
                        {
                            IValueConverter converter =
                                _registry.getConverter(fieldInfo.FieldType, targetPropInfo.PropertyType);
                            if (!(converter is DoNothingConverter))
                            {
                                try
                                {
                                    targetPropInfo.SetValue(target, converter.convert(valueToSet), null);
                                }
                                catch (Exception e)
                                {
                                    string s = messageForException +
                                        " error in setting property " + fieldName + " in class " + targetType.Name;
                                    TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                    if (!ignoreExceptionInCopyingOneField)
                                    {
                                        throw new ArgumentException(s);
                                    }
                                }
                            }
                            else
                            {
                                converter =
                                _registry.getConverter(targetPropInfo.PropertyType, fieldInfo.FieldType);
                                if (!(converter is DoNothingConverter))
                                {
                                    try
                                    {
                                        targetPropInfo.SetValue(target, converter.reverseConvert(valueToSet), null);
                                    }
                                    catch (Exception e)
                                    {
                                        string s = messageForException +
                                            " error in setting property " + fieldName + " in class " + targetType.Name;
                                        TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                        if (!ignoreExceptionInCopyingOneField)
                                        {
                                            throw new ArgumentException(s);
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        targetPropInfo.SetValue(target, valueToSet, null);
                                    }
                                    catch (Exception e)
                                    {
                                        string s = messageForException +
                                            " error in setting property " + fieldName + " in class " + targetType.Name;
                                        TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                        if (!ignoreExceptionInCopyingOneField)
                                        {
                                            throw new ArgumentException(s);
                                        }
                                    }
                                }
                            }
                        }
                    }// end try setting a property...
                    else
                    {
                        FieldInfo targetFieldInfo = targetType.GetField(fieldName);
                        // try setting a field
                        if (targetFieldInfo != null)
                        {
                            if (valueToSet == null || valueToSet == DBNull.Value)
                            {
                                targetFieldInfo.SetValue(target, null);
                            }
                            else
                            {
                                IValueConverter converter =
                                    _registry.getConverter(fieldInfo.FieldType, targetFieldInfo.FieldType);
                                if (!(converter is DoNothingConverter))
                                {
                                    try
                                    {
                                        targetFieldInfo.SetValue(target, converter.convert(valueToSet));
                                    }
                                    catch (Exception e)
                                    {
                                        string s = messageForException +
                                            " error in setting field " + fieldName + " in class " + targetType.Name;
                                        TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                        if (!ignoreExceptionInCopyingOneField)
                                        {
                                            throw new ArgumentException(s);
                                        }
                                    }
                                }
                                else
                                {
                                    converter =
                                    _registry.getConverter(targetFieldInfo.FieldType, fieldInfo.FieldType);
                                    if (!(converter is DoNothingConverter))
                                    {
                                        try
                                        {
                                            targetFieldInfo.SetValue(target, converter.reverseConvert(valueToSet));
                                        }
                                        catch (Exception e)
                                        {
                                            string s = messageForException +
                                                " error in setting field " + fieldName + " in class " + targetType.Name;
                                            TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                            if (!ignoreExceptionInCopyingOneField)
                                            {
                                                throw new ArgumentException(s);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            targetFieldInfo.SetValue(target, valueToSet);
                                        }
                                        catch (Exception e)
                                        {
                                            string s = messageForException +
                                                " error in setting field " + fieldName + " in class " + targetType.Name;
                                            TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s + e.ToString());
                                            if (!ignoreExceptionInCopyingOneField)
                                            {
                                                throw new ArgumentException(s);
                                            }
                                        }
                                    }
                                }
                            }
                        }// end try setting a field...
                        else
                        {
                            string s = messageForException +
                                " no public property or field named " + fieldName + " in class " + targetType.Name;
                            TraceUtilities.TraceEvent(System.Diagnostics.TraceEventType.Error, 1000, s);
                            if (!ignoreExceptionInCopyingOneField)
                            {
                                throw new ArgumentException(s);
                            }
                        }
                    }
                }
            }// end foreach field in source
        }

        /// <summary>
        /// Copies fields/properties' values from the source to the target object.
        /// By default, an Exception raised during a single field/property copy
        /// is ignored.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <param name="fieldsToCopy">A list of fields/properties' names whose values must
        /// be copied from the source to the target object. If null is passed,
        /// the method tries to copy all public source fields and properties to the target.</param>
        /// <exception cref="ArgumentException">If a property/field with the name of the one
        /// to be copied from the source is not found in the target object.</exception>
        public void CopyFields(object source, object target, IList<string> fieldsToCopy)
        {
            CopyFields(source, target, fieldsToCopy, true);
        }

        /// <summary>
        /// Copies fields/properties' values from the source to the target object.
        /// By default, all public fields/properties in the source are copied into the target
        /// object and an Exception raised during a single field/property copy
        /// is ignored.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <exception cref="ArgumentException">If a property/field with the name of the one
        /// to be copied from the source is not found in the target object.</exception>
        public void CopyFields(object source, object target)
        {
            CopyFields(source, target, null);
        }

        /// <summary>
        /// Copies fields/properties' values from the source to the target object.
        /// By default, all public fields/properties in the source are copied into the target
        /// object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <param name="target">The target object.</param>
        /// <param name="ignoreExceptionInCopyingOneField">If true, an error in
        /// copying one field/property is ignored leaving the corresponding field/property
        /// in the target object set to its default value. If false, in case
        /// of problems during the copy of one field/property an ArgumentException
        /// is immediately raised.</param>
        /// <exception cref="ArgumentException">If a property/field with the name of the one
        /// to be copied from the source is not found in the target object, or if there's a more generic problem with
        /// the copy of a single field/property (if ignoreExceptionInCopyingOneField
        /// == true), for example if the types source and target field/property
        /// types are different and a suitable IValueConverter is not found
        /// in the ValueConverterRegistry singleton.</exception>
        public void CopyFields(object source, object target, bool ignoreExceptionInCopyingOneField)
        {
            CopyFields(source, target, null, ignoreExceptionInCopyingOneField);
        }
    }
}
