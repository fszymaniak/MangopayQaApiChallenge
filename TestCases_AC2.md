# Test Cases US#01 AC2

### Test Cases for Acceptance Criteria 2

#### Test Case #01 Create a new wallet for user happy path
Prerequisite: New user is created
Step 1: Create a new valid wallet using the user id <br>
Step 2: Send a request with valid credentials to the /wallets endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the wallet ID <br>

#### Test Case #02 Verify if the wallet ID is unique
Prerequisite: New user is created
Step 1: Create a new valid wallet using the user id <br>
Step 2: Send a request with valid credentials to the /wallets endpoint <br>
Step 3: Save the user ID <br>
Step 4: Send another request to the /wallets endpoint <br>
Step 5: Verify if the wallet IDs from two last requests are different <br>
 
#### Test Case #03 Cannot create a new wallet for user due to the invalid client ID
Prerequisite: New user is created
Step 1: Try to create a new wallet using the user id <br>
Step 2: Send a request with invalid client ID to the /wallets endpoint <br>
Step 3: Verify that the status code is 401 Unauthorized <br>
Step 4: Verify that the new wallet id has not been returned <br>

#### Test Case #04 Cannot create a new wallet user due to the invalid client password
Prerequisite: New user is created
Step 1: Try to create a new wallet using the user id <br>
Step 2: Send a request with invalid client password to the /wallets endpoint <br>
Step 3: Verify that the status code is 401 Unauthorized <br>
Step 4: Verify that the new wallet id has not been returned <br>

#### Test Case #05 Cannot create a new wallet for user due to not existing user
Step 1: Try to create a new wallet using the user id <br>
Step 2: Send a request with not existing user id to the /wallets endpoint <br>
Step 3: Verify that the status code is 400 Bad Request <br>
Step 4: Verify that the new wallet id has not been returned <br>
