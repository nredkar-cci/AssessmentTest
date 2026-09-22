# AssessmentTest
This is a **.NET project** — the repository name does not reflect the project's content.
The repository was pre-created with this name so that project setup time is excluded from the time allocated for the assessment test. This was requested by the person supervising the .NET assessment test.

Actual Project Name is FitConnect.

# This project has 2 flows:
    1: Onboarding a user as a client
    2: Onboarding a user as a fitness coach

# There are 4 tables in total
    1: User table: Person must first be registered with the platform. Once registered there will be entry in the user table and User will get "User" Role by default. 
    2: Client table: This table is utilized when we need to onboard user as client who will subscribe to plans offered by fitness coaches. 
    3: FitnessCoach table: This table is utilized for onboard the user as fitness coach. Coach needs to be atleast 2 years of expirence and IsCertified flag must be active to become a coach.
    4: Plans: This table holds to plans for the fitness coach. 

To solve the given problem with limited time had cut corner on implmenting the custom plans for each fitness coach based on thier preferences. Instead kept a fixed plans based on the level of coach. If coach is elite than she/he has different plans and if coach is regular than she/he has different plans.

# Note: Custom plans for each fitness coach can be implemented by introducing the Junction table.

# Another point to note:
I have not created a postman collect but instead  swagger is integrated in the .NET app with authentication. 
However /openapi/v1.json file in swagger UI can be used to import collection in postman.

# There is residue code of template which has background worker and email functionality. I have not integrated this code in the problem. It can be used for future purpose.

