using LoanProject.Data.DbContect;
using LoanProject.Data.Models;
using LoanProject.Data.Models.Enums;
using LoanProject.Repository.Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoanProject.Repository.Tests
{
    [TestFixture]
    public class LoanRepositoryTests
    {
        private LoanRepository _loanRepository;
        private DatabaseContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new DatabaseContext(options);
            _loanRepository = new LoanRepository(_dbContext);

            var user = new User { Id = 1, Name = "John Doe", Email = "john@example.com" };
            var loans = new List<Loan>
            {
                new Loan { LoanId = 1, Amount = 1000, TermInMonths = 12, InterestRate = 5, LoanStatus = LoanStatus.Pending, UserId = 1, User = user },
                new Loan { LoanId = 2, Amount = 500, TermInMonths = 6, InterestRate = 3, LoanStatus = LoanStatus.Pending, UserId = 2, User = user },
                new Loan { LoanId = 3, Amount = 2000, TermInMonths = 24, InterestRate = 7, LoanStatus = LoanStatus.Approved, UserId = 3, User = user }
            };
            _dbContext.AddRange(loans);
            _dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetAll_ReturnsAllLoans()
        {
            var result = await _loanRepository.GetAll();
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task GetLoansByUserIdAsync_ReturnsLoansForGivenUserId()
        {
            int userId = 1;
            var result = await _loanRepository.GetLoansByUserIdAsync(x => x.UserId == userId);
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task UpdateLoanStatusAsync_UpdatesLoanStatusIfLoanExists()
        {
            int loanId = 1;
            var newStatus = LoanStatus.Approved;

            await _loanRepository.UpdateLoanStatusAsync(loanId, newStatus);

            var loan = _dbContext.Loans.FirstOrDefault(x => x.LoanId == loanId);
            Assert.IsNotNull(loan);
            Assert.That(loan.LoanStatus, Is.EqualTo(newStatus));
        }

        [Test]
        public async Task UpdateLoanStatusAsync_DoesNotUpdateLoanStatusIfLoanDoesNotExist()
        {
            int loanId = 100;
            var newStatus = LoanStatus.Approved;

            await _loanRepository.UpdateLoanStatusAsync(loanId, newStatus);

            var loan = _dbContext.Loans.FirstOrDefault(x => x.LoanId == loanId);
            Assert.IsNull(loan);
        }
    }
}