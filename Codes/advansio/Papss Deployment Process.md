## Deployment Process Note (Doc)

**These are steps reproducible to deploy the Papss Payment Platform on Linux server:**

1. Log in as sudo user with the ff command :
   cmd: sudo su

2. Install Docker on the server :

- OPTION 1: Redhat OS
  cmd: yum install docker
  cmd: yum remove docker docker-common docker-selinux docker-engine
  cmd: yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
  cmd: yum install docker-ce

- OPTION 2: Ubuntu OS
  cmd: snap install docker
  OR
  cmd: for pkg in docker.io docker-doc docker-compose docker-compose-v2 podman-docker containerd runc; do sudo apt-get remove $pkg; done

3. Install Postgresql on the server :
   cmd: docker pull postgres
   cmd: docker run --name postgres -p 5432:5432 -e POSTGRES_PASSWORD=postgres -d postgres

   **NB: There is no need to install the postgresql if there is an existing database server to host the Papss database**

4. Create Papss directories for Bridge 1, Bridge 2, and Web in 'opt' directory:
   cmd: cd /opt/
   cmd: mkdir papss-bridge1
   cmd: mkdir papss-bridge2
   cmd: mkdir papss-web

5. Copy respective Dockerfile files from Dev server to Prod server for Papss Bridge 1, Bridge 2, and Web:
   i Copy Dockerfile in /opt/papss-bridge1 from the Dev server to /opt/papss-bridge1 on the Prod server
   ii Repeat step i for directories papss-bridge2 and papss-web

6. Build the docker images, create and run the containers
   i Navigate to papss-bridge1 directory on Prod server at /opt/papss-bridge1 and run the ff
   **NB : Please note the dot at the end of the command**
   cmd: docker build --no-cache -t papss-bridge1 .
   cmd: docker run --name papss-bridge1 -p 9021:9021 -v /opt/papss-bridge1/logs/:/logs/ -d papss-bridge1

   ii Navigate to papss-bridge2 directory on the Prod server and run the ff
   **NB : Please note the dot at the end of the command**
   cmd: docker build --no-cache -t papss-bridge2 .
   cmd: docker run --name papss-bridge2 -p 7221:7221 -v /opt/papss-bridge2/logs/:/logs/ -e CORE_BANKING_BASE_URL=https://wso2am-gateway-bpr-uat-gateway.apps.kedrocpd01.kcbad.com -e DB_HOST=192.168.24.174 -e DB_PORT=1535 -e DB_USER=PAPSSDB -e DB_SCHEMA=PAPSS -e DB_PASS=Pab55db#2024# -e LDAP_URL=ldap://10.216.1.100 -e LDAP_USE=true -d papss-bridge2

   iii Create the directory /usr/share/nginx/html
   cmd: mkdir -p /usr/share/nginx/html

   iv Create the file default.conf in the /etc/nginx/conf.d/ directory
   cmd: mkdir -p /etc/nginx/conf.d/
   cmd: cd /etc/nginx/conf.d/
   cmd: touch default.conf

   iv Navigate to papss-web directory on the Prod server and run the ff
   **NB : Please note the dot at the end of the command**
   cmd: docker build --no-cache -t papss-web .
   cmd: docker run --restart always --name papss-web -p 80:80 -d papss-web
