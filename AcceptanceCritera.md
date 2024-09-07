# Acceptance Criteria

## User Story #01
As a Mangopay API client, I want to use the API to manage financial transactions
and ensure they are processed correctly and efficiently.

### Acceptance Criteria 1
**Given** a client has valid client credentials <br>
**When** the client requests to create a new user via the API <br>
**Then** the API should return a success status and a unique user ID <br>

### Acceptance Criteria 2:
**Given** a client has valid client credentials and the new user is created <br>
**When** the client requests to create a new wallet for an existing user via the API <br>
**Then** the API should return a success status and a unique wallet ID <br>

### Acceptance Criteria 3:
**Given** a client has valid client credentials and the new user is created <br>
**When** the client requests to create a new card registration for an existing user via the API <br>
**Then** the API should return a success status and a unique card ID <br>
**And** the card Status should be **“CREATED”** <br>

### Acceptance Criteria 4:
**Given** a client has valid client credentials <br>
**When** the client requests to tokenize a newly created card via the API <br>
**Then** the API should return a success status and a Registration Data <br>

### Acceptance Criteria 5:
**Given** a client has valid client credentials and Registration Data Key <br>
**When** the client requests to update a card registration with the registration data via the API <br>
**Then** the API should return a success status and updated card data including RegistrationData and CardId <br>
**And** the card Status should be **“VALIDATED”** <br>

### Acceptance Criteria 6:
**Given** a client has valid client credentials and a new user, wallet, validated card <br>
**When** the client requests to create a direct card pay-in via the API <br>
**Then** the API should return a success status and pay in data with id <be>
