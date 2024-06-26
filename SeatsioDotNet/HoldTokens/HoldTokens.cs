﻿using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using static SeatsioDotNet.Util.RestUtil;

namespace SeatsioDotNet.HoldTokens;

public class HoldTokens
{
    private readonly RestClient _restClient;

    public HoldTokens(RestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<HoldToken> CreateAsync(CancellationToken cancellationToken = default)
    {
        var restRequest = new RestRequest("/hold-tokens", Method.Post);
        return AssertOk(await _restClient.ExecuteAsync<HoldToken>(restRequest, cancellationToken));
    }

    public async Task<HoldToken> CreateAsync(int expiresInMinutes, CancellationToken cancellationToken = default)
    {
        var restRequest = new RestRequest("/hold-tokens", Method.Post)
            .AddJsonBody(new {expiresInMinutes});
        return AssertOk(await _restClient.ExecuteAsync<HoldToken>(restRequest, cancellationToken));
    }

    public async Task<HoldToken> ExpiresInMinutesAsync(string token, int expiresInMinutes, CancellationToken cancellationToken = default)
    {
        var restRequest = new RestRequest("/hold-tokens/{token}", Method.Post)
            .AddUrlSegment("token", token)
            .AddJsonBody(new {expiresInMinutes});
        return AssertOk(await _restClient.ExecuteAsync<HoldToken>(restRequest, cancellationToken));
    }

    public async Task<HoldToken> RetrieveAsync(string token, CancellationToken cancellationToken = default)
    {
        var restRequest = new RestRequest("/hold-tokens/{token}", Method.Get)
            .AddUrlSegment("token", token);
        return AssertOk(await _restClient.ExecuteAsync<HoldToken>(restRequest, cancellationToken));
    }
}