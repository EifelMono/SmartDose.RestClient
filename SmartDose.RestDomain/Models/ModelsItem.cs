using System;

#if RestDomainDev
namespace SmartDose.RestDomainDev.Models
#else
namespace SmartDose.RestDomain.Models
#endif
{
    public class ModelsItem
    {
        public Type Type { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Group { get; set; }
        public override string ToString()
            => FullName;
    }
}
