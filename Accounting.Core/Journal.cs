
namespace Accounting.Core
{
    public class Journal
    {
        public List<Transaction> Transactions { get; } = new();

        public static Journal Create() => new();

        public void AddTransaction(Transaction transaction) => Transactions.Add(transaction);

        private string DebuggerName => $"Transactions: {Transactions.Count}, Entries: {Transactions.SelectMany(x=> x.Entries).Count()}";
    }
}
