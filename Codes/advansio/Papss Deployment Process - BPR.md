## Deployment Process Note For BPR

**These are steps reproducible to deploy the Papss Payment Platform for BPR**

1. Log in as sudo user with the ff command :
   cmd: sudo su

2. Install Docker on the server :
   cmd: yum install docker
   cmd: yum remove docker docker-common docker-selinux docker-engine
   cmd: yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
   cmd: yum install docker-ce

3. Create Papss directories for Bridge 1, Bridge 2, and Web in 'opt' directory:
   cmd: cd /opt/
   cmd: mkdir papss-bridge1
   cmd: mkdir papss-bridge2
   cmd: mkdir papss-web

4. Copy respective Dockerfile files from Dev server to Prod server for Papss Bridge 1, Bridge 2, and Web:
   i Copy Dockerfile in /opt/papss-bridge1 from the Dev server to /opt/papss-bridge1 on the Prod server
   ii Repeat step i for directories papss-bridge2 and papss-web

   **Please ensure the ff details in the Dockerfile for papss-bridge2 are correct for Production:**
   * DB_HOST : 192.168.24.174  (Database Host)
   * DB_PORT : 1535  (Database Port)
   * DB_USER : PAPSSDB  (Database User)
   * DB_SCHEMA : PAPSS  (Database Schema or SID)
   * DB_PASS : Pab55db#2024#  (Database Password)
   * LDAP_URL : ldap://10.216.1.100  (LDAP Url)

5. Build the docker images, create and run the containers
   i Navigate to papss-bridge1 directory on Prod server at /opt/papss-bridge1 and run the ff
   **NB : Please note the dot at the end of the command**
   cmd: docker build --no-cache -t papss-bridge1 .
   cmd: docker run --restart always --name papss-bridge1 -p 9021:9021 -v /opt/papss-bridge1/logs/:/logs/ -d papss-bridge1

   ii Navigate to papss-bridge2 directory on the Prod server and run the ff
   **NB : Please note the dot at the end of the command**
   cmd: docker build --no-cache -t papss-bridge2 .
   cmd: docker run --restart always --name papss-bridge2 -p 7221:7221 -v /opt/papss-bridge2/logs/:/logs/ -d papss-bridge2

   iii Navigate to papss-web directory on the Prod server and run the ff
   **NB : Please note the dot at the end of the command**
   cmd: docker build --no-cache -t papss-web .
   cmd: docker run --restart always --name papss-web -p 80:80 -d papss-web

   We need the **Default.config** file
