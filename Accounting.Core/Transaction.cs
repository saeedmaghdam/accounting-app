namespace Accounting.Core
{
    public record Transaction(Guid Id, DateTimeOffset Date, string Description)
    {
        public List<Entry> Entries { get; } = new();

        public static Transaction Create(DateTimeOffset date, string description) => new(Guid.NewGuid(), date, description);

        public void AddDebitEntry(DebitEntry debitEntry) => Entries.Add(debitEntry);

        public void AddCreditEntry(CreditEntry creditEntry) => Entries.Add(creditEntry);
    }
}
