### Ldap Search

ldapsearch -x -H ldap://172.18.106.3 -b "" -s base "(objectClass=*)"

ldapsearch -x -H ldap://172.18.106.3 -b "OU=ABL TEST USER,DC=ACCESSBANKTEST,DC=NET" -s base "(objectClass=*)"
ldapsearch -x -H ldap://172.18.106.3 -b "" -s base "(objectClass=*)"

ldapsearch -x -H ldap://172.18.106.3 -b "DC=ACCESSBANKTEST,DC=NET" -s base "(objectClass=*)"


## _This will fetch all objects in the LDAP directory including DC details_ ----
ldapsearch -x -H ldap://172.18.106.3 -b "" -s base "(objectClass=*)"
## -----------------------------------------------------------------------------

## _This will fetch all objects in the LDAP directory including DC_
ldapsearch -x -H ldap://10.216.1.100  -b "" -D "sa.bprpapss" -W
ldapsearch -x -H ldap://10.216.1.100  -b "DC=simba,DC=local" -D "<username>" -W
