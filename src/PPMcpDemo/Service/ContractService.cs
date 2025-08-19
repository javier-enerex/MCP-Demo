using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using PPMcpDemo.Models;
using System.Text;

namespace PPMcpDemo.Service;

public class ContractService : IContractService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ContractService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<PagedResult<VMContract>> GetContractAsync(VMContractFilter filter, CancellationToken cancellationToken = default)
    {
        var baseUrl = $"{_config["PropellerApi:BaseUrl"]}/contract/summarized";

        var kvps = new List<KeyValuePair<string, string?>>();

        if (filter?.SalesPerson?.Count > 0)
            foreach (var v in filter.SalesPerson)
                kvps.Add(new("SalesPerson", v));

        //if (filter?.SignatureMethod is short sig)
        //kvps.Add(new("SignatureMethod", sig.ToString()));

        if (filter?.SignDateIni is DateTime signDateIni)
            kvps.Add(new("SignDateIni", signDateIni.ToString("o")));

        if (filter?.SignDateEnd is DateTime signDateEnd)
            kvps.Add(new("SignDateEnd", signDateEnd.ToString("o")));

        if (filter?.CreatedDateIni is DateTime createdDateIni)
            kvps.Add(new("CreatedDateIni", createdDateIni.ToString("o")));

        if (filter?.CreatedDateEnd is DateTime createdDateEnd)
            kvps.Add(new("CreatedDateEnd", createdDateEnd.ToString("o")));

        if (filter?.Status?.Count > 0)
            foreach (var v in filter.Status)
                kvps.Add(new("Status", v.ToString()));

        if (filter?.ServiceType?.Count > 0)
            foreach (var v in filter.ServiceType)
                kvps.Add(new("ServiceType", v.ToString()));

        if (!string.IsNullOrWhiteSpace(filter?.Name))
            kvps.Add(new("Name", filter.Name));

        if (!string.IsNullOrWhiteSpace(filter?.NextCursor))
            kvps.Add(new("NextCursor", filter.NextCursor));

        var url = QueryHelpers.AddQueryString(baseUrl, kvps);

        using var request = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = JsonConvert.DeserializeObject<PagedResult<VMContract>>(json);

        return result ?? new PagedResult<VMContract>
        {
            Items = Enumerable.Empty<VMContract>(),
            NextCursor = null,
            Total = 0
        };
    }

    public async Task<IEnumerable<VMContractTemplate>> GetContractTemplateAsync(VMContractTemplateFilter filter, CancellationToken cancellationToken = default)
    {
        var baseUrl = $"{_config["PropellerApi:BaseUrl"]}/contract/templates";

        var kvps = new List<KeyValuePair<string, string?>>();

        if (filter?.OpportunityExternalID != Guid.Empty)
            kvps.Add(new("OpportunityExternalID", filter.OpportunityExternalID.ToString()));

        if (filter?.CustomPricingExternalID != null)
            kvps.Add(new("CustomPricingExternalID", filter.CustomPricingExternalID.ToString()));

        if (filter?.CustomPricingScenarioExternalID != null)
            kvps.Add(new("CustomPricingScenarioExternalID", filter.CustomPricingScenarioExternalID.ToString()));

        if (!string.IsNullOrWhiteSpace(filter?.TemplateName))
            kvps.Add(new("TemplateName", filter.TemplateName));

        var url = QueryHelpers.AddQueryString(baseUrl, kvps);

        using var request = new HttpRequestMessage(HttpMethod.Get, url);

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);

        var result = JsonConvert.DeserializeObject<IEnumerable<VMContractTemplate>>(json);

        return result ?? Enumerable.Empty<VMContractTemplate>();
    }

    public async Task<string> CreateAndSendContractToSign(VMAddContractForSignatureRequest vMAddContractForSignatureRequest, CancellationToken cancellationToken = default)
    {
        var apiBase = _config["PropellerApi:BaseUrl"];
        var url = $"{apiBase}/contract/send-for-signature";

        using var req = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonConvert.SerializeObject(vMAddContractForSignatureRequest), Encoding.UTF8, "application/json")
        };

        // Optional: auth headers if needed
        // req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using var resp = await _httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        var body = await resp.Content.ReadAsStringAsync(cancellationToken);

        // The endpoint returns a *string* indicating success/failure; preserve that.
        // If you want to surface HTTP errors, throw here instead of returning body on failure.
        if (!resp.IsSuccessStatusCode)
            throw new HttpRequestException($"Send contract failed ({(int)resp.StatusCode}): {body}");

        return body;
    }
}
