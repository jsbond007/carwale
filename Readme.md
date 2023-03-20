**This Project has two Parts**
1. Backend
2. Frontend

## Configure and Run Backend
Following are the Pre-requisites - 
- .Net 6.0 SDK 
- Visual Studio 2022 or Visual Studo Code

**Steps ** **How to Run **
	- Open the Solution in Visual Studio 2022
	- Go to appsettings.json
			- Change RunDbMigration to true when running first time
			 - Run the Project with "CarWale" profile (not iis)
			- After first run this will create cw-sqllite.db in your main folder
			- After migration is run successfully please make this setting back to false
			- After First migrationi this will also seed four users and two tenants in the database
			 - Following are users created - muser1, muser2, muser3, muser4
			 - All user has same default password "Abc@12345"

- Project should run on two Ports - https - 7005 and http 5005
- Go to https://localhost:7005/swagger/index.html
- This should show all the APIs documentation

**Using Apis**
- Go to https://localhost:7005/swagger/index.html or  http://localhost:5005/swagger/index.html

- All Apis require an Authenticated user
- So acquire a token first
	 - Go to Auth 
	 	 - Call /Auth/login from Swagger Api Explorer
		 - It requires "UserName" and "Password", use any of the users created as default
		 - After successfull login this will return response with token 
		 - You can use this token to Authorize further requests

##Run Frontend
Following are the Pre-requisites - 
- Node Version 18+

**Steps how to run **
- Go to command prompt and change to in the fronend root directory 
- Run **npm i --force**
- force is required for color library has a lower React version depedency (we can fix it later)
- Run **npm run start**
- This will compile and run the project on port no. - http / 3000
as http://localhost:3000




