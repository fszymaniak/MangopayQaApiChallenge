# Test Cases US#01 AC1

### Test Cases for Acceptance Criteria 4

#### Test Case #01 Tokenize a new card (Challange Flow Card)
Prerequisite: New card is created
Step 1: Create a new valid card tokenize request data using the created card <br>
Step 2: Send a request with valid credentials to the CardRegistrationURL endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the registration data <br>