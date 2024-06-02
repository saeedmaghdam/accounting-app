using Accounting.Core;

namespace Accounting.Specs.StepDefinitions
{
    public partial class Steps
    {
        [When("I view the ledger")]
        public void WhenIViewTheLedger()
        {
            // Method intentionally left empty.
        }

        [Then("I should see the updated balances of the ledger accounts")]
        public void ThenIShouldSeeTheUpdatedBalancesOfTheLedgerAccounts(DataTable ledgerAccounts)
        {
            foreach (var row in ledgerAccounts.Rows)
            {
                var account = Account.ToAccount(row["Account"]);
                var balance = double.Parse(row["Balance"]);

                var ledgerAccount = _ledger!.Accounts.Find(x => x.Account == account);
                ledgerAccount!.Balance.Should().Be(balance);
            }
        }
    }
}
