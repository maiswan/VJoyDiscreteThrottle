#Requires AutoHotkey v2.0
BaseUrl := "http://localhost:12023/api/v2/"
Whr := ComObject('WinHttp.WinHttpRequest.5.1')

Post(RelativeUrl)
{
	Whr.Open("POST", BaseUrl . RelativeUrl, true)
	Whr.Send()
}

+1::
{
	Post("notch/min")
}

+q::
{
	Post("notch/decrement")
}

+a::
{
	Post("notch/neutral/toward")
}

+z::
{
	Post("notch/increment")
}