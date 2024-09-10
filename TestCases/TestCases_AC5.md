# Test Cases US#01 AC1

### Test Cases for Acceptance Criteria 5

#### Test Case #01 Update card details with Registration Data
Prerequisite 1: New user is created
Prerequisite 2: New card is verified
Step 1: Create update request data with RegistrationData <br>
Step 2: Send a PUT request with valid credentials to the /cardregistrations/{{CARD_REGISTRATION_ID}} endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the registration data and status is "VALIDATED" <br>