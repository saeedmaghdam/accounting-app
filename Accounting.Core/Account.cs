using System.ComponentModel;
using System.Diagnostics;

namespace Accounting.Core
{
    [DebuggerDisplay("{DebuggerName}")]
    public record Account(string Name)
    {
        public bool IsTemp { get => AccountType == AccountType.Expense || AccountType == AccountType.Revenue; }
        public AccountType AccountType { get => Name[0] switch
                {
                    '1' => AccountType.Asset,
                    '2' => AccountType.Liability,
                    '3' => AccountType.Equity,
                    '4' => AccountType.Revenue,
                    '5' => AccountType.Expense,
                    '0' => AccountType.Misc,
                    _ => throw new InvalidEnumArgumentException("Invalid account type")
                };
        }

        public static Account Create(string name) => new(name);

        public static Account ToAccount(string name)
        {
            return name switch
            {
                "Income Summary" => Account.Create($"00001:{name}"),
                "Cash" => Account.Create($"10001:{name}"),
                "Accounts Receivable" => Account.Create($"10002:{name}"),
                "Supplies" => Account.Create($"10003:{name}"),
                "Prepaid Insurance" => Account.Create($"10004:{name}"),
                "Equipment" => Account.Create($"10005:{name}"),
                "Bank" => Account.Create($"10006:{name}"),
                "Land" => Account.Create($"10007:{name}"),
                "Building" => Account.Create($"10008:{name}"),
                "Accumulated Depreciation" => Account.Create($"20001:{name}"),
                "Accounts Payable" => Account.Create($"20002:{name}"),
                "Salaries Payable" => Account.Create($"20003:{name}"),
                "Unearned Revenue" => Account.Create($"20004:{name}"),
                "Notes Payable" => Account.Create($"20005:{name}"),
                "Interest Payable" => Account.Create($"20006:{name}"),
                "Sales Tax Payable" => Account.Create($"20007:{name}"),
                "Owner's Equity" => Account.Create($"30001:{name}"),
                "Common Stock" => Account.Create($"30002:{name}"),
                "Retained Earnings" => Account.Create($"30003:{name}"),
                "Dividends" => Account.Create($"30004:{name}"),
                "Service Revenue" => Account.Create($"40001:{name}"),
                "Rent Revenue" => Account.Create($"40002:{name}"),
                "Sales Revenue" => Account.Create($"40003:{name}"),
                "Deferred Revenue" => Account.Create($"40004:{name}"),
                "Supplies Expense" => Account.Create($"50001:{name}"),
                "Depreciation Expense" => Account.Create($"50002:{name}"),
                "Insurance Expense" => Account.Create($"50003:{name}"),
                "Salaries Expense" => Account.Create($"50004:{name}"),
                "Interest Expense" => Account.Create($"50005:{name}"),
                "Rent Expense" => Account.Create($"50006:{name}"),
                "Utilities Expense" => Account.Create($"50007:{name}"),
                "Miscellaneous Expense" => Account.Create($"50008:{name}"),
                "Office Supplies" => Account.Create($"50009:{name}"),
                "Telephone Expense" => Account.Create($"50010:{name}"),
                "Advertising Expense" => Account.Create($"50011:{name}"),
                "Bank Fees" => Account.Create($"50012:{name}"),
                "Delivery Expense" => Account.Create($"50013:{name}"),
                "Gas and Fuel" => Account.Create($"50014:{name}"),
                "Maintenance Expense" => Account.Create($"50015:{name}"),
                "Travel Expense" => Account.Create($"50016:{name}"),
                "Meals and Entertainment" => Account.Create($"50017:{name}"),
                "Postage Expense" => Account.Create($"50018:{name}"),
                "Training Expense" => Account.Create($"50019:{name}"),
                _ => throw new ArgumentException($"Invalid account name {name}")
            };
        }

        public override string ToString() => Name;

        private string DebuggerName => $"{AccountType, -12}{Name, 10}";
    }
}
