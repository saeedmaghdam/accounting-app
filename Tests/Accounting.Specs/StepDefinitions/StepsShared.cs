using Accounting.Core;

namespace Accounting.Specs.StepDefinitions
{
    [Binding]
    public partial class Steps
    {
        private Journal? _journal;
        private Ledger? _ledger;

        public Steps()
        {
            _journal = Journal.Create();
            _ledger = Ledger.Create();
        }
    }
}
