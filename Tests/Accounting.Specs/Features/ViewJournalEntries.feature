Feature: View Journal Entries

A short summary of the feature

@tag1
Scenario: Viewing all journal entries
    Given multiple journal entries recorded
      | date       | description               | debit account   | credit account     | amount |
      | 2024-01-01 | Investment by Owner       | Cash            | Owner's Equity     | 1000   |
      | 2024-01-02 | Purchase office supplies  | Office Supplies | Accounts Payable   | 200    |
    When I view the journal
    Then I should see all the journal entries
      | date       | description               | debit account   | credit account     | amount |
      | 2024-01-01 | Investment by Owner       | Cash            | Owner's Equity     | 1000   |
      | 2024-01-02 | Purchase office supplies  | Office Supplies | Accounts Payable   | 200    |