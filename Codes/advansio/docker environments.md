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

EXPOSE 7721

#CMD ["java","-DkeyAlias=tepe", "-jar", "./PapssService-0.0.1-SNAPSHOT.jar", "--spring.config.location=./application.properties"]

CMD java -jar ./papss-0.0.1-SNAPSHOT.jar --spring.config.location=./bridge2-application.properties