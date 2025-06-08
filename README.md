# Challenges
* First, I didn't work before with dataminig. I am not sure how to get all the info that is described.
* It is missing some examples to get the required info.
* I am not familiar with the area described, medical. Even if I get info, I need to confirm this info is the expected one.
* I am thinking in the organization of the project, and I maybe create the required elements for the project, but the minig part.
  * I reviewed a course of prompting and way to use an specific data set to create queries to your own SUBSET of information.
  * Probably using that you are be able to reply with the queries.
* It is too many tasks, but I think the initial requirement to create a project and dockerize them, it may be create by Gemini or copilot, probably REPLIT

## Steps to complete project
* List and understand all requirements
* Define which will be my first point
* How many mocking can I do in project?
  * `the focus is shifting away from architecture and code quality (which are still very important) and toward more problem-solving, business/product understanding, and AI usage.` based on this point, the final code it is NOT the MAIN point.
  * I will start researching about the OBJECTIVE and create some examples of the APIs
  * Once I have some examples, I will research where can I get the info


## DATA MINING
### Research
* Research which pages are the required info.
* What is the info required to be saved in a DB

### Data
* Once found the information for the DB, create a mock of data.
* Gets a example for the DB

### LLM GEMINI
* Research how to read info from the DB to complete to QUERY PART

# Research of requirement
* `● Scrape or parse DailyMed drug labels for Dupixent.`
* Dailymed page: https://dailymed.nlm.nih.gov/dailymed/
* Dupixent: https://dailymed.nlm.nih.gov/dailymed/drugInfo.cfm?setid=595f437d-2729-40bb-9c62-c8ece1f82780
* Request to Gemini to describe me the DailyMed and Dupixent
* Request to Gemini what is a ICD-10

## Notes initial research
* Is it about a specific meds or should it be generic
* FOR MANY:
  * System needs to have all XML locally and have a system than can parse them.
  * How can we detect the file is new and requires a RE SYNC?
* ICD-10 needs to be processed by LLM, I need to research how to use a LLM and open source
  * Where can I find a free open source and with which LLM?
  * What will be my limit using the LLM.
* Where do I find a free data source of ICD-10

# Architecture of project
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

Requirements:
* Detect file changes (runs every X minutes)
* Needs be a service or RUN at demand

## Conversor
Features
* Once the data is saved on tables, starts to review which new items needs to be CONVERTED
* For each indication request a LLM API
* Save a 2nd block of data
  * setid
  * indicationId
  * ICD-10 code
  * date
  * other notes?

Requirements
* When should be run, for how long it will wait?

## API
Features:
● Implement CRUD operations:
  ○ Create, read, update, and delete drug-indication mappings.
● Authentication & Authorization
  ○ Users should be able to register and log in.
  ○ Implement role-based access control.
