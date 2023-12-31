﻿using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class ProvidingServiceRepository: IProvidingServiceRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/ProvidingService";
    private readonly HttpClient _httpClient;
    public ProvidingServiceRepository(HttpClient httpClient, IHttpService http)
    {
        _httpClient = httpClient;
        _http = http;

    }

    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateProvidingService(MultipartFormDataContent model)
    {
        var result = await _http.PostAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateProvidingService(MultipartFormDataContent model)
    {
        var result = await _http.PutAsync<bool>($"{_URL}", model);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteProvidingServiceByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteProvidingService", ids);
        return result;
    }
    public async Task<ResponseData<IEnumerable<ProvidingService>>> GetProvidingService()
    {
        var result = await _http.Get<IEnumerable<ProvidingService>>($"{_URL}");
        return result;
    }

    public async Task<ResponseData<ProvidingServiceDto>> GetProvidingServiceById(long Id)
    {
        var result = await _http.PostAsync<long, ProvidingServiceDto>($"{_URL}", Id);
        return result;
    }


    #endregion
}
