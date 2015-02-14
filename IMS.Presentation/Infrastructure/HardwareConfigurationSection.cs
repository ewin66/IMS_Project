// <copyright file="BarcodeScannerListenerConfigurationSection.cs" company="Nicholas Piasecki"> 
// Copyright (c) 2009 by Nicholas Piasecki All rights reserved. 
// </copyright>

namespace Viktor.IMS.Presentation
{
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The configuration section for the barcode scanner listener.
    /// </summary>
    public class HardwareConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the list of hardware IDs to listen to as barcode scanners.
        /// </summary>
        [ConfigurationProperty("hardwareIds", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(HardwareConfigurationElementCollection))]
        [SuppressMessage(
            "Microsoft.Usage",
            "CA2227",
            Justification = "The setter is required for the configuration classes to deserialize.")]
        public HardwareConfigurationElementCollection HardwareIds
        {
            get { return this["hardwareIds"] as HardwareConfigurationElementCollection; }
            set { this["hardwareIds"] = value; }
        }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <exception cref="ConfigurationErrorsException">if the configuration is malformed</exception>
        /// <returns>the configuration section</returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024",
            Justification = "The method call conveys that this is an expensive operation that may fail.")]
        public static HardwareConfigurationSection GetConfiguration()
        {
            return ConfigurationManager.GetSection("hardware") as HardwareConfigurationSection;
        }
    }
}
