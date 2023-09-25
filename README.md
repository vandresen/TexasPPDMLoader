# TexasPPDMLoader
Loading of Kansas Geological Survey data into PPDM

The program will automatically download wellbore for you and load into your PPDM SQL Server database. Only SQL Server is supported.

The release have a self contained executable that you can download. This does not have a certificate so you will get a warning when using it.

It will require that you enter the path for where the files will be downloaded and the 3 digit county code. It also requires the connection string to your SQL server database. If you omit this then it will create a csv file in the path that you entered.



