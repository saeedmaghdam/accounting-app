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

            foreach (var account in Accounts.Where(x => x.Account.AccountType == AccountType.Revenue))
            {
                if (account.Balance == 0)
                    continue;

                var amount = Math.Abs(account.Balance);
                if (account.Balance > 0)
                {
                    AddDebitEntry(incomeSummaryLedgerAccount.Account, amount);
                    AddCreditEntry(account.Account, amount);
                }
                else
                {
                    AddCreditEntry(incomeSummaryLedgerAccount.Account, amount);
                    AddDebitEntry(account.Account, amount);
                }
            }

            foreach (var account in Accounts.Where(x => x.Account.AccountType == AccountType.Expense))
            {
                if (account.Balance == 0)
                    continue;

                var amount = Math.Abs(account.Balance);
                if (account.Balance > 0)
                {
                    AddDebitEntry(incomeSummaryLedgerAccount.Account, amount);
                    AddCreditEntry(account.Account, amount);
                }
                else
                {
                    AddCreditEntry(incomeSummaryLedgerAccount.Account, amount);
                    AddDebitEntry(account.Account, amount);
                }
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

            if (incomeSummaryLedgerAccount.Balance > 0)
            {
                closeFiscalYearTransaction.AddCreditEntry(CreditEntry.Create(incomeSummaryLedgerAccount.Account, incomeSummaryBalanceAmount));
                closeFiscalYearTransaction.AddDebitEntry(DebitEntry.Create(retainedEarningsLedgerAccount.Account, incomeSummaryBalanceAmount));

                AddDebitEntry(retainedEarningsLedgerAccount.Account, incomeSummaryBalanceAmount);
                AddCreditEntry(incomeSummaryLedgerAccount.Account, incomeSummaryBalanceAmount);
            }
            else
            {
                closeFiscalYearTransaction.AddDebitEntry(DebitEntry.Create(incomeSummaryLedgerAccount.Account, incomeSummaryBalanceAmount));
                closeFiscalYearTransaction.AddCreditEntry(CreditEntry.Create(retainedEarningsLedgerAccount.Account, incomeSummaryBalanceAmount));

                AddCreditEntry(retainedEarningsLedgerAccount.Account, incomeSummaryBalanceAmount);
                AddDebitEntry(incomeSummaryLedgerAccount.Account, incomeSummaryBalanceAmount);
            }
            journal.AddTransaction(closeFiscalYearTransaction);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Accounts);
        }
    }
}
