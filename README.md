# GameLib
A system to manage game's borrowing (like a library with that borrow books, but for games...)

## Can i try it?
Of course, check [this out](https://game-lib-front.herokuapp.com/).

## To run the project
You can run every thing at once executing this command from the project root directory: ```docker-compose -f docker/docker-compose.yml up```
Check the docer/docker-compose.yml file to get to know the ports.

## .NET Core API Archtecture
For this project, a architecture with layers was chosen for better responsabilities separation. Naming the Layers from most external to more internal we have:
1. **Controllers Layer**: Make the comunication between server and client, allowing to se and iteract with the sistem information.
2. **Services Layer**: Responsable for keeping all business rules at one layer, increasing organization and reusability.
3. **Repository Layer**: Responsable for all data quering and changings from DB, making easier to change the DB access technology and keeping the project organized.
4. **Model Layer**: This layer has all systems modeling in terms of data, this been entities, view models, DTOs, and exceptions.
5. **Test Layer**: This layer that execute some casas on every push, to make sure that nothing that was OK is now geting broke.

## VueJs Archtecture
Nothing fenci here, it is a pretty default VueJS project.

<p align="center">
  <img height="700" src="doc/architecture.png?raw=true">
</p>


Note: Each layer is separated in a project.

## Main Technologies & Libraries used
### Back-end
1. DotNet Core 3.1 (Framework Web)
2. Entity Famework 3.1 (ORM)
3. Dependency Injection
4. Entity Framework Migrations
5. Global exception handling with exception filter
6. DotNet Filters
7. C# Extensions
### Front-end
1. HTML / CSS / Javascript
2. VueJs
3. Vuetify
4. VueX
5. VueRouter
6. Axios
7. scss/sass
 
### Others project and develop technologies
1. gitignore.io (to generate gitignore)
2. Database PostgreSQL
3. Docker & Docker Compose
4. Nuget
5. Unit Test
6. Continuous integration, using GitHub Actions
7. Deploy (Not continuous) with Heroku

## Modeling
Frist a conceptual entity relational diagram was made:
<p align="center">
  <img height="250" src="doc/concept_ER.png?raw=true">
</p>

Then a logic entity realtional diagram:
<p align="center">
  <img height="300" src="doc/logical_ER.png?raw=true">
</p>

And finally the classes.
