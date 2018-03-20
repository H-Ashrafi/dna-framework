﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Dna
{
    /// <summary>
    /// The main entry point into the Dna Framework library
    /// </summary>
    public static class Framework
    {
        #region Private Members

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        private static IServiceProvider ServiceProvider;

        #endregion

        #region Public Properties

        /// <summary>
        /// The dependency injection service provider
        /// </summary>
        public static IServiceProvider Provider => ServiceProvider;

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public static IConfiguration Configuration => Provider.GetService<IConfiguration>();

        /// <summary>
        /// Gets the default logger
        /// </summary>
        public static ILogger Logger => Provider.GetService<ILogger>();

        /// <summary>
        /// Gets the logger factory for creating loggers
        /// </summary>
        public static ILoggerFactory LoggerFactory => Provider.GetService<ILoggerFactory>();

        /// <summary>
        /// Gets the framework environment
        /// </summary>
        public static FrameworkEnvironment Environment => Provider.GetService<FrameworkEnvironment>();

        /// <summary>
        /// Gets the framework exception handler
        /// </summary>
        public static IExceptionHandler ExceptionHandler => Provider.GetService<IExceptionHandler>();

        #endregion

        #region Public Methods

        /// <summary>
        /// Should be called once a Framework Construction is finished and we want to build it and
        /// start our application
        /// </summary>
        /// <param name="construction">The construction</param>
        /// <param name="logStarted">Specifies if the Dna Framework Started message should be logged</param>
        public static FrameworkConstruction Build(this FrameworkConstruction construction, bool logStarted = true)
        {
            // Build the service provider
            ServiceProvider = construction.Services.BuildServiceProvider();

            // Log the startup complete
            if (logStarted)
                Logger.LogCriticalSource($"Dna Framework started in {Environment.Configuration}...");

            // Return construction for calling ConfigureServices after, when required
            return construction;
        }

        /// <summary>
        /// Shortcut to Framework.Provider.GetService to get an injected service of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">The type of service to get</typeparam>
        /// <returns></returns>
        public static T Service<T>()
        {
            // Use provider to get the service
            return Provider.GetService<T>();
        }

        #endregion
    }
}
