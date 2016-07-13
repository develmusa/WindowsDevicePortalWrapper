﻿//----------------------------------------------------------------------------------------------
// <copyright file="RestGet.cs" company="Microsoft Corporation">
//     Licensed under the MIT License. See LICENSE.TXT in the project root license information.
// </copyright>
//----------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Tools.WindowsDevicePortal.Tests;

namespace Microsoft.Tools.WindowsDevicePortal
{
    /// <content>
    /// MOCK implementation of HTTP Get
    /// </content>
    public partial class DevicePortal
    {
        /// <summary>
        /// Submits the http get request to the specified uri.
        /// </summary>
        /// <param name="uri">The uri to which the get request will be issued.</param>
        /// /// <param name="validateCertificate">Whether the certificate should be validated.</param>
        /// <returns>Response data as a stream.</returns>
        private async Task<Stream> Get(
            Uri uri, 
            bool validateCertificate = true)
        {
            MemoryStream dataStream = null;

            WebRequestHandler handler = new WebRequestHandler();
            handler.UseDefaultCredentials = false;
            handler.Credentials = this.deviceConnection.Credentials;
            if (validateCertificate)
            {
                handler.ServerCertificateValidationCallback = this.ServerCertificateValidation;
            }
            else
            {
                handler.ServerCertificateValidationCallback = this.ServerCertificateNonValidation;
            }

            using (HttpClient client = new HttpClient(handler))
            {
                this.ApplyHttpHeaders(client, "GET");

                Task<HttpResponseMessage> getTask = TestHelpers.MockHttpResponder.GetAsync(uri);
                await getTask.ConfigureAwait(false);
                getTask.Wait();

                using (HttpResponseMessage response = getTask.Result)
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new DevicePortalException(response);
                    }

                    this.RetrieveCsrfToken(response);

                    using (HttpContent content = response.Content)
                    {
                        dataStream = new MemoryStream();

                        Task copyTask = content.CopyToAsync(dataStream);
                        await copyTask.ConfigureAwait(false);
                        copyTask.Wait();

                        // Ensure we return with the stream pointed at the origin.
                        dataStream.Position = 0;
                    }
                }
            }

            return dataStream;
        }
    }
}
