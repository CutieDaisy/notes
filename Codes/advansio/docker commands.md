### Commands
## Build
_NB: the dot at the end of the command is important_
docker build --no-cache -t [image name eg: papss-bridge2] .

## Run
docker run -p 7721:7721 [image name eg: papss-bridge2]

## View All Container's Configuration And Settings
docker inspect <container_name_or_id>
## View Specific Container Variables
docker exec sqlserver env
docker inspect --format '{{.Config.Env}}' <container_name_or_id>
docker inspect --format '{{json .Config.Env}}' <container_name_or_id>
docker inspect --format '{{json .NetworkSettings}}' <container_name_or_id>



## View Running Containers
docker ps
## View Images
docker images
## View Volumes
docker volume ls

## View Ports
docker ps


## View Logs
docker logs [container name eg: papss-bridge2]