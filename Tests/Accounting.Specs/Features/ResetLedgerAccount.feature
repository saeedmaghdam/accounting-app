Feature: Reset Ledger Account

A short summary of the feature

@tag1
Scenario: Resetting a ledger account at year-end
    Given a ledger account with a balance
      | Account         | Debit  | Credit | Balance |
      | Sales Revenue   | 5000   | 0      | 5000    |
    When the fiscal year is closed
    Then the ledger account "Sales Revenue" should be reset to zero