using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Fault which is used to notify about invalid arguments for a WCF method call.
    /// </summary>
    public class ArgumentFault
    {
        /// <summary>
        /// Gets or sets the name of the invalid argument.
        /// </summary>
        public string ArgumentName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentFault"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the invalid argument</param>
        public ArgumentFault(string argumentName)
        {
            ArgumentName = argumentName;
        }
    }
}
