# Test Cases US#01 AC1

### Test Cases for Acceptance Criteria 3

#### Test Case #01 Register a new card
Prerequisite: New user is created
Step 1: Register a new valid card using the created user <br>
Step 2: Send a request with valid credentials to the /cardregistrations endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the user ID and status is "CREATED" <br>