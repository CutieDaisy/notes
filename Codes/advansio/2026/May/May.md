Todos for the week of 2026-05-25:
* Add Logs for all projects
* USSD - Ussd for Internet Banking
* Salam Kalpe : 
    * Change core banking api from test to production
    * Add Reset Customer Pin and send sms and email - DONE
* Collections : 
    * Bulk payment, 
    * Sign up and login - Implement 3rd party authentication, and should be able to send email notifications to customers

* Tunes Transfer Error 
    * Error Msg: java.lang.NumberFormatException: Expected an int but was 2152200337 at line 1 column 218 path $.id
    * Investigate and fix the error that is occurring during Tunes transfer.  - DONE



Today's Checked Tasks (2026-05-29):
* Add Logs for all projects

* USSD - Ussd for Internet Banking

* Tunes Transfer Error
    * Investigation revealed that the Id being returned from the Tunes API is a long value, but the code is trying to parse it as an int, which is causing the NumberFormatException.
    * The code needs to be updated to parse the Id as a long value instead of an int.

* Had a meeting with Access Bank Liberia : 
    * to change their ldap server and port from ldap://172.18.1.3:389 to ldap://172.18.1.2:636 to enable secure connection to their ldap server.
    * This change was not successful. 
    * Meeting rescheduled for tomorrow to troubleshoot the issue and find a solution.

* Create a Proxy using yarp for Cairo Bank (Papss)

* Irene's Tasks:
    * Create a project to monitor contracts and send notifications when they are about to expire.

* GP Payment



Agent Banking Redesign
USSD
AF8
TPS
Go-Papss
Bloom Switch
Collections
Salam Kalpeh
E-Alert


