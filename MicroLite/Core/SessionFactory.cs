﻿// -----------------------------------------------------------------------
// <copyright file="SessionFactory.cs" company="MicroLite">
// Copyright 2012 - 2014 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace MicroLite.Core
{
    using MicroLite.Dialect;
    using MicroLite.Driver;
    using MicroLite.Listeners;
    using MicroLite.Logging;

    /// <summary>
    /// The default implementation of <see cref="ISessionFactory"/>.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("SessionFactory for {ConnectionName}")]
    internal sealed class SessionFactory : ISessionFactory
    {
        private static readonly ILog log = LogManager.GetCurrentClassLog();
        private readonly string connectionName;
        private readonly IDbDriver dbDriver;
        private readonly ISqlDialect sqlDialect;

        internal SessionFactory(SessionFactoryOptions sessionFactoryOptions)
        {
            this.connectionName = sessionFactoryOptions.ConnectionName;
            this.dbDriver = sessionFactoryOptions.DbDriver;
            this.sqlDialect = sessionFactoryOptions.SqlDialect;
        }

        public string ConnectionName
        {
            get
            {
                return this.connectionName;
            }
        }

        public IDbDriver DbDriver
        {
            get
            {
                return this.dbDriver;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This method is provided to create and return an ISession for the caller to use, it should not dispose of it, that is the responsibility of the caller.")]
        public IReadOnlySession OpenReadOnlySession()
        {
            return this.OpenReadOnlySession(ConnectionScope.PerTransaction);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This method is provided to create and return an ISession for the caller to use, it should not dispose of it, that is the responsibility of the caller.")]
        public IReadOnlySession OpenReadOnlySession(ConnectionScope connectionScope)
        {
            if (log.IsDebug)
            {
                log.Debug(Messages.SessionFactory_CreatingReadOnlySession, this.connectionName, this.sqlDialect.GetType().Name);
            }

            return new ReadOnlySession(connectionScope, this.sqlDialect, this.dbDriver);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This method is provided to create and return an ISession for the caller to use, it should not dispose of it, that is the responsibility of the caller.")]
        public ISession OpenSession()
        {
            return this.OpenSession(ConnectionScope.PerTransaction);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "This method is provided to create and return an ISession for the caller to use, it should not dispose of it, that is the responsibility of the caller.")]
        public ISession OpenSession(ConnectionScope connectionScope)
        {
            if (log.IsDebug)
            {
                log.Debug(Messages.SessionFactory_CreatingSession, this.connectionName, this.sqlDialect.GetType().Name);
            }

            return new Session(connectionScope, this.sqlDialect, this.dbDriver, Listener.Listeners);
        }
    }
}