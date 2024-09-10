# Acceptance Criteria

## User Story #01
As a Mangopay API client, I want to use the API to manage financial transactions
and ensure they are processed correctly and efficiently.

### Acceptance Criteria 1
**Given** a client has valid client credentials
**When** the client requests to create a new user via the API
**Then** the API should return a success status and a unique user ID

### Acceptance Criteria 2:
**Given** a client has valid client credentials and new user is created
**When** the client requests to create a new wallet for an existing user via the API
**Then** the API should return a success status and a unique wallet ID

### Acceptance Criteria 3:
**Given** a client has valid client credentials and new user is created
**When** the client requests to create a new card registration for an existing user via the API
**Then** the API should return a success status and a unique card ID
**And** the card Status should be **“CREATED”**

### Acceptance Criteria 4:
**Given** a client has valid client credentials
**When** the client requests to tokenize a newly created card via the API
**Then** the API should return a success status and a Registration Data

### Acceptance Criteria 5:
**Given** a client has valid client credentials and Registration Data Key
**When** the client requests to update a card registration with the registration data via the API
**Then** the API should return a success status and updated card data including RegistrationData and CardId
**And** the card Status should be **“VALIDATED”**

### Acceptance Criteria 6:
**Given** a client has valid client credentials and new user, wallet, validated card
**When** the client requests to create a direct card payin via the API
**Then** the API should return a success status and pay in data with id
