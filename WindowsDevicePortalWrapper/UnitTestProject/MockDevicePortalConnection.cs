﻿//----------------------------------------------------------------------------------------------
// <copyright file="MockDevicePortalConnection.cs" company="Microsoft Corporation">
//     Licensed under the MIT License. See LICENSE.TXT in the project root license information.
// </copyright>
//----------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.Tools.WindowsDevicePortal;
using static Microsoft.Tools.WindowsDevicePortal.DevicePortal;

namespace Microsoft.Tools.WindowsDevicePortal.Tests
{
    /// <summary>
    /// Mock implementation of IDevicePortalConnection
    /// </summary>
    public class MockDevicePortalConnection : IDevicePortalConnection
    {
        /// <summary>
        /// Device Certificate
        /// </summary>
        private X509Certificate2 deviceCertificate = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockDevicePortalConnection"/> class.
        /// </summary>
        public MockDevicePortalConnection()
        {
        }

        /// <summary>
        /// Gets Connection property
        /// </summary>
        public Uri Connection
        {
            get
            {
                return new Uri("http://localhost");
            }
        }

        /// <summary>
        /// Gets Web Socket Connection property
        /// </summary>
        public Uri WebSocketConnection
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Gets Credentials property
        /// </summary>
        public NetworkCredential Credentials
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets device family
        /// </summary>
        public string Family
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the device name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets device OS Info
        /// </summary>
        public OperatingSystemInformation OsInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the device's qualified name
        /// </summary>
        public string QualifiedName
        {
            get;
            set;
        }

        /// <summary>
        /// Returns certificate data
        /// </summary>
        /// <returns>certificate data</returns>
        public byte[] GetDeviceCertificateData()
        {
            return this.deviceCertificate.GetRawCertData();
        }

        /// <summary>
        /// Sets certificate data
        /// </summary>
        /// <param name="certificateData">certificate data</param>
        public void SetDeviceCertificate(byte[] certificateData)
        {
            X509Certificate2 cert = new X509Certificate2(certificateData);
            this.deviceCertificate = cert;
        }

        /// <summary>
        /// Xbox will never update the connection.
        /// </summary>
        /// <param name="requiresHttps">https required</param>
        public void UpdateConnection(bool requiresHttps)
        {
            return;
        }

        /// <summary>
        ///  Xbox will never update the connection.
        /// </summary>
        /// <param name="ipConfig">IP info</param>
        /// <param name="requiresHttps">https required</param>
        public void UpdateConnection(IpConfiguration ipConfig, bool requiresHttps)
        {
            return;
        }
    }
}
