# Database
Version: MySQL 8.4.0

## Run locally
For local testing, go to dblocal folder and execute:
```powershell
cd docker/dblocal
docker-compose up
```
Use user and pass described in the docker-compose file and execute the scripts in order
1. `tables.sql` (only run once)
2. `sp.sql` (can run many times)
3. `tables.sql` (can run many times)

## Run with docker-compose of all components
Run the documentation in `README.md` for start the project.
Execute the same script in above described order

## Future updates
If udpates are required and data already exist follow this process:
* Update `tables.sql` for having latest changes.
* Create a folder to `run once`
  * Create a file for a block of changes added in the feature/bug
  * This file will update the required changes and it is only required to execute on db that contains data.
