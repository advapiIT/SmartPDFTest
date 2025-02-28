﻿using Catel.IoC;
using SmallPdfTest.Repositories;
using SmallPdfTest.Services;

/// <summary>
///     Used by the ModuleInit. All code inside the InitializeResourceGroups method is ran as soon as the assembly is
///     loaded.
/// </summary>
public static class ModuleInitializer
{
    /// <summary>
    ///     Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        // Code added here will be executed as soon as the assembly is loaded by the .net runtime. This
        // is a great opportunity to register any services in the service locator:

        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<ICurrencySettingService, CurrencySettingService>();
        serviceLocator.RegisterType<ICurrencyService, CurrencyService>();
        serviceLocator.RegisterType<ICommonRepository, CommonRepository>();
        serviceLocator.RegisterType<IIsolatedStorageService, IsolatedStorageService>();
    }
}