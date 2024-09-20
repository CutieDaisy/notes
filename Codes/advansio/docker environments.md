### Environment

#FROM openjdk:23-ea-3-jdk-oraclelinux8
#FROM openjdk:23
FROM maven:3.9.8-eclipse-temurin-21 AS build
LABEL authors="lawrenceagbemava"
WORKDIR /

RUN \   
   curl -L -o ./bridge2-application.properties https://github.com/skytoo/papss-fbn/releases/download/dev-lr-access/bridge2-application.properties && \
   curl -L -o ./papss-0.0.1-SNAPSHOT.jar https://github.com/skytoo/papss-fbn/releases/download/dev-lr-access/papss-0.0.1-SNAPSHOT.jar

ENV PAPSS_ID=LR1009
ENV PAPSS_BRIDGE1_COUNTRY=LR
ENV PAPSS_BRIDGE1_CCY=LRD
ENV PAPSS_SWIFT_CODE=ACLILRLM
#ENV LDAP_URL=ldap://ad.accessbanktest.net
ENV LDAP_URL=ldap://172.18.106.3:389

ENV LDAP_API_KEY=
ENV LDAP_APP_ID=
ENV LDAP_USE=true
ENV 2FA_ENABLED=false


ENV SMTP_HOST=email-smtp.us-east-1.amazonaws.com
ENV SMTP_PORT=587
ENV SMTP_USER=AKIAU7OHGAGOFBY3X7A2
ENV SMTP_PASS=BPz3baAqUe0c8q/hWAVAeDTxUHrdLRBWyYiYXzuzZoNF
ENV SMTP_SENDER=info@oakwoodadvansio.com


EXPOSE 7721

#CMD ["java","-DkeyAlias=tepe", "-jar", "./PapssService-0.0.1-SNAPSHOT.jar", "--spring.config.location=./application.properties"]

CMD java -jar ./papss-0.0.1-SNAPSHOT.jar --spring.config.location=./bridge2-application.properties




## BPR Prod Environment

FROM openjdk:23-ea-3-jdk-oraclelinux8

WORKDIR /

RUN \
    curl -L -o ./bridge2-application.properties https://github.com/skytoo/papss-fbn/releases/download/dev-rw-bpr/bridge2-application.properties && \
    curl -L -o ./papss-0.0.1-SNAPSHOT.jar https://github.com/skytoo/papss-fbn/releases/download/dev-rw-bpr/papss-0.0.1-SNAPSHOT.jar

ENV DB_HOST=192.168.24.174
ENV DB_PORT=1535
ENV DB_USER=PAPSSDB
ENV DB_PASS=Pab55db#2024#
ENV DB_SCHEMA=PAPSS

ENV LDAP_USE=true
ENV LDAP_URL=ldap://10.216.1.100

ENV CORE_BANKING_BASE_URL=https://wso2am-gateway-bpr-uat-gateway.apps.kedrocpd01.kcbad.com

EXPOSE 7721

CMD java -jar ./papss-0.0.1-SNAPSHOT.jar --spring.config.location=./bridge2-application.properties

## ------ End of BPR Prod Environment ------


## BPR DockerFile Backup --------------------------
FROM openjdk:23-ea-3-jdk-oraclelinux8

WORKDIR /

RUN \
    curl -L -o ./bridge2-application.properties https://github.com/skytoo/papss-fbn/releases/download/dev-rw-bpr/bridge2-application.properties && \
    curl -L -o ./papss-0.0.1-SNAPSHOT.jar https://github.com/skytoo/papss-fbn/releases/download/dev-rw-bpr/papss-0.0.1-SNAPSHOT.jar


ENV DB_HOST=192.168.24.174
ENV DB_PORT=1535
ENV DB_USER=PAPSSDBDEV
ENV DB_PASS=Pab55db#2024#
ENV DB_SCHEMA=PAPSS

ENV SMTP_HOST=email-smtp.us-east-1.amazonaws.com
ENV SMTP_PORT=587
ENV SMTP_USER=AKIAU7OHGAGOFBY3X7A2
ENV SMTP_PASS=BPz3baAqUe0c8q/hWAVAeDTxUHrdLRBWyYiYXzuzZoNF
ENV SMTP_SENDER=info@oakwoodadvansio.com

ENV CORE_BANKING_BASE_URL=https://wso2am-gateway-bpr-uat-gateway.apps.kedrocpd01.kcbad.com

EXPOSE 7721

CMD java -jar ./papss-0.0.1-SNAPSHOT.jar --spring.config.location=./bridge2-application.properties

## --------- End of BPR Dockerfile Backup -------------------------------------------