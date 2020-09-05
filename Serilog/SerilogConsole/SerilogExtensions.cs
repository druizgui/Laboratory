// -----------------------------------------------------------------
// <copyright>Copyright (C) 2020, David Ruiz.</copyright>
// Licensed under the Apache License, Version 2.0.
// You may not use this file except in compliance with the License:
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Software is distributed on an "AS IS", WITHOUT WARRANTIES
// OR CONDITIONS OF ANY KIND, either express or implied.
// -----------------------------------------------------------------

namespace SerilogConsole
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;

    public static class SerilogExtensions
    {
        public static IServiceCollection UseSerilog<T>(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var type = typeof(T);
            return InternalUseSerilog(services, configuration, type);
        }

        public static IServiceCollection UseSerilog(this IServiceCollection services, IConfigurationRoot configuration)
        {
            return InternalUseSerilog(services, configuration);
        }

        private static IServiceCollection InternalUseSerilog(IServiceCollection services, IConfigurationRoot configuration, Type typeContext = null)
        {
            var config = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);
            Log.Logger = config
                .CreateLogger();

            if (typeContext != null) Log.Logger.ForContext(typeContext);

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}