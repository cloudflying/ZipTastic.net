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
        If zipData._State.Length > 0 Then
            zipData._stateAbbr = states(zipData._State)
        End If
        Return zipData
    End Function

    Private Function BuildStates() As Hashtable
        Dim states As Hashtable = New Hashtable
        states.Add("AL", "Alabama")
        states.Add("AK", "Alaska")
        states.Add("AZ", "Arizona")
        states.Add("AR", "Arkansas")
        states.Add("CA", "California")
        states.Add("CO", "Colorado")
        states.Add("CT", "Connecticut")
        states.Add("DE", "Delaware")
        states.Add("DC", "Dist of Columbia")
        states.Add("FL", "Florida")
        states.Add("GA", "Georgia")
        states.Add("HI", "Hawaii")
        states.Add("ID", "Idaho")
        states.Add("IL", "Illinois")
        states.Add("IN", "Indiana")
        states.Add("IA", "Iowa")
        states.Add("KS", "Kansas")
        states.Add("KY", "Kentucky")
        states.Add("LA", "Louisiana")
        states.Add("ME", "Maine")
        states.Add("MD", "Maryland")
        states.Add("MA", "Massachusetts")
        states.Add("MI", "Michigan")
        states.Add("MN", "Minnesota")
        states.Add("MS", "Mississippi")
        states.Add("MO", "Missouri")
        states.Add("MT", "Montana")
        states.Add("NE", "Nebraska")
        states.Add("NV", "Nevada")
        states.Add("NH", "New Hampshire")
        states.Add("NJ", "New Jersey")
        states.Add("NM", "New Mexico")
        states.Add("NY", "New York")
        states.Add("NC", "North Carolina")
        states.Add("ND", "North Dakota")
        states.Add("OH", "Ohio")
        states.Add("OK", "Oklahoma")
        states.Add("OR", "Oregon")
        states.Add("PA", "Pennsylvania")
        states.Add("RI", "Rhode Island")
        states.Add("SC", "South Carolina")
        states.Add("SD", "South Dakota")
        states.Add("TN", "Tennessee")
        states.Add("TX", "Texas")
        states.Add("UT", "Utah")
        states.Add("VT", "Vermont")
        states.Add("VA", "Virginia")
        states.Add("WA", "Washington")
        states.Add("WV", "West Virginia")
        states.Add("WI", "Wisconsin")
        states.Add("WY", "Wyoming")
        Return states
    End Function
End Class
