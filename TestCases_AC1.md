# Test Cases US#01 AC1

### Test Cases for Acceptance Criteria 1

#### Test Case #01 Create a natural user happy path
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with valid credentials to the /users/natural endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the user ID <br>

#### Test Case #02 Verify if the user ID is unique
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with valid credentials to the /users/natural endpoint <br>
Step 3: Save the user ID <br>
Step 4: Send another request to the /users/natural endpoint <br>
Step 5: Verify if the wallet IDs from two last requests are different <br>
 
#### Test Case #03 Cannot create a new natural user due to the invalid client ID
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with an invalid client ID to the /users/natural endpoint <br>
Step 3: Verify that the status code is 401 Unauthorized <br>
Step 4: Verify that the new user id has not been returned <br>

#### Test Case #04 Cannot create a new natural user due to the invalid client password
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with an invalid API key to the /users/natural endpoint <br>
Step 3: Verify that the status code is 401 Unauthorized <br>
Step 4: Verify that the new user id has not been returned <br>

#### Test Case #05 Cannot create a new natural user due to an invalid email
Step 1: Create a new natural user data with an invalid email <br>
Step 2: Send a request with valid credentials to the /users/natural endpoint <br>
Step 3: Verify that the status code is 400 BadRequest <br>
Step 4: Verify that the new user id has not been returned <br>
