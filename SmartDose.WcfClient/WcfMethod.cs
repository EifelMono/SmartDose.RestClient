using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
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
                    classBuilderDefinition.AddProperty(parameter.Name,
                        parameter.ParameterType.IsArray
                            ? Activator.CreateInstance(parameter.ParameterType, 0)
                            : Activator.CreateInstance(parameter.ParameterType),
                        ClassBuilderPropertyCustomAttribute.All);
                }
                else
                    classBuilderDefinition.AddPropertyByType(parameter.Name, parameter.ParameterType);
            }
            Input = ClassBuilder.NewObject(classBuilderDefinition);
        }

        public void Execute()
        {

        }

        public override string ToString()
            => Name;
    }
}
