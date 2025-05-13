Bill Payment
Cash Token
Commission
Device Location
Platform

"Env": [
                "PAPSS_ID=LR100012",
                "PAPSS_BRIDGE2_BASE_URL:http://172.17.0.3:7221/papss/incoming",
                "PATH=/usr/java/openjdk-23/bin:/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin",
                "JAVA_HOME=/usr/java/openjdk-23",
                "LANG=C.UTF-8",
                "JAVA_VERSION=23-ea+3",
                "SSL_ALIAS=server",
                "PAPSS_BIC=LR100012",
                "SWIFT_CODE=ACLILRLM",
                "KEYSTORE_PATH=./papss_accessbankliberia_com.jks",
                "PORT=11000",
                "PAPSS_BRIDGE2_BASE_URL=http://172.17.0.3:7221/papss/incoming/",
                "PAPSS_BRIDGE2_USER=AC",
                "PAPSS_BRIDGE2_PASSWORD=AC",
                "COUNTRIES_WITH_ZERO_DECIMALS="


                
-e PAPSS_ID=LR100012 -e SSL_ALIAS=server -e PAPSS_BIC=LR100012 -e SWIFT_CODE=ACLILRLM -e KEYSTORE_PATH=./papss_accessbankliberia_com.jks -e PORT=11000 -e PAPSS_BRIDGE2_BASE_URL=http://172.17.0.3:7221/papss/incoming/ -e PAPSS_BRIDGE2_USER=AC -e PAPSS_BRIDGE2_PASSWORD=AC -e COUNTRIES_WITH_ZERO_DECIMALS
            ],


docker run --restart always --name papss-bridge1-07052025 -v /opt/papss-bridge1/logs/:/logs/       