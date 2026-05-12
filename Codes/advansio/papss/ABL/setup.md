
### ABL PAPSS Bridge Setup

# Bridge 1
docker run --restart always --name papss-bridge1 -p 11000:9021 -p 9021:9021 -e CORE_BANK_API_URL=http://172.17.0.2:7221/papss/incoming/ -e PAPSS_CONFIG_BIC=LR100012 -e PAPSS_CONFIG_SWIFT_CODE=ACLILRLM -e PAPSS_CONFIG_KEY_STORE_PATH=/opt/papss-bridge1/papss_accessbankliberia_com.jks -e PAPSS_CONFIG_KEY_PASS=Abl@2024 -e PAPSS_CONFIG_IPS_URL=pm.prd.papssnet.papss.com -v /opt/papss-bridge1/:/opt/papss-bridge1/ -v /opt/papss-bridge1/logs/:/logs/ --log-opt max-size=10m --log-opt max-file=3 -d 533b88f6dae3


# Bridge 2
docker run --restart always -v /etc/hosts:/etc/hosts --name papss-bridge2 -p 7221:7221 -p 10500:7221 -v /opt/papss-bridge2/logs/:/logs/ -e DB_HOST=172.18.253.91 -e PAPSS_BRIDGE1_CCY=LRD -e PAPSS_BRIDGE1_COUNTRY=LR -e PAPSS_ID=LR100012 -e PAPSS_SWIFT_CODE=ACLILRLM -e CORE_BANKING_BASE_URL=https://papi.accessbank.com.lr/ -e LDAP_URL=ldap://172.18.1.2:636 -e LDAP_USE=true -e LDAP_API_KEY= -e LDAP_APP_ID= -e 2FA_ENABLED=false -e LDAP_USERNAME_PREFIX=ACCESSBANK -e CORE_BANKING_PASS=%%Ablp@2024^^ -e PAPSS_BRIDGE1_BASE=http://172.17.0.3:9021/papss/ -d papss-bridge2-06112024

docker run --restart always -v /etc/hosts:/etc/hosts --name papss-bridge2 -p 7221:7221 -p 10500:7221 -v /opt/papss-bridge2/logs/:/logs/ -e DB_HOST=172.18.253.91 -e PAPSS_BRIDGE1_CCY=LRD -e PAPSS_BRIDGE1_COUNTRY=LR -e PAPSS_ID=LR100012 -e PAPSS_SWIFT_CODE=ACLILRLM -e CORE_BANKING_BASE_URL=https://papi.accessbank.com.lr/ -e LDAP_URL=ldaps://172.18.1.3:389 -e LDAP_USE=true -e LDAP_API_KEY= -e LDAP_APP_ID= -e 2FA_ENABLED=false -e LDAP_USERNAME_PREFIX=ACCESSBANK -e CORE_BANKING_PASS=%%Ablp@2024^^ -e PAPSS_BRIDGE1_BASE=http://172.17.0.3:9021/papss/ -d papss-bridge2-06112024


# Web UI
docker run --restart always --name papss-web2 -p 443:80 -p 80:80 -v /opt/papss-web/default.conf:/etc/nginx/conf.d/default.conf -v /etc/hosts:/etc/hosts -d papss-web-21032025


LDAP Domain Name : ad.accessbank.com.lr

 "Env": [
                "LDAP_APP_ID=",
                "LDAP_USERNAME_PREFIX=ACCESSBANK",
                "CORE_BANKING_PASS=%%Ablp@2024^^",
                "PAPSS_BRIDGE1_CCY=LRD",
                "PAPSS_ID=LR100012",
                "LDAP_USE=true",
                "PAPSS_BRIDGE1_COUNTRY=LR",
                "DB_HOST=172.18.253.91",
                "LDAP_API_KEY=",
                "2FA_ENABLED=false",
                "PAPSS_BRIDGE1_BASE=http://172.17.0.3:9021/papss/",
                "PAPSS_SWIFT_CODE=ACLILRLM",
                "CORE_BANKING_BASE_URL=https://papi.accessbank.com.lr/",
                "LDAP_URL=ldap://172.18.1.2:636",
                "PATH=/usr/java/openjdk-23/bin:/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin",
                "JAVA_HOME=/usr/java/openjdk-23",
                "LANG=C.UTF-8",
                "JAVA_VERSION=23-ea+3"
            ],