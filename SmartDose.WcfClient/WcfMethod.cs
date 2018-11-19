using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SmartDose.Core;

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
            var classBuilderDefinition = new ClassBuilderDefinition();
            foreach (var parameter in Method.GetParameters())
            {
                if (parameter.ParameterType.IsClass && parameter.ParameterType != typeof(string))
                {
                    object o = null;
                    if (parameter.ParameterType.IsArray)
                        o = Activator.CreateInstance(parameter.ParameterType, 0);
                    else
                        o = Activator.CreateInstance(parameter.ParameterType);
                    classBuilderDefinition.AddProperty(parameter.Name, o, ClassBuilderPropertyCustomAttribute.All);

                    //classBuilderDefinition.AddProperty(parameter.Name,
                    //     parameter.ParameterType.IsArray
                    //         ? Activator.CreateInstance(parameter.ParameterType, 0)
                    //         : Activator.CreateInstance(parameter.ParameterType),
                    //     ClassBuilderPropertyCustomAttribute.All);
                }
                else
                    classBuilderDefinition.AddPropertyByType(parameter.Name, parameter.ParameterType);
            }
            Input = ClassBuilder.NewObject(classBuilderDefinition);
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
