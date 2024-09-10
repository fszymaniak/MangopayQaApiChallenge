### How I would test NFRs
NFR stands for Non-Functional Requirements. <br>
I am going to present some examples how I would take care of the performance, security and usability testing.

#### Performance testing
Find the equivalent number of people who will be working with the API then we should run tests with the increased scope.
- Load testing with the maximum capacity of the API
- Stress testing - how the API is behaving after much loads with its normal capacity
- Volume testing - verifying how API is handling large amount of data
- Spike testing - how the API is behaving when there will be more transactions e.g. Black Friday, Christmas or great discount sales

#### Security API testing
- OWASP top 10 for API testing e.g.
	- Unauthorized Access / Broken Access: 
		- try to access wallet for other users using my user credentials
		- try to make a payin for another users
	- Validate how the rate limiting looks in the API, if single user is able to run as many request as he want
- trying to make some requests with JavaScript injections

#### Usability API testing
- verifying if the API is complex (should not be)
- naming and endpoints should easy to understand and self-documenting themselves
- checking if the updated documentation exists
- verify if the error handling and exceptions are meaningful
- one thing should be achieved by one flow not the multiple ones e.g. POST user, there should be one POST for the specific user multiple POSTs endpoints which saving similar data