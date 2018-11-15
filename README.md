# Dark-Sky-Comparisons
Console Application for Comparing Weather Data Between Two Locations

## Description

This is a console application that allows simple comparison of current weather statistics between two locations.
The user is asked to enter two Zip Codes for the two locations to be compared.  These Zip Codes are then located
within an included CSV file in order to determine their associated Latitude and Longitude values.  These values are
then passed to the Dark Sky API using an HTTP call.  The data are then passed to two instances of the DarkSkyData class,
allowing each stat to be compared using the Console Application's menu.  Included in these options is the ability to
save the comparisons as a text file, and then immediately print the contents of the saved file to the screen.

## Instructions

The most straightforward way to use this applications is to download the repository, open the SLN file in Visual Studio,
build the application, then click Run.
