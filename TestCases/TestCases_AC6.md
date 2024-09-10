# Test Cases US#01 AC1

### Test Cases for Acceptance Criteria 6

#### Test Case #01 Make a Direct Pay In 
Prerequisite 1: New card is created and tokenized
Prerequisite 2: New wallet is created
Step 1: Create a payin request data using the user, verified card and created wallet <br>
Step 2: Send a POST request with valid credentials to the /payins/card/direct endpoint <br>
Step 3: Verify that the status code is 200 OK <br>
Step 4: Verify if the API returns the registrtion data and status is "VERIFIED" <br>