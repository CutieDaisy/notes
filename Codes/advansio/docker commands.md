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

Papss Deployment Document

ifconfig
2 EXIT
3 exit
4 cat /etc/os-release
5 apt update
6 apt upgrade
7 sudo apt-get update
8 sudo apt-get install ca-certificates curl
9 sudo install -m 0755 -d /etc/apt/keyrings
10 sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
11 sudo chmod a+r /etc/apt/keyrings/docker.asc
12 echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
 13 $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
14 sudo apt-get update
15 sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
16 docker ps -a
17 docker pull postgres
18 ls
19 docker ps -a
20 docker images
21 docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres
22 docker ps -a
23 docker run -it --rm postgres psql -h 172.17.0.1 -U postgres
24 docker run -it --rm postgres psql -h 172.17.0.1 -U papss
25 cd /opt/
26 mkdir papss-bridge1
27 cd papss-bridge1/
28 nano Dockerfile
29 docker buidl -t --no-cache papss-bridge1 .
30 docker build -t --no-cache papss-bridge1 .
31 docker build --no-cache -t papss-bridge1 .
32 init 6
33 docker ps -a
34 docker start postgres

docker build --no-cache -t papss-bridge2 .

docker run --name papss-bridge1 -p 9021:9021 -v /opt/papss-bridge1/logs/:/logs/ -d papss-bridge1

docker run --name papss-bridge2 -p 7221:7221 -v /opt/papss-bridge2/logs/:/logs/ -d papss-bridge2
