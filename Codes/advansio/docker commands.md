### Commands

## Build

_NB: the dot at the end of the command is important_
docker build --no-cache -t [image name eg: papss-bridge2] .

## Run

docker run -p 7721:7721 [image name eg: papss-bridge2]

## Running Papss-Bridge2 Container with Env Variables

docker run --name papss-bridge2 -p 7221:7221 -v /opt/papss-bridge2/logs/:/logs/ -e DB_HOST=172.17.0.1 -e PAPSS_BRIDGE1_CCY=LRD -e PAPSS_BRIDGE1_COUNTRY=LR -e PAPSS_ID=LR1009 -e PAPSS_SWIFT_CODE=ACLILRLM -e CORE_BANKING_BASE_URL=http://172.18.105.91/ -e LDAP_URL=ldap://172.18.106.3:389 -e LDAP_USE=true -e LDAP_API_KEY= -e LDAP_APP_ID= -e 2FA_ENABLED=false -e LDAP_USERNAME_PREFIX=ACCESSBANKTEST -d papss-bridge2

_NB: The (LDAP_USERNAME_PREFIX) is the Domain Unit or Name prefix added to the usernames when logging in_
Example: ACCESSBANKTEST\rashid

-v /opt/mana/logs/:/logs/: This option is used to mount a volume. It maps the directory /opt/mana/logs/ on your host machine to the /logs/ directory inside the container. This allows the container to read and write to the host directory, which is useful for persisting data or sharing files between the host and the container12.

-d: This option stands for detached mode. It runs the container in the background and prints the container ID. This is useful when you want the container to run continuously without tying up your terminal

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


## View Container files
docker exec -it <container_name> /bin/sh
OR
docker exec -it <container_name> /bin/bash

## Copy files from or to Container
docker cp <container>:/file/path/nginx/html/main.585967f5c5468100.js ./
