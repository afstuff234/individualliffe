﻿Imports System.Collections
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports CustodianLife.Data
Imports CustodianLife.Model
Imports CustodianLife.Repositories
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Partial Class I_LIFE_ReceiptPrint
    Inherits System.Web.UI.Page
    Dim strKey As String
    Dim lifReceipt As ReportDocument
    Dim reportpath As String = SiteGlobal.ReportPath
    Dim reportname As String = "LifeReceipt.rpt"
    Dim rcReportData As ReceiptsRepository

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            lifReceipt = New ReportDocument 'Instance of rpt file
            rcReportData = New ReceiptsRepository()
            Session("rcReportData") = rcReportData
            Session("lifReceipt") = lifReceipt

            strKey = CType(Session("rcPrintNo"), String)

        Else 'post back
            strKey = CType(Session("rcPrintNo"), String)
            rcReportData = CType(Session("rcReportData"), ReceiptsRepository)
            lifReceipt = CType(Session("lifReceipt"), ReportDocument)
            executeReport()
        End If
    End Sub

    Protected Sub butView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butView.Click
        executeReport()
    End Sub
    Private Sub executeReport()
        ' create 3 records of the receipt details

        '1. get the receipt record in a dataset and make 2 more copies
        strKey = CType(Session("rcPrintNo"), String)
        Dim ds As DataSet = rcReportData.GetReceiptDetails(Trim(strKey))
        Dim ds1 As DataSet = ds.Copy
        Dim ds2 As DataSet = ds.Copy


        '2. merge the datasets above into one
        ds1.Merge(ds2)
        ds.Merge(ds1)

        '3 extract the table
        Dim dt As DataTable = ds.Tables(0)

        '4 add extra column with which to use for grouping in the report. e.g. a serial number and fill 
        Dim nwData As DataTable = AddAutoIncrementField(dt)
        reportpath = SiteGlobal.ReportPath
        reportname = "LifeReceipt.rpt"
        reportpath = reportpath & reportname
        lifReceipt.Load(reportpath)

        lifReceipt.SetDataSource(nwData)

        '6 view the report
        With CrystalReportViewer1
            .ReportSource = lifReceipt
            .HasPrintButton = True
            .HasRefreshButton = True
            .HasSearchButton = True
            .HasToggleGroupTreeButton = True
            .HasZoomFactorList = True
            .HasPageNavigationButtons = True
            .HasGotoPageButton = True
            .DisplayPage = True
            .DisplayToolbar = True
            .DisplayGroupTree = False
            .DataBind()
            .RefreshReport()

        End With



    End Sub

    ''' <summary>
    ''' This adds an identity field to a data filled table
    ''' </summary>
    ''' <param name="ATable">the data filled table </param>
    ''' <returns>data table with an identity field</returns>
    ''' <remarks>Used this identitied table for report purposes i.e. grouping</remarks>
    Public Function AddAutoIncrementField(ByVal ATable As DataTable) As DataTable
        If Not ATable.Columns.Contains("sSNo") Then
            Dim BColumn As DataColumn = New DataColumn("sCopyType", Type.GetType("System.String"))
            Dim AColumn As DataColumn = New DataColumn("sSNo", Type.GetType("System.Int32"))
            AColumn.AutoIncrement = True
            AColumn.AutoIncrementSeed = 1
            AColumn.AutoIncrementStep = 1
            ATable.Columns.Add(BColumn)
            ATable.Columns.Add(AColumn)
            Dim ARow As DataRow
            Dim intCtr As Integer = 0
            For Each ARow In ATable.Rows
                intCtr += 1
                ARow.Item("sCopyType") = CopyType(intCtr)
                ARow.Item("sSNo") = intCtr
            Next
            AColumn.ReadOnly = True
        End If

        Return ATable
    End Function


    Protected Function CopyType(ByVal typecnt As Int16) As String
        Dim cpyType As String = ""
        If typecnt = 1 Then
            cpyType = "Client Copy"
        ElseIf typecnt = 2 Then
            cpyType = "Account Copy"
        Else
            cpyType = "File Copy"

        End If
        Return cpyType
    End Function

    Protected Sub butClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles butClose.Click
        Response.Redirect("PRG_LI_INDV_RECEIPT.aspx")
    End Sub
End Class
