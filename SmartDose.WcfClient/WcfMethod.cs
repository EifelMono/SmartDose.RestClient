﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SmartDose.Core;
using SmartDose.Core.Extensions;

namespace SmartDose.WcfClient
{
    public class WcfMethod
    {
        public string Name { get; set; }

        public MethodInfo Method { get; set; }

        public object Input { get; set; }

        public object Output { get; set; }

        public void CreateInput()
        {
            try
            {
                var classBuilderDefinition = new ClassBuilderDefinition();
                var values = new Dictionary<string, object>();
                foreach (var parameter in Method.GetParameters())
                {
                    if (parameter.ParameterType.IsClass && parameter.ParameterType != typeof(string))
                    {
                        if (parameter.ParameterType.IsArray)
                        {
                            classBuilderDefinition.AddPropertyByType(parameter.Name, parameter.ParameterType,
                                ClassBuilderPropertyCustomAttribute.ListConverter);
                            values[parameter.Name] = Activator.CreateInstance(parameter.ParameterType, 0);
                        }
                        else
                        {
                            classBuilderDefinition.AddPropertyByType(parameter.Name, parameter.ParameterType,
                                ClassBuilderPropertyCustomAttribute.ExpandableObjectConverter);
                            values[parameter.Name] = Activator.CreateInstance(parameter.ParameterType);
                        }
                    }
                    else
                    {
                        classBuilderDefinition.AddPropertyByType(parameter.Name, parameter.ParameterType);
                        values[parameter.Name] = null;
                    }
                }
                Input = ClassBuilder.NewObject(classBuilderDefinition);
                foreach (var property in Input.GetType().GetProperties())
                    try
                    {
                        property.SetValue(Input, values[property.Name]);
                    }
                    catch
                    {
                        property.SetValue(Input, null);
                    }
            }
            catch (Exception ex)
            {
                ex.LogException();
                Input = null;
            }
        }

        public async Task<(bool Ok, object Value)> CallMethodAsync(ICommunicationObject client)
        {
            try
            {
                var values = new List<object>();

                if (Input != null)
                {
                    foreach (var property in Input.GetType().GetProperties())
                    {
                        var value = property.GetValue(Input);
                        if (property.Name == "PageFilter")
                        {
                            if (value is uint ps && ps == 0)
                                value = null;
                        }
                        else
                        if (property.Name == "SortFilter")
                        {
                            if (value is null || value is string sn && string.IsNullOrEmpty(sn))
                                value = null;
                        }
                        values.Add(value);
                    }
                }
                if (Method.ReturnType.Name == "Task")
                {
                    Method.Invoke(client, values.ToArray());
                    return (true, null);
                }
                else
                    return (true, await (dynamic)Method.Invoke(client, values.ToArray()));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return (false, null);
            }
        }

        public override string ToString()
            => Name;
    }
}