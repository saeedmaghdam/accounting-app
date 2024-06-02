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
    And the ledger should update the "Retained Earnings" account with a credit of 3800

@tag1
Scenario: Closing the fiscal year with net income
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Office Supplies Expense   |
      | 2024-01-03 | Rent Expense              |
      | 2024-01-04 | Service Revenue           |
      | 2024-01-05 | Utilities Expense         |
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
    And the following entries for transaction on date "2024-01-04"
      | Type   | Account         | Amount |
      | Debit  | Cash            | 3000   |
      | Credit | Service Revenue | 3000   |
    And the following entries for transaction on date "2024-01-05"
      | Type   | Account            | Amount |
      | Debit  | Utilities Expense  | 150    |
      | Credit | Accounts Payable   | 150    |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 6650
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a credit of 6650

@tag1
Scenario: Closing the fiscal year with net loss
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Office Supplies Expense   |
      | 2024-01-03 | Rent Expense              |
      | 2024-01-04 | Utilities Expense         |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Bank            | 2000   |
      | Credit | Sales Revenue   | 2000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account         | Amount |
      | Debit  | Office Supplies | 500    |
      | Credit | Accounts Payable| 500    |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 1500   |
      | Credit | Cash            | 1500   |
    And the following entries for transaction on date "2024-01-04"
      | Type   | Account            | Amount |
      | Debit  | Utilities Expense  | 300    |
      | Credit | Accounts Payable   | 300    |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Retained Earnings", and an entry with credit account "Income Summary", and amount 300
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a debit of 300

@tag1
Scenario: Closing the fiscal year with dividends declared
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Office Supplies Expense   |
      | 2024-01-03 | Rent Expense              |
      | 2024-01-04 | Dividends Declared        |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Bank            | 4000   |
      | Credit | Sales Revenue   | 4000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account         | Amount |
      | Debit  | Office Supplies | 300    |
      | Credit | Accounts Payable| 300    |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 1200   |
      | Credit | Cash            | 1200   |
    And the following entries for transaction on date "2024-01-04"
      | Type   | Account            | Amount |
      | Debit  | Retained Earnings  | 500    |
      | Credit | Dividends          | 500    |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 2500
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a credit of 2000

@tag1
Scenario: Closing the fiscal year with zero balance
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Office Supplies Expense   |
      | 2024-01-03 | Rent Expense              |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Bank            | 1000   |
      | Credit | Sales Revenue   | 1000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account         | Amount |
      | Debit  | Office Supplies | 500    |
      | Credit | Accounts Payable| 500    |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 500    |
      | Credit | Cash            | 500    |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 0
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a credit of 0

@tag1
Scenario: Closing the fiscal year with zero balance and dividends declared
	Given the following transactions
	  | Date       | Description               |
	  | 2024-01-01 | Revenue from Sales        |
	  | 2024-01-02 | Office Supplies Expense   |
	  | 2024-01-03 | Rent Expense              |
	  | 2024-01-04 | Dividends Declared        |
	And the following entries for transaction on date "2024-01-01"
	  | Type   | Account         | Amount |
	  | Debit  | Bank            | 1000   |
	  | Credit | Sales Revenue   | 1000   |
	And the following entries for transaction on date "2024-01-02"
	  | Type   | Account         | Amount |
	  | Debit  | Office Supplies | 500    |
	  | Credit | Accounts Payable| 500    |
	And the following entries for transaction on date "2024-01-03"
	  | Type   | Account         | Amount |
	  | Debit  | Rent Expense    | 500    |
	  | Credit | Cash            | 500    |
	And the following entries for transaction on date "2024-01-04"
	  | Type   | Account            | Amount |
	  | Debit  | Retained Earnings  | 500    |
	  | Credit | Dividends          | 500    |
	When the fiscal year is closed
	Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 0
	And the ledger should reset all revenue and expense accounts to zero
	And the ledger should update the "Retained Earnings" account with a credit of -500

@tag1
Scenario: Closing the fiscal year with prepaid expenses
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Prepaid Insurance         |
      | 2024-01-03 | Rent Expense              |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Cash            | 7000   |
      | Credit | Sales Revenue   | 7000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account            | Amount |
      | Debit  | Prepaid Insurance  | 500    |
      | Credit | Cash               | 500    |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 1000   |
      | Credit | Cash            | 1000   |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 6000
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a credit of 6000

@tag1
Scenario: Closing the fiscal year with accrued expenses
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Accrued Salaries          |
      | 2024-01-03 | Rent Expense              |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Cash            | 6000   |
      | Credit | Sales Revenue   | 6000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account         | Amount |
      | Debit  | Salaries Expense| 800    |
      | Credit | Salaries Payable| 800    |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 1000   |
      | Credit | Cash            | 1000   |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 4200
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a credit of 4200

@tag1
Scenario: Closing the fiscal year with unearned revenue
    Given the following transactions
      | Date       | Description               |
      | 2024-01-01 | Revenue from Sales        |
      | 2024-01-02 | Unearned Revenue          |
      | 2024-01-03 | Rent Expense              |
    And the following entries for transaction on date "2024-01-01"
      | Type   | Account         | Amount |
      | Debit  | Cash            | 4000   |
      | Credit | Sales Revenue   | 4000   |
    And the following entries for transaction on date "2024-01-02"
      | Type   | Account         | Amount |
      | Debit  | Cash            | 1000   |
      | Credit | Unearned Revenue| 1000   |
    And the following entries for transaction on date "2024-01-03"
      | Type   | Account         | Amount |
      | Debit  | Rent Expense    | 1500   |
      | Credit | Cash            | 1500   |
    When the fiscal year is closed
    Then the journal should have a transaction with date "current date", description "Close Fiscal Year", an entry with debit account "Income Summary", and an entry with credit account "Retained Earnings", and amount 2500
    And the ledger should reset all revenue and expense accounts to zero
    And the ledger should update the "Retained Earnings" account with a credit of 2500