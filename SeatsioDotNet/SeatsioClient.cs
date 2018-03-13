﻿using RestSharp;
using RestSharp.Authenticators;

namespace SeatsioDotNet
{
    public class SeatsioClient
    {
        private readonly string _secretKey;
        private readonly string _baseUrl;

        public SeatsioClient(string secretKey, string baseUrl)
        {
            _secretKey = secretKey;
            _baseUrl = baseUrl;
        }

        public Subaccounts.Subaccounts subaccounts()
        {
            return new Subaccounts.Subaccounts(createRestClient());
        }

        private RestClient createRestClient()
        {
            var client = new RestClient(_baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(_secretKey, null);
            return client;
        }
    }
}