using Accounting.Core;

namespace Accounting.Specs.StepDefinitions
{
    public partial class Steps
    {
        [Given("the following transactions")]
        public void GivenTheFollowingTransactions(DataTable transactions)
        {
            foreach (var row in transactions.Rows)
            {
                var transactionDate = DateTimeOffset.Parse(row["Date"]);
                var description = row["Description"];

                var transaction = Transaction.Create(transactionDate, description);

                _journal!.AddTransaction(transaction);
            }
        }

        [Given("the following entries for transaction on date {string}")]
        public void GivenTheFollowingEntriesForTransactionOnDate(DateTimeOffset transactionDate, DataTable entries)
        {
            var transaction = _journal!.Transactions.Single(t => t.Date == transactionDate);

            foreach (var row in entries.Rows)
            {
                var account = Account.ToAccount(row["Account"]);
                var amount = double.Parse(row["Amount"]);

                if (row["Type"] == "Debit")
                    transaction.AddDebitEntry(DebitEntry.Create(account, amount));
                else
                    transaction.AddCreditEntry(CreditEntry.Create(account, amount));
            }
        }

        [When("the fiscal year is closed")]
        public void WhenTheFiscalYearIsClosed()
        {
            _ledger!.Initialize(_journal!);
            _ledger!.CloseFiscalYear(_journal!);
        }

        [Then("the journal should have an transaction with date {string}, description {string}, an entry with debit account {string}, and an entry with credit account {string}, and amount {double}")]
        public void ThenTheJournalShouldHaveAnEntryWithDateDescriptionDebitAccountCreditAccountAndAmount(string transactionDate, string description, string debitAccount, string creditAccount, double amount)
        {
            var transaction = default(Transaction);
            DateTimeOffset.TryParse(transactionDate, out var date);
            if (transactionDate == "current date")
                transaction = _journal!.Transactions.Single(t => t.Date.Date == DateTimeOffset.UtcNow.Date);
            else
                transaction = _journal!.Transactions.Single(t => t.Date == date);

            var transactionDebitEntry = transaction.Entries.OfType<DebitEntry>().Single();
            var transactionCreditEntry = transaction.Entries.OfType<CreditEntry>().Single();

            if (transactionDate == "current date")
                transaction.Date.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromDays(1));
            else
                transaction.Date.Should().Be(date);
            transaction.Description.Should().Be(description);

            transactionDebitEntry.Account.Should().Be(Account.ToAccount(debitAccount));
            transactionDebitEntry.Amount.Should().Be(amount);

            transactionCreditEntry.Account.Should().Be(Account.ToAccount(creditAccount));
            transactionCreditEntry.Amount.Should().Be(amount);
        }

        [Then("the journal should have a transaction with date {string}, description {string}, an entry with debit account {string}, and an entry with credit account {string}, and amount {int}")]
        public void ThenTheJournalShouldHaveATransactionWithDateDescriptionAnEntryWithDebitAccountAndAnEntryWithCreditAccountAndAmount(string transactionDate, string description, string debitAccount, string creditAccount, double amount)
        {
            var transaction = default(Transaction);
            DateTimeOffset.TryParse(transactionDate, out var date);
            if (transactionDate == "current date")
                transaction = _journal!.Transactions.Single(t => t.Date.Date == DateTimeOffset.UtcNow.Date);
            else
                transaction = _journal!.Transactions.Single(t => t.Date == date);

            var transactionDebitEntry = transaction.Entries.OfType<DebitEntry>().Single();
            var transactionCreditEntry = transaction.Entries.OfType<CreditEntry>().Single();

            if (transactionDate == "current date")
                transaction.Date.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromDays(1));
            else
                transaction.Date.Should().Be(date);
            transaction.Description.Should().Be(description);

            transactionDebitEntry.Account.Should().Be(Account.ToAccount(debitAccount));
            transactionDebitEntry.Amount.Should().Be(amount);

            transactionCreditEntry.Account.Should().Be(Account.ToAccount(creditAccount));
            transactionCreditEntry.Amount.Should().Be(amount);
        }

        [Then("the ledger should reset all revenue and expense accounts to zero")]
        public void ThenTheLedgerShouldResetAllRevenueAndExpenseAccountsToZero()
        {
            var revenueLedgerAccounts = _ledger!.Accounts.Where(x => x.Account.AccountType == AccountType.Revenue);
            foreach (var account in revenueLedgerAccounts)
                account.Balance.Should().Be(0);

            var expenseLedgerAccounts = _ledger!.Accounts.Where(x => x.Account.AccountType == AccountType.Expense);
            foreach (var account in expenseLedgerAccounts)
                account.Balance.Should().Be(0);
        }
    }
}
