using System.Diagnostics;

namespace Accounting.Core
{
    [DebuggerDisplay("{DebuggerName}")]
    public record Transaction(Guid Id, DateTimeOffset Date, string Description)
    {
        public List<Entry> Entries { get; } = new();

        public static Transaction Create(DateTimeOffset date, string description) => new(Guid.NewGuid(), date, description);

        public void AddDebitEntry(DebitEntry debitEntry) => Entries.Add(debitEntry);

        public void AddCreditEntry(CreditEntry creditEntry) => Entries.Add(creditEntry);

        private string DebuggerName
        {
            get
            {
                var description = Description.Length > 30 ? $"{Description.Substring(0, 30)} ..." : Description;
                return $"{Date.ToString("yyyy-MM-dd HH:mm:ss"), -25}{description,-40}{Entries.OfType<CreditEntry>().Sum(x => x.Amount)}";
            }
        }
    }
}
