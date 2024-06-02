Feature: View Journal Entries

A short summary of the feature

@tag1
Scenario: Viewing all journal entries
    Given a new transaction with date "2024-01-01", description "Investment by Owner", and the following entries
      | Type   | Account         | Amount |
      | Debit  | Cash            | 1000   |
      | Credit | Owner's Equity  | 1000   |
    And a new transaction with date "2024-01-02", description "Purchase office supplies", and the following entries
      | Type   | Account         | Amount |
      | Debit  | Office Supplies | 200    |
      | Credit | Accounts Payable| 200    |
    When the transaction is recorded
    And I view the journal
    Then the journal should have a transaction with date "2024-01-01", description "Investment by Owner", and the following entries
      | Type   | Account         | Amount |
      | Debit  | Cash            | 1000   |
      | Credit | Owner's Equity  | 1000   |
    And the journal should have a transaction with date "2024-01-02", description "Purchase office supplies", and the following entries
      | Type   | Account         | Amount |
      | Debit  | Office Supplies | 200    |
      | Credit | Accounts Payable| 200    |
    And the ledger should update the "Cash" account with a debit of 1000
    And the ledger should update the "Owner's Equity" account with a credit of 1000
    And the ledger should update the "Office Supplies" account with a debit of 200
    And the ledger should update the "Accounts Payable" account with a credit of 200