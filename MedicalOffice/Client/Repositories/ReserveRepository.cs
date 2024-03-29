﻿using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;

namespace MedicalOffice.Client.Repositories;

public class ReserveRepository : IReserveRepository
{
    #region Constructor
    private readonly IHttpService _http;
    private readonly string _URL = "api/Reserves";
    public ReserveRepository(IHttpService http)
    {
        _http = http;
    }
    #endregion

    #region Methods
    public async Task<ResponseData<bool>> CreateReserve(ReserveDto Reserve)
    {
        var result = await _http.PostAsync<ReserveDto, bool>($"{_URL}/createReserve", Reserve);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteReserve(ReserveDto Reserve)
    {
        var result = await _http.DeleteAsync<ReserveDto, bool>($"{_URL}/deleteReserve", Reserve);
        return result;
    }

    public async Task<ResponseData<bool>> DeleteReservesByIds(IEnumerable<long> ids)
    {
        var result = await _http.DeleteAsync<IEnumerable<long>, bool>($"{_URL}/deleteReserves", ids);
        return result;
    }

    public async Task<ResponseData<List<ReserveDto>>> GetAllReserves(int skip , int take, string search)
    {
        var result = await _http.Get<List<ReserveDto>>($"{_URL}/pages/{skip}/{take}/{search}");
        return result;
    }


    public async Task<ResponseData<IEnumerable<ReserveDto>>> GetAllReserves(string search)
    {
        var result = await _http.Get<IEnumerable<ReserveDto>>($"{_URL}/pages/{search}");
        return result;
    }


    public async Task<ResponseData<int>> GetAllReservesCount()
    {
        var result = await _http.Get<int>($"{_URL}/countReserves");
        return result;
    }

    public async Task<ResponseData<int>> GetAllReservesCount(string search)
    {
        var result = await _http.Get<int>($"{_URL}/countReserves/{search}");
        return result;
    }

    public async Task<ResponseData<ReserveDto>> GetReserveById(long Id)
    {
        var result = await _http.PostAsync<long, ReserveDto>($"{_URL}/getReserveById", Id);
        return result;
    }

    public async Task<ResponseData<bool>> UpdateReserve(ReserveDto Reserve)
    {
        var result = await _http.PutAsync<ReserveDto, bool>($"{_URL}/updateReserve", Reserve);
        return result;
    }

    public async Task<ResponseData<bool>> ChanageStatusReserveToReserved( IEnumerable<long> ids)
    {
        var result = await _http.PutAsync<IEnumerable<long>, bool>($"{_URL}/changeStatusReserveToReserved", ids);
        return result;
    }


    public async Task<ResponseData<bool>> ChanageStatusReserveToCancelled(IEnumerable<long> ids)
    {
        var result = await _http.PutAsync<IEnumerable<long>, bool>($"{_URL}/changeStatusReserveToCancelled", ids);
        return result;
    }

    #endregion
}
