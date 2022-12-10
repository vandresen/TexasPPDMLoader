# TexasPPDMLoader
Loading of Texas Railroad oil &amp; gas data into PPDM

All kinds of Texas oil&gas data can be downloaded from:
//https://www.rrc.texas.gov/resource-center/research/data-sets-available-for-download/#digital-map-data-table

This tool requires shapefile data from "Well layers by county" and "Statewide API data" (the dbase version). The program will automatically download these for you.

The release have a self contained executable that you can download. This does not have a certificate so you will get a warning when using it.

It will require that you enter the path for where the files will be downloaded and the 3 digit county code. It also requires the connection string to your SQL server database. If you omit this then it will create a csv file in the path that you entered.


