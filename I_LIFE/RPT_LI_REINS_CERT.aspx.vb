﻿
Partial Class I_LIFE_RPT_LI_REINS_CERT
    Inherits System.Web.UI.Page
    Dim rParams As String() = {"nw", "nw", "new", "new", "new", "new"}
    Protected FirstMsg As String

    Protected Sub butOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butOK.Click
        Dim str() As String
        '  Dim reportname As String
        If (txtStartDate.Text = "") Then
            Status.Text = "Cancellation start date must not be empty"
            Exit Sub
        End If
        If (txtEndDate.Text = "") Then
            Status.Text = "Cancellation end date must not be empty"
            Exit Sub
        End If



        str = DoDate_Process(txtStartDate.Text, txtStartDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " Start date, ")
            Status.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtStartDate.Focus()
            Exit Sub
        Else
            txtStartDate.Text = str(2).ToString()
        End If

        str = DoDate_Process(txtEndDate.Text, txtEndDate)
        If (str(2) = Nothing) Then
            Dim errMsg = str(0).Insert(18, " End date ")
            Status.Text = errMsg.Replace("Javascript:alert('", "").Replace("');", "")
            txtEndDate.Focus()
            Exit Sub
        Else
            txtEndDate.Text = str(2).ToString()
        End If

        'Dim startDate = Format(Convert.ToDateTime(DoConvertToDbDateFormat(txtStartDate.Text)), "MM/dd/yyyy")
        'Dim endDate = Format(Convert.ToDateTime(DoConvertToDbDateFormat(txtEndDate.Text)), "MM/dd/yyyy")

        Dim startDate1 = Convert.ToDateTime(DoConvertToDbDateFormat(txtStartDate.Text))
        Dim endDate1 = Convert.ToDateTime(DoConvertToDbDateFormat(txtEndDate.Text))

        If startDate1 > endDate1 Then
            Status.Text = "Start Date must not be greater than end date"
            Exit Sub
        End If

        Dim startDate = Format(startDate1, "MM/dd/yyyy")
        Dim endDate = Format(endDate1, "MM/dd/yyyy")


        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        rParams(0) = "rptIndReAssuranceCertRpt"
        rParams(1) = "pStart_Date="
        rParams(2) = startDate + "&"
        rParams(3) = "pEnd_Date="
        rParams(4) = endDate + "&"
        rParams(5) = url

        Session("ReportParams") = rParams
        Response.Redirect("../PrintView.aspx")
        ' Response.Redirect("PrintView.aspx")
    End Sub
End Class
