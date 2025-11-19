# TexasPPDMLoader
Loading of Texas Railroad oil &amp; gas data into PPDM

All kinds of Texas oil&gas data can be downloaded from:
//https://www.rrc.texas.gov/resource-center/research/data-sets-available-for-download/#digital-map-data-table

This tool requires shapefile data from "Well layers by county" and "Statewide API data" (the dbase version). The program will automatically download these for you. It also download the full wellbore ASCII file.

The release have a self contained executable that you can download. This does not have a certificate so you will get a warning when using it.

Usage:
  TexasPPDMLoader [options]

Options:
  --path <path>                 Directory to store downloaded files (Default = C:       emp)
  --county <county> (REQUIRED)  3-character county code
  --connection <connection>     Optional SQL Server connection string (Default = csv file)
  -?, -h, --help                Show help and usage information
  --version                     Show version information

It is using software from Selenium to download data. Selenium is basically test software for web based user interface. We are using their chrome driver for this.

We are trying to get all wells in a county. Some wells may have bottom locations outside the county and not be available in the shapefiles. In this case we can only provide the surface location. For some wells we may not find a match in well header info shapefile so no location data will be available for these. There are also several wells we cannot get a proper well UWI. All these well UWIs will start with the letter U.

Data types supported:
* Well header
* Formations
* Perforations
* Casing data



