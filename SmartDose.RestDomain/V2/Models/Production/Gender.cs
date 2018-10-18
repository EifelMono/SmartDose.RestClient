﻿using System.Runtime.Serialization;


namespace SmartDose.RestDomain.V2.Models.Production
{
    /// <summary>
    /// 
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// the gender is not set, this is the default value used.
        /// </summary>
        Undefined,

        /// <summary>
        /// gender neutral or unimportend
        /// </summary>
        Undifferentiated,

        /// <summary>
        /// Set the gender to male
        /// </summary>
        Male,

        /// <summary>
        /// Set the gender to female
        /// </summary>
        Female,
    }
}