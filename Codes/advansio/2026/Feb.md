## 9th Feb
# USSD :
* Transfer to other bank is throwing error - **Log file for Feb 6th 2026** shows "Connection Error" - **Line 1224**
* Schedule  a meeting with Lynna to discuss the USSD error and possible solutions.

# Bloom Agency Banking Project:
    - Include the currency (at transaction level and at account(wallet) level)

# FBC :
    - Check the IP and Port
    - Provide channel api details to FBC for testing
    - 

# FBN :
    - DRC - Implementation of DRC for FBN (by tomorrow 10th Feb)



## 17th February 2026

## SL
* USSD
* Bloom Agency
* Unity Bank
* FBN DRC


## FBN Commands
 sudo docker run --name bridge2-sl --log-driver json-file --log-opt max-size=10m --log-opt max-file=1 -p 7221:7221 -e DB_HOST=172.22.0.1 -e DB_PORT=15432 -e DB_SCHEMA=papss_sl -e PAPSS_URL=http://172.17.0.1:9021/papss/ -e CORE_BANK_API_BASE_URL=https://fibridge-fin11-dev.fbn-devops-dev-asenv.appserviceenvironment.net/api/v1/ -e CORE_BANK_API_USER=PapssSlGm -e CORE_BANK_API_PASS=5c85cd00709d7a414f0156f451cccb51 -e CORE_BANK_API_BANK_CODE=07 -e CORE_BANK_API_COUNTRY_CODE=07 -e PAPSS_COUNTRY=SL -e PAPSS_ID=SL1012 -e PAPSS_BIC=ICBZSLFR -e PAPSS_CURR=SLE -d bridge-2


  ERROR com.oakwood.advansio.papss.service.impl.CoreBankingImpl193 [http-nio-7222-exec-10] [400 Bad Request] during [POST] to [https://fibridge-fin11-dev.fbn-devops-dev-asenv.appservic$
  "RequestId": null,
  "ResponseCode": "700",
  "ResponseMessage": "Client Not permitted"
}]



Conduit for Capital for government, central banks and co-operates


# Sunday 22 Feb
**2026-02-22 22:09:40,780**

FBN Get Account Details Response :
feign.FeignException$BadRequest: [400 Bad Request] during [POST] to [https://fibridge-fin11-dev.fbn-devops-dev-asenv.appserviceenvironment.net/api/v1/account/get-account-details] [CoreBankingFeign#GetAccountName(Map)]: [{
  "RequestId": null,
  "ResponseCode": "700",
  "ResponseMessage": "Client Not permitted"
}]


curl -X POST --header 'Content-Type: application/json' --header 'Accept: application/json' --header 'AppId: PapssSlGm' --header 'AppKey: 5c85cd00709d7a414f0156f451cccb51' -d '{ \ 
   "AccountNumber": "101301000109382", 
   "RequestId": "787r6efudyidi",
   "CountryId": "06" 
 }' 'https://fibridge-fin11-dev.fbn-devops-dev-asenv.appserviceenvironment.net/api/v1/account/get-account-details'

 curl: (7) Failed to connect to fibridge-fin11-dev.fbn-devops-dev-asenv.appserviceenvironment.net port 443: Connection timed out




 POST https://fibridge-fin11-dev.fbn-devops-dev-asenv.appserviceenvironment.net/api/v1/ac$
2026-02-24 15:53:23,776 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] BRANCH: 03
2026-02-24 15:53:23,776 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] charset: UTF-8
2026-02-24 15:53:23,776 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] Content-Length: 103
2026-02-24 15:53:23,776 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] Content-Type: application/json
2026-02-24 15:53:23,777 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] SOURCE: 5c85cd00709d7a414f0156f451cccb51
2026-02-24 15:53:23,777 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] USERID: PapssSlGm
2026-02-24 15:53:23,777 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName]
2026-02-24 15:53:23,777 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] {"CountryId":"03","RequestId":"ec85d1fb-ca9f-4af3-aa69-aff74fb47b0a","AccountNumber":"1013010$
2026-02-24 15:53:23,777 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] ---> END HTTP (103-byte body)
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] <--- HTTP/1.1 400 Bad Request (1867ms)
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] cache-control: no-cache,no-store, no-cache, must-revalidate, post-check=0, pre-check=0
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] content-length: 97
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] content-type: text/plain; charset=utf-8
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] cross-origin-embedder-policy: require-corp
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] cross-origin-opener-policy: same-origin
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] date: Tue, 24 Feb 2026 14:53:25 GMT
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] expires: -1
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] permissions-policy: strict -origin-when-cross-origin
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] pragma: no-cache
2026-02-24 15:53:25,645 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] referrer-policy: same-origin
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] set-cookie: ARRAffinity=0aa69915266871205a67096b40953eafb333722c9d662666b4ee1cbd3af96c28;Path$
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] set-cookie: ARRAffinitySameSite=0aa69915266871205a67096b40953eafb333722c9d662666b4ee1cbd3af96$
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] strict-transport-security: max-age=31536000; includeSubDomains
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] x-content-type-options: nosniff
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] x-frame-options: DENY
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] x-permitted-cross-domain-policies: none
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] x-xss-protection: 1; mode=block
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName]
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] {
  "RequestId": null,
  "ResponseCode": "700",
  "ResponseMessage": "Client Not permitted"
}
2026-02-24 15:53:25,646 DEBUG feign.slf4j.Slf4jLogger72 [http-nio-7223-exec-2] [CoreBankingFeign#GetAccountName] <--- END HTTP (97-byte body)
2026-02-24 15:53:25,647 ERROR com.oakwood.advansio.papss.service.impl.CoreBankingImpl96 [http-nio-7223-exec-2] [400 Bad Request] during [POST] to [https://fibridge-fin11-dev.fbn-devops-dev-asenv.appservicee$
  "RequestId": null,
  "ResponseCode": "700",
  "ResponseMessage": "Client Not permitted"
}]
