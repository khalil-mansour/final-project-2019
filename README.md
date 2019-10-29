# Development setup

The following guide will help you setup your dev environment.

## Installing docker

Select one of the following links for your setup

### Windows

[`https://hub.docker.com/editions/community/docker-ce-desktop-windows`](https://hub.docker.com/editions/community/docker-ce-desktop-windows)

### Linux

[`https://docs.docker.com/install/linux/docker-ce/ubuntu/`](https://docs.docker.com/install/linux/docker-ce/ubuntu/)

### MAC

[`https://docs.docker.com/docker-for-mac/install/`](https://docs.docker.com/docker-for-mac/install/)

## Usefull command
    ```bash
    docker-compose ps # <-- list the running containers
	docker-compose down <container name> # <-- remove the container. It will be rebuild next docker-compose up
    docker-compose stop <container name> # <-- stop the container without removing it
	docker-compose start <container name> # <-- start the container
    ```
##### Source : [Docker-compose command](https://docs.docker.com/compose/reference/)

## Building the docker image with the dev-database

1. In your CI, run the following command : 
`docker-compose up -d `

2. Use *pgAdmin 4* to create a connection and test that everything is working.
- username: postgres
- password: postgres

`The default port binding is *localhost:8083*.`


## Environment Variables

Environment variables are hidden and fetched from git using the **env_mapper.py** script.
A gitlab-runner will execute this script after the build stage for dotnet (*dotnet restore* and *dotnet build* ).
Using a configuration file (**config.ini**), the python script will map the environment variables to the corresponding keys in **appsettings.json**. This way,
the environment vars are separated from the code.

In order to add environment variables, refer to the following steps:
1. [Add the KVP to the repository](https://depot.dinf.usherbrooke.ca/projets/a19/eq10/projet_assurance/projet_assurance_backend/-/settings/ci_cd).
2. In **config.ini**, add the KVP under [env] using the following format :
   `KEY=$VALUE` 

    Make sure $VALUE is the same as the key you entered in the repo.



## Deployment staging
##### Source : [Deploying a containerized web application](https://cloud.google.com/kubernetes-engine/docs/tutorials/hello-app)

1. In the gcloud console set-up the console so it is bind to your project
    ```bash
    gcloud config set project [PROJECT_ID]
    gcloud config set compute/zone [COMPUTE_ZONE] # <-- northamerica-northeast1-a
    gcloud container clusters get-credentials [CLUSTER_NAME]
    ```

2. This step is only require if it's the first time deploying
    ```bash
    kubectl run hello-web --image=gcr.io/${PROJECT_ID}/${IMAGE} --port 8080
    kubectl get pods # <-- Check if your pod are up
    kubectl expose deployment hello-web --type=LoadBalancer --port 80 --target-port 8080
    kubectl get service hello-web # <-- Get external_ip to acces the API
    ```
    
3. Update the the pod with the latest image
    ```bash
    kubectl set image deployment/hello-web hello-web=gcr.io/${PROJECT_ID}/{$IMAGE}
    ```

# Backend development guide

In order to understand how the backend side of the project is configured to work, one must understand the principles of a layered architecture and the importance of isolating the different components of a solution.
An onion style architecture (also known as clean arch) is a way to setup your code with the purpose of isolating into layers (projects) different parts of your code and insuring that no dependencies from an outer layer are needed for them to work.
This allows for easier testing, cleaner code and offers versatility in terms of change. However, this type of architecture can be initially confusing as it creates a lot of overhead even for basic use cases. Just like an onion, it can make you cry
but if you know how to cook it, it's really tasty.

The purpose of this guide is to clear any misunderstandings and to use a preexisting use case as a template.

**FileUploadUseCase**

## Core Layer (also known as Business Layer)

The core layer is where the central business rules are located. It is mostly pure C#. Here is what lies within the core.

### Entities

Entities are the business objects that are to be manipulated by use cases. In our current example, the entity concerned is a File.

### Data Transfer Objects (DTO)

The DTOs are objects that carry data between processes. Their whole purpose is to aggregate data into one call that otherwise would have been transfered using several calls.
DTOs are simple objects that should not contain any business logic but may contain serialization and deserialization mechanisms. This project contains 3 types of DTOs :

- UseCaseRequests
- UseCaseResponses
- GatewayResponses (Responses from Infrastructure)

### Use Cases 

Use cases are where the all the logic resides in the Core. Typically, use cases handle a very specific chore and are autological/homological (ex. FileUploadUseCase.cs). This means that each use case is modular and serves only one purpose.
The use cases need a reference to the interfaces that the Infrastructure layer will implement so the constructor must contain a repository interface (in our case: IFileRepository).
Each use case implements an interface (ex. IFileUploadUseCase) which also implements the **Handle** method of the overarching use case request handler interface. More info on interfaces below. This **Handle** method
is a task that returns a boolean and has two arguments : *message* & *outputPort*

The message parameter is an Input Port whose sole responsibility is to carry the request model into the use case from the upper layer that triggers it (UI, controller etc.).

The *outputPort* is an abstraction of the response and has its own **Handle** method. The purpose of this is to be able to get the response out to the caller in a suitable format based on if the use case results were as expected or not (response.Success ?) and to filter the reponse returned through a *Presenter* that will be implemented in the outer layer.
This allows us to completely isolate UI/Framework dependencies from our use case. More details on the *Presenter* in Presentation Layer.

Inside the **Handle** method, a *response* object is created by an asynchronous call to the interface's task (The task is implemented in the Infrastructure layer). In order to establish an agreed upon precedent, make sure to explicitly call this object ''response''.

### Interfaces

Since the code in this layer is mostly pure C#, interfaces represent the external dependencies and their implementations get injected into the use cases.

Here is a list of the required interfaces and their purpose :

- Repositories : Represent what the Infrastructure/Database layer has to implement (ex. IFileRepository -> Create, Fetch, Fetchall, etc.).
- IUseCaseRequestHandler : Defines the shape of all of our use case classes.
- UseCases : Simple interface that implements IUseCaseRequestHandler.
- IUseCaseRequest : Outputs a UseCaseResponse and is implemented by the UseCaseRequest DTOs.
- IOutputPort : Defines how the response is returned to caller and is implemented in the Presentation layer.

The important thing to note about these interfaces is that they serve to inject implementations from outer layers into the core. When working on a specific use case, start by adding an interface for the use case to implement
(ex. IFileUploadUseCase) and if it's a new entity, add it's interface to the repositories (ex. IFileRepository).

### Dependencies (CoreConfigureServices)

This static class contains a single method **MapCoreServices** that serves to inject the dependencies needed for your use case. This method gets called by the runtime to add services to the container.
When creating a new use case, make sure to add it to the services with its specific interface.

## Presentation Layer

The presentation layer is the layer responsible for handling models, controllers, endpoints, web pages, devices, etc.
It is in the outermost part of the ''onion'' and is responsible for formatting the response data from the use cases to a suitable format for the caller.

### Controllers

The controllers are the application's entrypoints. In the context of a REST Api, these classes all contain actions that receive requests and handle the particular use cases.
Each action has a specific route and returns data through a presenter.

In our case, The **FileController** handles all the use cases related to the *File* entity.

### Models

There are two types of models. Here is their purpose.

- Request model : Data received from caller.
- Response model : Data sent as response after use case completion.

Models contain no logic but can have serialization/deserialization properties attributed to the fields.

### Presenters

The presenters contain .NET framewrok-specific references and types and therefore should not bleed into the use cases, as per the *dependency rule*.
Presenters implement the use case's *outputPorts* with all of the work being handled by its **Handle** method (pun intended).
In this approach, we use DIP to invert the control flow so rather than getting a return value with the response data from the use case we allow it to decide when and where it sends the data through the output port to the presenter.
We could use a callback, but for our purposes, the presenter creates an http response message that is returned by our controller.
From there, we're just building a regular MVC ContentResult and in this case, directly storing the serialized results from the use case and setting the appropriate HttpStatusCode based on the result of the use case.


## Infrastructure Layer (also known as Data Layer)

### Repositories

This layer contains the application's data access and persistence concerns and any frameworks, drivers or dependencies they require.
In this layer, the repositories implement all the methods from their respective interfaces from the core layer and return a response depending on if the database queries were successful or not.

In our specific use case, we would have a *FileRepository* that implements the *IFileRepository* interface from the core layer and it would contain the signed methods. The methods contain the queries (using Dapper) and the returned responses.

### Dependencies (InfrastructureConfigureServices)

This static class contains a single method **MapInfrastructureServices** that serves to inject the dependencies needed for your repositories. This method gets called by the runtime to add services to the container.
When creating a new use case, make sure to add it to the services with its specific interface.


