# MachiniaAPI
This repository contains code of Machinia API which is a .Net Web API developed in C# with MongoDB as backend.
Machinia API contains below 3 API web call:
1.	POST call(http://.../TrainingCenter) - API to create a new training centre and it should be saved to the MongoDB. 
2.	GET call(http://.../TrainingCenters) – API to get list of all saved training centre from the database
3.	GET call(http://.../TrainingCenters/<CenterCode>) – API to get a training centre matching the CenterCode from the database

# Additional info for Sample Code
Below NuGet packages have been used:-
1.	MongoDB.Driver :- Official .NET driver for MongoDB.
2.	Microsoft.Extensions.Options :- Provides a strongly typed way of specifying and accessing settings using dependency injection.

# Prerequisite
1. The windows client OS system with Visual studio 2022 or higher, .net framework 6 with web development framework and MongoDB server installed
2. Active MongoDB server with database name as "MachiniaDB" and collection name as "Machinia" 

# Steps
1. 

  

