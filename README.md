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
 * Add initial projects
 * Start with dupixent and test later with other drugs

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
* Here is the link of GEMINI questions I did: https://g.co/gemini/share/4a8b5bff4e3f
  * About use LLM and some free data set, I will require time for finding one and LEARN how to use this dataset with the LLM.
  * I review some course how to send request to LLM for an specific and small dataset.

# Architecture
Moved information to file [Architecture.md](Architecture.md)

# How to execute
## Run docker-compose
The code was build in Visual Studio in Windows. I have installed WSL 2 and Docker.
1. Open Powershell and go to folder `cd ./docker/`
2. execute `builder_images.ps1` This will build the 3 images required for the docker-compose
3. Go to folder `cd compose`
4. Execute `docker-compose up` , don't use `-d` it is only to validate the services are running.
5. Add more xml of drugs in `./docker/compose/parserData`, once file changed is detected it should be parsed.
  * Last Updated: this expected behavior is not working. It is required to review if a permission is requried for WSL folder or docker

# Final notes
* This way to use LLM and AI, in this moment, it is out of the scope of my knowledge.
* The amount of tasks requested are too big for the initial hours was commented in the beggining for the challenge.
* I focused in organization of tasks and thinking in Architecture POV for future improvement.
* I really enjoyed to request more to AI, the explanation about main concepts and how improved my initial architecture is great.
* Thanks to this challenge I will go to learn and understand more about LLM and use in some personal projects or courses.

## Reply to: How would you lead an engineering team to implement and maintain this project?
I will assume that all of the team are like me, without a knowledge of many or all topics
* Divide the tasks for researching:
  * Research about specific topics of the problem.
    * Deliverable: example of what data is expected to get from the XML and how at least one or 2 indications are converted to ICD-10
  * Research about LLM and how to use dataset.
    * Deliverable: example of how a LLM uses a small dataset for reply
* Prepare and display to team what will be our organization.
* Define with the team at least:
  * Common models used around project
  * Common interfaces or data that will be used between projects
* Divide the tasks for projects
* Define the minimum documentation and Unit Tests required
  * I like QA notes, other team member could review the notes and test or continue an update based on that
  * A good coverage is always good, but the Unit Tests should validate we process errors
* We need automation, meanwhile we develop, QA can:
  * Define which elements can be tested at the beginning
  * Continue reviewing what are the info required for correctly testing.
