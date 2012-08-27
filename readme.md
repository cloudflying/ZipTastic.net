Ziptastic.net Class Library
===================

Welcome to the ZipTastic .net Class Library.

Simple usage:

    Imports ZipTastic
    Dim myZips As New ZipTastic.getZipInfo
    Dim myZipCodeData As iZip = myZips.getZipData("06281", "US")
    MsgBox(myZipCodeData.City & " : " & myZipCodeData._State)

For more information see our article on Stupid Code Tricks
http://www.stupidcodetricks.com/net-framework/intermediate/ziptastic-net-class-library/

License 
===================

This work is licensed under a Creative Commons Attribution 3.0 Unported License.
http://creativecommons.org/licenses/by/3.0/