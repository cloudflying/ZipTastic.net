Imports Newtonsoft.Json
Public Class iZip
    ' {"city": "Central Village", "state": "Connecticut", "country": "US"}
    <JsonProperty("city")> _
    Public Property City As String
    <JsonProperty("state")> _
    Public Property _State As String
    <JsonProperty("country")> _
    Public Property Country As String
    Public Property zipCode As String
    Public Property _stateAbbr As String
End Class
