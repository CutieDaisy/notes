* Unable to send Emails
Error : 
451 5.7.3 STARTTLS is required to send mail [PA7P264CA0250.FRAP264.PROD.OUTLOOK.COM 2026-03-05T11:41:09.483Z 08DE7A509B86EADD]

org.eclipse.angus.mail.smtp.SMTPSendFailedException: 451 5.7.3 STARTTLS is required to send mail [PA7P264CA0250.FRAP264.PROD.OUTLOOK.COM 2026-03-05T11:41:09.483Z 08DE7A509B86EADD]

	at org.eclipse.angus.mail.smtp.SMTPTransport.issueSendCommand(SMTPTransport.java:2422)
	at org.eclipse.angus.mail.smtp.SMTPTransport.mailFrom(SMTPTransport.java:1839)
	at org.eclipse.angus.mail.smtp.SMTPTransport.sendMessage(SMTPTransport.java:1316)