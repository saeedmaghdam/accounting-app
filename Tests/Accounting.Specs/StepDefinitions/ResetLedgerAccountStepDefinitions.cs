using Accounting.Core;

namespace Accounting.Specs.StepDefinitions
{
    public partial class Steps
    {
        [Given("a ledger account with a balance")]
        public void GivenALedgerAccountWithABalance(DataTable ledgerAccounts)
        {
            var transaction = Transaction.Create(DateTimeOffset.UtcNow, "Initial balance");
            _journal!.AddTransaction(transaction);

            foreach (var row in ledgerAccounts.Rows)
            {
                var account = Account.ToAccount(row["Account"]);
                var debitAmount = double.Parse(row["Debit"]);
                var creditAmount = double.Parse(row["Credit"]);

                transaction.AddDebitEntry(DebitEntry.Create(account, debitAmount));
                transaction.AddCreditEntry(CreditEntry.Create(account, creditAmount));
            }
        }

        [Then("the ledger account {string} should be reset to zero")]
        public void ThenTheLedgerAccountShouldBeResetToZero(string account)
        {
            var ledgerAccount = _ledger!.Accounts.Find(x => x.Account == Account.ToAccount(account));
            ledgerAccount!.Balance.Should().Be(0);
        }
    }
}
