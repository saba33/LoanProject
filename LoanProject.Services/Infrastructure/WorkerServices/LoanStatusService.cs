using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Infrastructure.WorkerServices
{
    public class LoanStatusService : IHostedService, IDisposable
    {
        private readonly ILoanRepository _loanRepository;
        private readonly Random _random;
        private Timer _timer;
        public LoanStatusService(ILoanRepository loanRepository, Random random)
        {
            _loanRepository = loanRepository;
            _random = random;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateLoanStatusAsync, null, TimeSpan.Zero, TimeSpan.FromMinutes(3));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void UpdateLoanStatusAsync(object state)
        {
            var loans = await _loanRepository.GetAll();
            foreach (var loan in loans)
            {
                if (loan.LoanStatus == LoanStatus.Approved || loan.LoanStatus == LoanStatus.Declined)
                {
                    continue;
                }

                var status = _random.Next(2) == 0 ? LoanStatus.Approved : LoanStatus.Declined;
                _loanRepository.UpdateLoanStatusAsync(loan.LoanId, status).Wait();
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
