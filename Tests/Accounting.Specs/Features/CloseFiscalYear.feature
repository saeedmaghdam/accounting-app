Feature: Close Fiscal Year

A short summary of the feature

Feature: Close Fiscal Year

@tag1
Scenario: Closing the fiscal year
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Office Supplies Expense   |
      | 2024-01-03 | Rent Expense              |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Bank            | 5000   |
      | Credit | Sales Revenue   | 5000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account         | Amount |
      | Debit  | Office Supplies | 200    |
      | Credit | Accounts Payable| 200    |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 1000   |
      | Credit | Cash            | 1000   |
    When the fiscal year is closed
    Then the journal should have an transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 3800
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Owner's Equity" account with a credit of 3800