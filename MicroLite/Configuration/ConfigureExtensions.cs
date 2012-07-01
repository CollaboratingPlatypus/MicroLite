﻿// -----------------------------------------------------------------------
// <copyright file="ConfigureExtensions.cs" company="MicroLite">
// Copyright 2012 Trevor Pilley
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace MicroLite.Configuration
{
    using System;
    using MicroLite.Logging;
    using MicroLite.Mapping;

    /// <summary>
    /// The class used to configure extensions to the MicroLite ORM framework.
    /// </summary>
    internal sealed class ConfigureExtensions : IConfigureExtensions, IHideObjectMethods
    {
        private static readonly ILog log = LogManager.GetLog("MicroLite.Configuration");

        public void SetLogResolver(Func<string, ILog> logResolver)
        {
            log.TryLogInfo(Messages.ConfigureExtensions_UsingLogger, logResolver != null && logResolver.Method != null ? logResolver.Method.ToString() : string.Empty);
            LogManager.GetLogger = logResolver;
        }

        public void SetMappingConvention(IMappingConvention mappingConvention)
        {
            if (mappingConvention == null)
            {
                throw new ArgumentNullException("mappingConvention");
            }

            log.TryLogInfo(Messages.ConfigureExtensions_UsingMappingConvention, mappingConvention.GetType().FullName);
            ObjectInfo.MappingConvention = mappingConvention;
        }
    }
}