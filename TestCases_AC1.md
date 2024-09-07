# Test Cases

### Test Cases for Acceptance Criteria 1

#### Test Case #01 Create natural user happy path
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with valid credentials to the /users/natural endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the user id <br>

#### Test Case #02 Verify if the user id is uniqe
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with valid credentials to the /users/natural endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Save the user id <br>
Step 5: Send another request to the /users/natural endpoint <br>
Step 6: Verify if the user ids from the first and the second call are different <br>
 
#### Test Case #03 Cannot create a new natural user due to the invalid client id
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with invalid client id to the /users/natural endpoint <br>
Step 3: Verify that the status code is 401 Unathorized <br>
Step 4: Verify that the new user id has not been returned <br>

#### Test Case #04 Cannot create a new natural user due to the invalid api key
Step 1: Create a new valid natural user data <br>
Step 2: Send a request with invalid api ket to the /users/natural endpoint <br>
Step 3: Verify that the status code is 401 Unathorized <br>
Step 4: Verify that the new user id has not been returned <br>

#### Test Case #04 Cannot create a new natural user due to missing first name
Step 1: Create a new natural user data with missing first name <br>
Step 2: Send a request with valid credentials to the /users/natural endpoint <br>
Step 3: Verify that the status code is 400 BadRequest <br>
Step 4: Verify that the new user id has not been returned <br>
