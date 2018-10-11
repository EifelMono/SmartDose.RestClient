using System.Runtime.Serialization;

namespace SmartDose.RestDomain.V2.Model.Production
{
    /// <summary>
    /// Fault which is used to notify about an error during the save operation of entities.
    /// </summary>
    public class EntitySaveFault
    {
        /// <summary>
        /// Gets or sets the error message of the fault.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity that could not be saved.
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySaveFault"/> class.
        /// </summary>
        /// <param name="message">The message that describes the fault.</param>
        /// <param name="entityType">The type of the entity that could not be saved.</param>
        public EntitySaveFault(string message, string entityType)
        {
            EntityType = entityType;
            Message = message;
        }
    }
}
