using Accounting.Core;

namespace Accounting.Specs.StepDefinitions
{
    [Binding]
    public class AddTransactionsStepDefinitions
    {
        private Journal? _journal;
        private Transaction? _transaction;
        private Ledger? _ledger;

        [Given("a new transaction with date {string}, description {string}, debit account {string}, credit account {string}, and amount {double}")]
        public void GivenANewJournalEntryWithDateDescriptionDebitAccountCreditAccountAndAmount(DateTimeOffset transactionDate, string description, string debitAccount, string creditAccount, double amount)
        {
            var account = Account.ToAccount(debitAccount);
            _transaction = Transaction.Create(transactionDate, description);
            _transaction.AddDebitEntry(DebitEntry.Create(account, amount));
            _transaction.AddCreditEntry(CreditEntry.Create(account, amount));
        }

        [Given("a new transaction with date {string}, description {string}, and the following entries")]
        public void GivenANewTransactionWithDateDescriptionAndTheFollowingEntries(DateTimeOffset transactionDate, string description, DataTable entries)
        {
            _transaction = Transaction.Create(transactionDate, description);

            foreach (var row in entries.Rows)
            {
                var account =Account.ToAccount(row["Account"]);
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
            _journal = Journal.Create();
            _journal.AddTransaction(_transaction!);
        }

        [Then("the journal should have a transaction with date {string}, description {string}, and the following entries")]
        public void ThenTheJournalShouldHaveATransactionWithDateDescriptionAndTheFollowingEntries(DateTimeOffset transactionDate, string description, DataTable entries)
        {
            var transaction = _journal!.Transactions[0];

            transaction.Date.Should().Be(transactionDate);
            transaction.Description.Should().Be(description);

            foreach (var row in entries.Rows)
            {
                var account =Account.ToAccount(row["Account"]);
                var amount = double.Parse(row["Amount"]);

                if (row["Type"] == "Debit")
                    transaction.Entries.OfType<DebitEntry>().Should().Contain(DebitEntry.Create(account, amount));
                else
                    transaction.Entries.OfType<CreditEntry>().Should().Contain(CreditEntry.Create(account, amount));
            }
        }


        [Then("the journal should have a transaction with date {string}, description {string}, and two entries, debit account {string} with amount {double}, credit account {string} with amount {double}")]
        public void ThenTheJournalShouldHaveAnEntryWithDateDescriptionDebitAccountCreditAccountAndAmount(DateTimeOffset transactionDate, string description, string debitAccount, double debitAmount, string creditAccount, double creditAmount)
        {
            var account =Account.ToAccount(debitAccount);
            var transaction = _journal!.Transactions[0];

            transaction.Date.Should().Be(transactionDate);
            transaction.Description.Should().Be(description);

            transaction.Entries.OfType<DebitEntry>().Should().Contain(DebitEntry.Create(account, debitAmount));
            transaction.Entries.OfType<CreditEntry>().Should().Contain(CreditEntry.Create(account, creditAmount));
        }

        [Then("the ledger should update the {string} account with a debit of {double}")]
        public void ThenTheLedgerShouldUpdateTheAccountWithADebitOf(string debitAccount, double amount)
        {
            if (_ledger == null)
                _ledger = Ledger.Create();

            var account =Account.ToAccount(debitAccount);

            _ledger.AddDebitEntry(account, amount);

            var ledgerAccount = _ledger.Accounts.Single(a => a.Account == account);
            ledgerAccount.Balance.Should().Be(amount);
            _ledger.Accounts.Should().Contain(ledgerAccount);
        }

        [Then("the ledger should update the {string} account with a credit of {double}")]
        public void ThenTheLedgerShouldUpdateTheAccountWithACreditOf(string creditAccount, double amount)
        {
            if (_ledger == null)
                _ledger = Ledger.Create();

            var account =Account.ToAccount(creditAccount);

            _ledger.AddCreditEntry(account, amount);

            var ledgerAccount = _ledger.Accounts.Single(a => a.Account == account);
            ledgerAccount.Balance.Should().Be(-amount);
            _ledger.Accounts.Should().Contain(ledgerAccount);
        }
    }
}
