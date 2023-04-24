using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanProject.Services.Infrastructure.WorkerServices
{
    public class LoanStatusService : BackgroundWorker
    {
        private readonly ILoanRepository _loanRepository;
        private readonly Random _random;

        public LoanStatusService(ILoanRepository loanRepository, Random random)
        {
            _loanRepository = loanRepository;
            _random = random;
            Interval = TimeSpan.FromMinutes(2).TotalMilliseconds;
        }

        protected async override void OnDoWork(DoWorkEventArgs e)
        {
            while (!CancellationPending)
            {
                var loans = await _loanRepository.GetExistingLoansAsync();
                foreach (var loan in loans)
                {
                    if (loan.LoanStatus == LoanStatus.Approved || loan.LoanStatus == LoanStatus.Declined)
                    {
                        continue;
                    }

                    var status = _random.Next(2) == 0 ? LoanStatus.Approved : LoanStatus.Declined;
                    _loanRepository.UpdateLoanStatusAsync(loan.LoanId, status).Wait();
                }

                Thread.Sleep((int)Interval);
            }
        }
    }
}
