# Architecture of project
* Created an initial idea, improvement with AI.

## Parser 
Features:
* Read a folder with XMLs files (recursively)
* Parser at least:
  * setid
  * Name
  * Category
  * Indications {muchos indications}
  * Dates when it was saved
* Save in DB previous information
* Save info in batches
* Configuration
  * DB Information
  * Folder

Requirements:
* Detect file changes (runs every X minutes)
  * Review FileSystemWatcher  if we will work with few or many files
* Needs be a service or RUN at demand
* Create a queue of notification 

## Conversor
Features
* Once the data is saved on tables, starts to review which new items needs to be CONVERTED
* For each indication request a LLM API
* Save a 2nd block of data
  * setid
  * indicationId
  * ICD-10 code
  * date
  * other ifo?
* Configuration:
  * API_KEYs we will need for the LLM
  * DB Information
  * Batch processor
  * Unique/Identifier for the current application

Requirements
* When should be run, for how long it will wait?

## API
Features:
* Implement CRUD operations:
  * Create, read, update, and delete drug-indication mappings.
* Authentication & Authorization
  * Users should be able to register and log in.
  * Implement role-based access control.
* Organization:
  * API layer
* Configuration:
  * API_KEYS for DB access

## DB
Features:
* Support the data for Parser and Conversor
* If possible, create triggers that can call or react with code (some queries)
* CRUD is it possible to use firebase user management
* Use SP all the time, for hiding real implementation

## Common libraries
* Models should be common
* Settings reader
* Common Log Configuration
* DB code for CRUD

## Reason of organization
Having different apps/service, each one can be started independtly.

At least the first time, Parser and Conversor will require a 

# C4 level 1
Go to file: [C4_L1.puml](./docs/C4_L1.puml)

# Achitecture reoganized
After giving my initial idea, Gemini recommend different changes

The one I recognize that will improve a lot are:
* Use Filewatches
* Use SQS for communicating events. I am still thinking that working in batches in DB will be faster, but communication of new work it is also good with SQS probably with batches too.

## DailyMed (External): Source of SPL XML.
## Parser (C# Service/Daemon):
Monitors a configured folder (using FileSystemWatcher or scheduled polling).
Reads and parses SPL XML files.
Saves SetId, Name, Category, and raw Indications text into the Drugs and Indications tables in the SQL Database.
After successful DB save, publishes a message to a Message Queue for each new or updated indication record that requires conversion.
Message Queue (e.g., RabbitMQ, Azure Service Bus):
Acts as a buffer and decoupler between Parser and Conversor.
Ensures reliable delivery of messages.
## Conversor (C# Service/Worker):
Continuously consumes messages from the Message Queue.
For each message, it retrieves the raw Indication text from the DB.
Makes a batched, asynchronous call to the LLM API to get ICD-10 codes.
Implements robust retry logic with exponential backoff for LLM API failures.
Updates the Status of the Indication record in the SQL Database (e.g., to CONVERTED or CONVERSION_FAILED).
Saves the ICD-10 Mappings (ICD-10 code, description, confidence, date) to the Icd10Mappings table in the SQL Database.
### LLM API (External): Provides the text-to-ICD-10 conversion.
### API (ASP.NET Core Web API):
Exposes RESTful endpoints for CRUD operations on drug data and ICD-10 mappings.
Uses Firebase Authentication for user registration and login.
Validates Firebase tokens on incoming requests.
Implements role-based authorization to control access to different data and operations.
Interacts with the SQL Database (via an ORM like Entity Framework) for data retrieval and storage.
### SQL Database:
Stores Drugs, Indications, Icd10Mappings, and potentially Users and Roles tables.
Optimized with indices for query performance.
No complex triggers for business logic.
### User: Interacts with the API to query processed data.
