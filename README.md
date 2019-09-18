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



## Building the docker image with the dev-database

1. In your CI, run the following command : 
`docker-compose up -d `

2. Execute the **docker-compose.yml** which will create the dev database in an image.

3. Use *pgAdmin 4* to create a connection and test that everything is working.
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