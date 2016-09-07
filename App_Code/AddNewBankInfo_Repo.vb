Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Public Class ClaimBeneficiaryRepository
    Shared _rtnMessage As String

    Public Function AddNewBankInfo_Repo(ByVal fileNumber As String, ByVal proposalNumber As String, ByVal polNumber As String, ByVal claimNo As String, _
                                     ByVal bankAccountNo As String, ByVal bankAccountName As String, ByVal bankName As String, ByVal bankSortCode As String, _
                                     ByVal bankAccountType As String, ByVal settlementStatus As String, ByVal benefName As String, ByVal benefAge As String, _
                                     ByVal benefShare_pcnt As String, ByVal benefShare As String, ByVal benefPayout As String, ByVal GuardianName As String, _
                                     ByVal flag As String, ByVal dDate As DateTime, ByVal userId As String, ByVal connString As String) As String

        Dim mystrConn As String = connString
        Dim benefShares_pcnt As Decimal = Convert.ToDecimal(benefShare_pcnt)
        Dim benefShares As Decimal = Convert.ToDecimal(benefShare)
        Dim benefPayouts As Decimal = Convert.ToDecimal(benefPayout)

        Dim conn As OleDbConnection
        conn = New OleDbConnection(mystrConn)
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = conn
        cmd.CommandText = "SPIL_INS_BENEFICIARY_BANK_DETAILS"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_FILE_NO", fileNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_PROPOSAL_NO", proposalNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_POLY_NO", polNumber)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_CLM_NO", claimNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_ACCOUNT_NO", bankAccountNo)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_ACCOUNT_NAME", bankAccountName)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_NAME", bankName)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_SORT_CODE", bankSortCode)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_ACCOUNT_TYPE", bankAccountType)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_SETTELEMENT_STATUS", settlementStatus)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENEFICIARY_NAME", benefName)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENEFICIARY_AGE", benefAge)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENEFICIARY_SHARE_PCNT", benefShares_pcnt)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENEFICIARY_SHARE", benefShares)
        cmd.Parameters.AddWithValue("@@TBIL_CLM_BENE_BANK_PAYOUT", benefPayouts)
        cmd.Parameters.AddWithValue("@TBIL_CLM_BENE_BANK_GUARDIAN_NAME", GuardianName)

        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_FLAG", flag)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_KEYDTE", dDate)
        cmd.Parameters.AddWithValue("@TBIL_CLM_RPTD_OPERID", userId)

        Try
            conn.Open()
            Dim adapter As OleDbDataAdapter = New OleDbDataAdapter()
            adapter.SelectCommand = cmd

            Dim ds As DataSet = New DataSet()
            adapter.Fill(ds)
            conn.Close()

            Dim dt As DataTable = ds.Tables(0)
            For Each dr As DataRow In dt.Rows
                Dim msg = dr("Msg").ToString()
                If msg = 1 Then
                    '_rtnMessage = "Entry Successful, with CLAIM NUMBER: " + claimNo + " generated!"
                    _rtnMessage = "Entry Successful!"
                Else
                    _rtnMessage = "Update of existing records Successful!"
                End If
            Next
        Catch ex As Exception
            _rtnMessage = "Error! " + ex.Message.ToString()
        End Try


        Return _rtnMessage

    End Function
End Class
