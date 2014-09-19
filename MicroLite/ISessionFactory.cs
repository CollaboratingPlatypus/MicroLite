﻿// -----------------------------------------------------------------------
// <copyright file="ISessionFactory.cs" company="MicroLite">
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
namespace MicroLite
{
    using MicroLite.Driver;

    /// <summary>
    /// The interface which specifies the factory options for creating <see cref="ISession"/>s.
    /// </summary>
    public interface ISessionFactory : IHideObjectMethods
    {
        /// <summary>
        /// Gets the name of the connection used by the session factory.
        /// </summary>
        string ConnectionName
        {
            get;
        }

        /// <summary>
        /// Gets the DB driver used by the session factory.
        /// </summary>
        IDbDriver DbDriver
        {
            get;
        }

#if NET_4_5

        /// <summary>
        /// Opens a new asynchronous read-only session to the database using <see cref="ConnectionScope"/>.PerTransaction.
        /// </summary>
        /// <returns>A new asynchronous read-only session instance.</returns>
        IAsyncReadOnlySession OpenAsyncReadOnlySession();

        /// <summary>
        /// Opens a new asynchronous read-only session to the database using the specified <see cref="ConnectionScope"/>.
        /// </summary>
        /// <param name="connectionScope">The connection scope to use for the session.</param>
        /// <returns>A new asynchronous read-only session instance.</returns>
        IAsyncReadOnlySession OpenAsyncReadOnlySession(ConnectionScope connectionScope);

        /// <summary>
        /// Opens a new asynchronous session to the database using <see cref="ConnectionScope"/>.PerTransaction.
        /// </summary>
        /// <returns>A new session instance.</returns>
        IAsyncSession OpenAsyncSession();

        /// <summary>
        /// Opens a new asynchronous session to the database using the specified <see cref="ConnectionScope"/>.
        /// </summary>
        /// <param name="connectionScope">The connection scope to use for the session.</param>
        /// <returns>A new session instance.</returns>
        IAsyncSession OpenAsyncSession(ConnectionScope connectionScope);

#endif

        /// <summary>
        /// Opens a new read-only session to the database using <see cref="ConnectionScope"/>.PerTransaction.
        /// </summary>
        /// <returns>A new read-only session instance.</returns>
#if NET_4_5

        [System.Obsolete(".NET 4.5 apps should fully utilise async actions, consider using the new IAsyncReadOnlySession via OpenAsyncReadOnlySession instead")]
#endif
        IReadOnlySession OpenReadOnlySession();

        /// <summary>
        /// Opens a new read-only session to the database using the specified <see cref="ConnectionScope"/>.
        /// </summary>
        /// <param name="connectionScope">The connection scope to use for the session.</param>
        /// <returns>A new read-only session instance.</returns>
#if NET_4_5

        [System.Obsolete(".NET 4.5 apps should fully utilise async actions, consider using the new IAsyncReadOnlySession via OpenAsyncReadOnlySession instead")]
#endif
        IReadOnlySession OpenReadOnlySession(ConnectionScope connectionScope);

        /// <summary>
        /// Opens a new session to the database using <see cref="ConnectionScope"/>.PerTransaction.
        /// </summary>
        /// <returns>A new session instance.</returns>
#if NET_4_5

        [System.Obsolete(".NET 4.5 apps should fully utilise async actions, consider using the new IAsyncSession via OpenAsyncSession instead")]
#endif
        ISession OpenSession();

        /// <summary>
        /// Opens a new session to the database using the specified <see cref="ConnectionScope"/>.
        /// </summary>
        /// <param name="connectionScope">The connection scope to use for the session.</param>
        /// <returns>A new session instance.</returns>
#if NET_4_5

        [System.Obsolete(".NET 4.5 apps should fully utilise async actions, consider using the new IAsyncSession via OpenAsyncSession instead")]
#endif
        ISession OpenSession(ConnectionScope connectionScope);
    }
}