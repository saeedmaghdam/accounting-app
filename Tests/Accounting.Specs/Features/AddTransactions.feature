Feature: Add Transactions

A short summary of the feature

@tag1
Scenario: Adding a transaction with one debit entry and one credit entry
    Given a new transaction with date "2024-01-01", description "Investment by Owner", and the following entries
      | Type   | Account         | Amount |
      | Debit  | Cash            | 1000   |
      | Credit | Owner's Equity  | 1000   |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-01", description "Investment by Owner", and the following entries
      | Type   | Account         | Amount |
      | Debit  | Cash            | 1000   |
      | Credit | Owner's Equity  | 1000   |
    And the ledger should update the "Cash" account with a debit of 1000
    And the ledger should update the "Owner's Equity" account with a credit of 1000

Scenario: Adding a transaction with one debit entry and multiple credit entries
    Given a new transaction with date "2024-01-02", description "Sales Transaction", and the following entries
      | Type   | Account                | Amount |
      | Debit  | Accounts Receivable    | 700    |
      | Credit | Sales Revenue          | 600    |
      | Credit | Sales Tax Payable      | 100    |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-02", description "Sales Transaction", and the following entries
      | Type   | Account                | Amount |
      | Debit  | Accounts Receivable    | 700    |
      | Credit | Sales Revenue          | 600    |
      | Credit | Sales Tax Payable      | 100    |
    And the ledger should update the "Accounts Receivable" account with a debit of 700
    And the ledger should update the "Sales Revenue" account with a credit of 600
    And the ledger should update the "Sales Tax Payable" account with a credit of 100

@tag1
Scenario: Adding a compound transaction with multiple debit and credit entries
    Given a new transaction with date "2024-01-03", description "Office Supplies Purchase", and the following entries
      | Type   | Account           | Amount |
      | Debit  | Office Supplies   | 500    |
      | Credit | Cash              | 300    |
      | Credit | Accounts Payable  | 200    |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-03", description "Office Supplies Purchase", and the following entries
      | Type   | Account           | Amount |
      | Debit  | Office Supplies   | 500    |
      | Credit | Cash              | 300    |
      | Credit | Accounts Payable  | 200    |
    And the ledger should update the "Office Supplies" account with a debit of 500
    And the ledger should update the "Cash" account with a credit of 300
    And the ledger should update the "Accounts Payable" account with a credit of 200

@tag1
Scenario: Adding a transaction with multiple debit entries and one credit entry
    Given a new transaction with date "2024-01-04", description "Utility Bill Payment", and the following entries
      | Type   | Account           | Amount |
      | Debit  | Utilities Expense | 400    |
      | Debit  | Bank Fees         | 50     |
      | Credit | Cash              | 450    |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-04", description "Utility Bill Payment", and the following entries
      | Type   | Account           | Amount |
      | Debit  | Utilities Expense | 400    |
      | Debit  | Bank Fees         | 50     |
      | Credit | Cash              | 450    |
    And the ledger should update the "Utilities Expense" account with a debit of 400
    And the ledger should update the "Bank Fees" account with a debit of 50
    And the ledger should update the "Cash" account with a credit of 450


@tag1
Scenario: Adding a transaction with prepayment
    Given a new transaction with date "2024-01-05", description "Prepaid Insurance", and the following entries
      | Type   | Account             | Amount |
      | Debit  | Prepaid Insurance   | 600    |
      | Credit | Cash                | 600    |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-05", description "Prepaid Insurance", and the following entries
      | Type   | Account             | Amount |
      | Debit  | Prepaid Insurance   | 600    |
      | Credit | Cash                | 600    |
    And the ledger should update the "Prepaid Insurance" account with a debit of 600
    And the ledger should update the "Cash" account with a credit of 600

@tag1
Scenario: Adding a transaction with accrued expense
    Given a new transaction with date "2024-01-06", description "Accrued Salaries", and the following entries
      | Type   | Account             | Amount |
      | Debit  | Salaries Expense    | 1200   |
      | Credit | Salaries Payable    | 1200   |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-06", description "Accrued Salaries", and the following entries
      | Type   | Account             | Amount |
      | Debit  | Salaries Expense    | 1200   |
      | Credit | Salaries Payable    | 1200   |
    And the ledger should update the "Salaries Expense" account with a debit of 1200
    And the ledger should update the "Salaries Payable" account with a credit of 1200

@tag1
Scenario: Adding a transaction with deferred revenue
    Given a new transaction with date "2024-01-07", description "Deferred Revenue", and the following entries
      | Type   | Account             | Amount |
      | Debit  | Cash                | 1500   |
      | Credit | Deferred Revenue    | 1500   |
    When the transaction is recorded
    Then the journal should have a transaction with date "2024-01-07", description "Deferred Revenue", and the following entries
      | Type   | Account             | Amount |
      | Debit  | Cash                | 1500   |
      | Credit | Deferred Revenue    | 1500   |
    And the ledger should update the "Cash" account with a debit of 1500
    And the ledger should update the "Deferred Revenue" account with a credit of 1500