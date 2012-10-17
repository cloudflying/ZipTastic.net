Imports Microsoft.VisualBasic
Imports System.Net
Imports System.IO
Imports Newtonsoft.Json

Public Class getZipInfo
    '
    ' #### Ziptastic .net class library
    ' #### Created by Justin Porter
    ' #### Attribution 3.0 Unported (CC BY 3.0) 
    ' #### License: http://creativecommons.org/licenses/by/3.0/
    ' #### Please link to this article: http://www.stupidcodetricks.com/net-framework/intermediate/ziptastic-net-class-library/
    ' #### view us on github: https://github.com/cloudflying/ZipTastic.net
    ' #### Enjoy!

    ' #### version 1.1 updates
    ' #### added zipCode and _stateAbbr to the iZip property sheet
    ' #### added optional getAbbr : determines state's two letter abbreviation (Only supports US).
    ' #### added comments - JP


    ' in case of future upgrades
    Public API_Key As String = String.Empty

    ''' <summary>
    ''' Grabs ZipTastic Zip Code information and serializes it
    ''' </summary>
    ''' <param name="zipCode">Provide the Zip Code of the place you are searching for</param>
    ''' <param name="country">OPTIONAL (Default: US) Specify the two letter country code</param>
    ''' <param name="getAbbr">OPTIONAL (Default: False) Allows you to look up the two letter state code if US is the country</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getZipData(zipCode As String, Optional country As String = "US", Optional getAbbr As Boolean = False) As iZip
        Try
            Dim str As String = sendReq(zipCode, country)
            Dim zipData As iZip = JsonConvert.DeserializeObject(Of iZip)(str)
            If getAbbr = True And LCase(country) = "us" Then zipData = DetermineStateAbbr(zipData)
            zipData.zipCode = zipCode
            Return zipData
        Catch ex As Exception
            Throw New ApplicationException("An error occurred retrieving your city and state information")
        End Try

    End Function




    Private Function sendReq(zipCode As String, country As String) As String
        Dim uri As String = "http://zip.elevenbasetwo.com/v2/" & country & "/" & zipCode
        Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
        request.Method = WebRequestMethods.Http.Get
        request.ContentType = "text/html"
        Dim oResponse As HttpWebResponse = request.GetResponse()
        Dim reader As New StreamReader(oResponse.GetResponseStream())
        Dim tmp As String = reader.ReadToEnd()
        oResponse.Close()
        Return tmp
    End Function


    Private Function DetermineStateAbbr(zipData As iZip) As iZip
        Dim states As Hashtable = BuildStates()
        If Len(zipData._State) > 0 Then
            zipData._stateAbbr = states(zipData._State)
        End If
        Return zipData
    End Function

    Private Function BuildStates() As Hashtable
        Dim states As Hashtable = New Hashtable
        states.Add("Alabama", "AL")
        states.Add("Alaska", "AK")
        states.Add("Arizona", "AZ")
        states.Add("Arkansas", "AR")
        states.Add("California", "CA")
        states.Add("Colorado", "CO")
        states.Add("Connecticut", "CT")
        states.Add("Delaware", "DE")
        states.Add("Dist of Columbia", "DC")
        states.Add("Florida", "FL")
        states.Add("Georgia", "GA")
        states.Add("Hawaii", "HI")
        states.Add("Idaho", "ID")
        states.Add("Illinois", "IL")
        states.Add("Indiana", "IN")
        states.Add("Iowa", "IA")
        states.Add("Kansas", "KS")
        states.Add("Kentucky", "KY")
        states.Add("Louisiana", "LA")
        states.Add("Maine", "ME")
        states.Add("Maryland", "MD")
        states.Add("Massachusetts", "MA")
        states.Add("Michigan", "MI")
        states.Add("Minnesota", "MN")
        states.Add("Mississippi", "MS")
        states.Add("Missouri", "MO")
        states.Add("Montana", "MT")
        states.Add("Nebraska", "NE")
        states.Add("Nevada", "NV")
        states.Add("New Hampshire", "NH")
        states.Add("New Jersey", "NJ")
        states.Add("New Mexico", "NM")
        states.Add("New York", "NY")
        states.Add("North Carolina", "NC")
        states.Add("North Dakota", "ND")
        states.Add("Ohio", "OH")
        states.Add("Oklahoma", "OK")
        states.Add("Oregon", "OR")
        states.Add("Pennsylvania", "PA")
        states.Add("Rhode Island", "RI")
        states.Add("South Carolina", "SC")
        states.Add("South Dakota", "SD")
        states.Add("Tennessee", "TN")
        states.Add("Texas", "TX")
        states.Add("Utah", "UT")
        states.Add("Vermont", "VT")
        states.Add("Virginia", "VA")
        states.Add("Washington", "WA")
        states.Add("West Virginia", "WV")
        states.Add("Wisconsin", "WI")
        states.Add("Wyoming", "WY")
        Return states
    End Function
End Class
