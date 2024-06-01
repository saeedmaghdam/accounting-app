Feature: View Ledger Accounts

A short summary of the feature

@tag1
Scenario: Viewing all ledger accounts
    Given multiple journal entries recorded
      | date       | description               | debit account   | credit account     | amount |
      | 2024-01-01 | Investment by Owner       | Cash            | Owner's Equity     | 1000   |
      | 2024-01-02 | Purchase office supplies  | Office Supplies | Accounts Payable   | 200    |
    When I view the ledger
    Then I should see the updated balances of the ledger accounts
      | account         | debit  | credit | balance |
      | Cash            | 1000   | 0      | 1000    |
      | Owner's Equity  | 0      | 1000   | 1000    |
      | Office Supplies | 200    | 0      | 200     |
      | Accounts Payable| 0      | 200    | 200     |
