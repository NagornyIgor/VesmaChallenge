# Vesma Challenge

## Startup
### Docker
Ensure that [Docker](https://docs.docker.com/get-started/get-docker/) is installed on your system.

You can start the application from Visual Studio using "docker-compose" profile.

In case you want to setup docker container manually:
Navigate to the root directory of the repository, open a console, and run the following command:
```shell
docker-compose -f .\docker-compose.yml up --build -d
```

This command starts the Docker container, which host the API service.
You can interact with the containers using their exposed ports.

To access the api reference go to `http://localhost:{port}/scalar`:
* http://localhost:8010/scalar
* https://localhost:8011/scalar