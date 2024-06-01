namespace Accounting.Core
{
    public class Ledger
    {
        public List<LedgerAccount> Accounts { get; } = new();

        public static Ledger Create() => new();

        public void AddAccount(LedgerAccount account) => Accounts.Add(account);

        public void AddDebitEntry(Account account, double amount)
        {
            var ledgerAccount = Accounts.FirstOrDefault(a => a.Account == account);
            if (ledgerAccount == null)
            {
                ledgerAccount = LedgerAccount.Create(account);
                Accounts.Add(ledgerAccount);
            }

            ledgerAccount.AddDebitEntry(DebitEntry.Create(account, amount));
        }

        public void AddCreditEntry(Account account, double amount)
        {
            var ledgerAccount = Accounts.FirstOrDefault(a => a.Account == account);
            if (ledgerAccount == null)
            {
                ledgerAccount = LedgerAccount.Create(account);
                Accounts.Add(ledgerAccount);
            }

            ledgerAccount.AddCreditEntry(CreditEntry.Create(account, amount));
        }

        public void Initialize(Journal journal)
        {
            foreach (var entry in journal.Transactions.SelectMany(x => x.Entries))
            {
                switch (entry)
                {
                    case DebitEntry debitEntry:
                        AddDebitEntry(debitEntry.Account, debitEntry.Amount);
                        break;
                    case CreditEntry creditEntry:
                        AddCreditEntry(creditEntry.Account, creditEntry.Amount);
                        break;
                }
            }
        }

        public void CloseFiscalYear(Journal journal)
        {
            var incomeSummaryAccountName = "00001:Income Summary";
            var incomeSummaryLedgerAccount = Accounts.FirstOrDefault(x => x.Account.Name == incomeSummaryAccountName);
            if (incomeSummaryLedgerAccount == null)
            {
                incomeSummaryLedgerAccount = LedgerAccount.Create(Account.Create(incomeSummaryAccountName));
                Accounts.Add(incomeSummaryLedgerAccount);
            }

            foreach (var account in Accounts.Where(x=> x.Account.AccountType == AccountType.Revenue))
            {
                AddDebitEntry(incomeSummaryLedgerAccount.Account, account.Balance);
                AddCreditEntry(account.Account, account.Balance);
            }

            foreach (var account in Accounts.Where(x => x.Account.AccountType == AccountType.Expense))
            {
                AddDebitEntry(incomeSummaryLedgerAccount.Account, account.Balance);
                AddCreditEntry(account.Account, account.Balance);
            }

            var incomeSummaryBalanceAmount = Math.Abs(incomeSummaryLedgerAccount.Balance);

            var retainedEarningsAccount = Account.ToAccount("Retained Earnings");
            var retainedEarningsLedgerAccount = Accounts.FirstOrDefault(x => x.Account == retainedEarningsAccount);
            if (retainedEarningsLedgerAccount == null)
            {
                retainedEarningsLedgerAccount = LedgerAccount.Create(retainedEarningsAccount);
                Accounts.Add(retainedEarningsLedgerAccount);
            }

            var closeFiscalYearTransaction = Transaction.Create(DateTimeOffset.UtcNow, "Close Fiscal Year");
            closeFiscalYearTransaction.AddDebitEntry(DebitEntry.Create(incomeSummaryLedgerAccount.Account, incomeSummaryBalanceAmount));
            closeFiscalYearTransaction.AddCreditEntry(CreditEntry.Create(retainedEarningsLedgerAccount.Account, incomeSummaryBalanceAmount));
            journal.AddTransaction(closeFiscalYearTransaction);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Accounts);
        }
    }
}
