// <copyright file="BarcodeScannerListenerConfigurationElement.cs" company="Nicholas Piasecki"> 
// Copyright (c) 2009 by Nicholas Piasecki All rights reserved. 
// </copyright>
namespace Viktor.IMS.Presentation
{
    using System.Configuration;

    /// <summary>
    /// A barcode scanner configuration element.
    /// </summary>
    public class BarcodeScannerListenerConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the hardware ID.
        /// </summary>
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public string Id
        {
            get { return (string)this["id"]; }
            set { this["id"] = value; }
        }
    }
}
