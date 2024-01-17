﻿using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Enum;
using MedicalOffice.Shared.Helper.Mapper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services.Helper;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Ui.Pages.Components
{
    public class ReservationComponentBase : ComponentBase
    {

        #region Inject
        [Inject]
        public IDaysReserveRepository daysReserveRepository { get; set; }

        [Inject]
        public ITimesRepository timesRepository { get; set; }

        [Inject]
        public IUserRepository userRepository { get; set; }

        #endregion

        #region Constructor
        public ReservationComponentBase()
        {
            FinishInfo = d_none;
            ButtonContinueReserve = d_none;
        }
        #endregion

        #region Fields
        public string d_block { get; set; } = "d-block";
        public string d_none { get; set; } = "d-none";

        public string? CurrentNameDay { get; set; }
        public string? CurrentDateDay { get; set; }
        public List<DaysReserve> DaysReserves { get; set; } = new();
        public List<IGrouping<long, TimesReserve>> TimesReserves { get; set; } = new();
        public string Selected { get; set; } = string.Empty;

        private TimesReserve SelectedTime { get; set; } = new();
        public string FinishInfo { get; set; }
        public string ButtonContinueReserve { get; set; }

        public UserRegisterReserveDto UserRegisterReserveDto { get; set; } = new();

        [Parameter]
        public EventCallback<bool> IsComponentLoading { get; set; }
        #endregion

        #region Methods
        protected override async Task OnInitializedAsync()
        {
            var currentDate = DateTime.Now;
            CurrentDateDay = currentDate.ToShamsi();
            CurrentNameDay = currentDate.ToDayShamsi();
            await ShowDays();
            await IsComponentLoading.InvokeAsync(false);
        }

        /// <summary>
        /// Show Dates With Times
        /// </summary>
        /// <returns></returns>
        public async Task ShowDays()
        {
            var dates = await daysReserveRepository.GetTimesDayReserve();
            if (dates.Success)
            {
                var res = dates.Response.ToList();
                DaysReserves = res;

                var times = res.SelectMany(x => x.TimesReserves);

                if (times.Any())
                {
                    TimesReserves = times.GroupBy(x => x.DaysReserveId).ToList();
                }


            }
        }

        public void SetSelectedTime(TimesReserve selectTime)
        {
            SelectedTime = selectTime;
            ButtonContinueReserve = d_block;
        }

        public string SelectedClass(TimesReserve time) => time == SelectedTime ? "selected" : string.Empty;

        public void ContinuePurchaseProcess()
        {
            if (SelectedTime.Id == 0)
            {
                return;
            }
            FinishInfo = d_block;
            ButtonContinueReserve = d_none;
        }

        public void CancellReserve()
        {          
            FinishInfo = d_none;
            ButtonContinueReserve = d_block;
        }

        public async Task SaveInfoAndConnectToPay()
        {
            var user= UserRegisterReserveDto.Mapper();
            user.Password = UserRegisterReserveDto.NationalCode;
            user.RoleId = (long)RoleEnum.user;
            await userRepository.CreateUser(user);
        }

        #endregion
    }


}
