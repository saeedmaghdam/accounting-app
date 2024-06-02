using Accounting.Core;

namespace Accounting.Specs.StepDefinitions
{
    public partial class Steps
    {
        private Transaction? _transaction;

        [Given("a new transaction with date {string}, description {string}, and the following entries")]
        public void GivenANewTransactionWithDateDescriptionAndTheFollowingEntries(DateTimeOffset transactionDate, string description, DataTable entries)
        {
            _transaction = Transaction.Create(transactionDate, description);

            foreach (var row in entries.Rows)
            {
                var account = Account.ToAccount(row["Account"]);
                var amount = double.Parse(row["Amount"]);

                if (row["Type"] == "Debit")
                    _transaction.AddDebitEntry(DebitEntry.Create(account, amount));
                else
                    _transaction.AddCreditEntry(CreditEntry.Create(account, amount));
            }
        }


        [When("the transaction is recorded")]
        public void WhenTheTransactionIsRecorded()
        {
            _journal!.AddTransaction(_transaction!);
            _ledger!.Initialize(_journal!);
        }

        [Then("the journal should have a transaction with date {string}, description {string}, and the following entries")]
        public void ThenTheJournalShouldHaveATransactionWithDateDescriptionAndTheFollowingEntries(DateTimeOffset transactionDate, string description, DataTable entries)
        {
            var transaction = _journal!.Transactions[0];

            transaction.Date.Should().Be(transactionDate);
            transaction.Description.Should().Be(description);

            foreach (var row in entries.Rows)
            {
                var account = Account.ToAccount(row["Account"]);
                var amount = double.Parse(row["Amount"]);

                if (row["Type"] == "Debit")
                    transaction.Entries.OfType<DebitEntry>().Should().Contain(DebitEntry.Create(account, amount));
                else
                    transaction.Entries.OfType<CreditEntry>().Should().Contain(CreditEntry.Create(account, amount));
            }
        }

        [Then("the ledger should update the {string} account with a debit of {double}")]
        public void ThenTheLedgerShouldUpdateTheAccountWithADebitOf(string debitAccount, double amount)
        {
            var account = Account.ToAccount(debitAccount);

            var ledgerAccount = _ledger!.Accounts.Single(a => a.Account == account);
            ledgerAccount.Balance.Should().Be(amount);
            _ledger!.Accounts.Should().Contain(ledgerAccount);
        }

        [Then("the ledger should update the {string} account with a credit of {double}")]
        public void ThenTheLedgerShouldUpdateTheAccountWithACreditOf(string creditAccount, double amount)
        {
            var account = Account.ToAccount(creditAccount);

            var ledgerAccount = _ledger!.Accounts.Single(a => a.Account == account);
            ledgerAccount.Balance.Should().Be(-amount);
            _ledger!.Accounts.Should().Contain(ledgerAccount);
        }
    }
}
