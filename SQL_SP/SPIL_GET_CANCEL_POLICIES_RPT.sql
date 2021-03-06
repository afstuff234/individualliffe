USE [ABS_LIFE]
GO
/****** Object:  StoredProcedure [dbo].[SPIL_GET_LAPSE_POLICIES_RPT]    Script Date: 08/05/2015 12:58:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SPIL_GET_CANCEL_POLICIES_RPT]
(@pStart_Date	DATETIME
	,@pEnd_Date DATETIME
)
AS
BEGIN
	
SELECT WK.TBIL_POLY_POLICY_NO AS [POLICY NO], WK.[TBIL_POLY_ASSRD_CD] AS [ASSURED CODE],
		   WK.TBIL_POLY_AGCY_CODE AS [AGENT CODE],
		   RTRIM(ISNULL(TBIL_INSRD_SURNAME,'')) + ' ' +  RTRIM(ISNULL(TBIL_INSRD_FIRSTNAME,'')) AS [ASSURED NAME],
		   WS.TBIL_POL_PRM_PRDCT_CD AS [PRODUCT CODE], 
		   CONVERT(VARCHAR(10),WS.TBIL_POL_PRM_FROM, 103) AS [START DATE],
		   CONVERT(VARCHAR(10),WS.TBIL_POL_PRM_TO, 103) AS [END DATE], 
		   WU.TBIL_PRDCT_DTL_DESC AS [PRODUCT NAME],
		   WP.TBIL_AGCY_AGENT_NAME  AS [AGENT NAME],
		   CONVERT(VARCHAR(10),WK.TBIL_POLY_CANCEL_DT, 103)  AS [POLICY CANCELLED DATE],
		   (SELECT SUM(TBFN_TRANS_TOT_AMT) FROM TBFN_ALLOC_DETAIL 
		    WHERE TBFN_TRANS_POLY_NO=WK.TBIL_POLY_POLICY_NO) AS [PREMIUM PAID],
		    (SELECT SUM(TBFN_COMM_TRANS_COMM_AMT) FROM TBFN_AGENT_COMM_DETAIL 
		    WHERE TBFN_COMM_TRANS_POLY_NO =WK.TBIL_POLY_POLICY_NO) AS [BASIC COMM PAID]	    
	FROM TBIL_POLICY_DET AS WK 
	INNER JOIN tbil_ins_detail AS WV ON WK.TBIL_POLY_ASSRD_CD=WV.TBIL_INSRD_CODE
    INNER JOIN TBIL_POLICY_PREM_INFO AS WS ON WK.TBIL_POLY_POLICY_NO=WS.TBIL_POL_PRM_POLY_NO
	INNER JOIN TBIL_PRODUCT_DETL AS WU ON WS.TBIL_POL_PRM_PRDCT_CD=WU.TBIL_PRDCT_DTL_CODE
	LEFT OUTER JOIN TBIL_AGENCY_CD AS WP ON WK.TBIL_POLY_AGCY_CODE=WP.TBIL_AGCY_AGENT_CD
	WHERE TBIL_POLY_STATUS='C' AND TBIL_POLY_CANCEL_DT >=@pStart_Date AND TBIL_POLY_CANCEL_DT <=@pEnd_Date
END

