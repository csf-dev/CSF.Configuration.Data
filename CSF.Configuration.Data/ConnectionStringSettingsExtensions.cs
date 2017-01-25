//
// ConnectionStringSettingsExtensions.cs
//
// Author:
//       Craig Fowler <craig@csf-dev.com>
//
// Copyright (c) 2015 CSF Software Limited
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace CSF.Configuration.Data
{
  /// <summary>
  /// Extension methods for the <see cref="ConnectionStringSettings"/> type.
  /// </summary>
  public static class ConnectionStringSettingsExtensions
  {
    /// <summary>
    /// Creates and returns an open <c>IDbConnection</c> instance.
    /// </summary>
    /// <returns>The connection instance.</returns>
    /// <param name="settings">Connection string settings.</param>
    public static IDbConnection CreateAndOpenConnection(this ConnectionStringSettings settings)
    {
      var factory = CreateConnectionFactory(settings);

      var connection = factory();
      connection.Open();

      return connection;
    }

    /// <summary>
    /// Creates and returns a connection factory instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This connection factory is a delegate which is capable of creating <c>IDbConnection</c> instances.
    /// Those instances are not automatically opened, so it is up to the consuming code to open and dispose of
    /// connections.
    /// </para>
    /// </remarks>
    /// <returns>The connection factory delegate.</returns>
    /// <param name="settings">Connection string settings.</param>
    public static Func<IDbConnection> CreateConnectionFactory(this ConnectionStringSettings settings)
    {
      if(settings == null)
      {
        throw new ArgumentNullException (nameof(settings));
      }

      var factory = DbProviderFactories.GetFactory(settings.ProviderName);

      return () => {
        var output = factory.CreateConnection();
        output.ConnectionString = settings.ConnectionString;
        return output;
      };
    }
  }
}

