# TexasPPDMLoader
Loading of Texas Railroad oil &amp; gas data into PPDM

All kinds of Texas oil&gas data can be downloaded from:
//https://www.rrc.texas.gov/resource-center/research/data-sets-available-for-download/#digital-map-data-table

This tool requires shapefile data from "Well layers by county". You can download the wanted county based on the 3 digit county code.

The release have a self contained executable that you can download. This does not have a certificate so you will get a warning when using it.

It will require that you enter the path for where the files are and the 3 digit county code. It also requires the connection string to your SQL server database. If you omit this then it will create a csv file.


