USE [ABS_LIFE_STAGING]
GO

/****** Object:  StoredProcedure [dbo].[SPIL_SELECT_ANN_PROPOSAL]    Script Date: 08/18/2015 19:24:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SPIL_SELECT_ANN_PROPOSAL]
	(@PARAM_PROP_NUM	VARCHAR(40) = 'QI/2003/1201/P/P001/I/0000001'
	,@PARAM_FILE_NUM	VARCHAR(40) = '4274721'
	,@PARAM_MODULE		VARCHAR(3) = 'A'
	)

AS

SELECT TOP 1 PT.TBIL_ANN_POLY_FILE_NO, PT.TBIL_ANN_POLY_PROPSAL_NO, PT.TBIL_ANN_POLY_POLICY_NO
,PT.TBIL_ANN_POLY_UNDW_YR, PT.TBIL_ANN_POLY_BRANCH_CD, PT.TBIL_ANN_POLY_PRDCT_CD
,PT.TBIL_ANN_POLY_PROPSL_ACCPT_STATUS
,PRM.TBIL_ANN_POL_PRM_FILE_NO, PRM.TBIL_ANN_POL_PRM_PROP_NO, PRM.TBIL_ANN_POL_PRM_POLY_NO, PRM.TBIL_ANN_POL_PRM_FROM, PRM.TBIL_ANN_POL_PRM_TO
--,PRM_DTL.TBIL_POL_PRM_DTL_FILE_NO, PRM_DTL.TBIL_POL_PRM_DTL_PROP_NO, PRM_DTL.TBIL_POL_PRM_DTL_POLY_NO
,INSRD.TBIL_INSRD_SURNAME, INSRD.TBIL_INSRD_FIRSTNAME
,PROD_DTL.TBIL_PRDCT_DTL_CAT, PROD_DTL.TBIL_PRDCT_DTL_DESC
,RCT.TBIL_RCT_DATE, RCT.TBIL_RCT_NUM, RCT.TBIL_RCT_AMT

FROM TBIL_ANN_POLICY_DET AS PT

LEFT JOIN TBIL_ANN_POLICY_PREM_INFO AS PRM
  ON (PRM.TBIL_ANN_POL_PRM_FILE_NO = PT.TBIL_ANN_POLY_FILE_NO AND PRM.TBIL_ANN_POL_PRM_PROP_NO = PT.TBIL_ANN_POLY_PROPSAL_NO AND PRM.TBIL_ANN_POL_PRM_MDLE IN ('ANN','A'))

--LEFT JOIN TBIL_POLICY_PREM_DETAILS AS PRM_DTL
  --ON (PRM_DTL.TBIL_POL_PRM_DTL_FILE_NO = PT.TBIL_ANN_POLY_FILE_NO AND PRM_DTL.TBIL_POL_PRM_DTL_PROP_NO = PT.TBIL_ANN_POLY_PROPSAL_NO AND PRM_DTL.TBIL_POL_PRM_DTL_MDLE IN('IND','I'))
 -- ON (PRM_DTL.TBIL_POL_PRM_DTL_FILE_NO = PT.TBIL_ANN_POLY_FILE_NO AND PRM_DTL.TBIL_POL_PRM_DTL_PROP_NO = PT.TBIL_ANN_POLY_PROPSAL_NO)

LEFT JOIN TBIL_INS_DETAIL AS INSRD
  ON (INSRD.TBIL_INSRD_CODE = PT.TBIL_ANN_POLY_ASSRD_CD AND INSRD.TBIL_INSRD_MDLE IN('ANN','A'))

LEFT JOIN TBIL_PRODUCT_DETL AS PROD_DTL
  --ON (PROD_DTL.TBIL_PRDCT_DTL_CODE = PT.TBIL_ANN_POLY_PRDCT_CD AND PROD_DTL.TBIL_PRDCT_DTL_MDLE IN('ANN','A'))
    ON (PROD_DTL.TBIL_PRDCT_DTL_CODE = PT.TBIL_ANN_POLY_PRDCT_CD AND PROD_DTL.TBIL_PRDCT_DTL_CAT IN('ANN','A'))
LEFT JOIN TBIL_POLICY_RECEIPT AS RCT
  ON (RCT.TBIL_RCT_FILE_NUM = PT.TBIL_ANN_POLY_FILE_NO AND RCT.TBIL_RCT_PROPOSAL_NUM = PT.TBIL_ANN_POLY_PROPSAL_NO AND RCT.TBIL_RCT_POLICY_NUM = PT.TBIL_ANN_POLY_POLICY_NO AND RCT.TBIL_RCT_MDLE IN('ANN','A'))
--ON (RCT.TBIL_RCT_FILE_NUM = PT.TBIL_ANN_POLY_FILE_NO AND RCT.TBIL_RCT_PROPOSAL_NUM = PT.TBIL_ANN_POLY_PROPSAL_NO AND RCT.TBIL_RCT_POLICY_NUM = PT.TBIL_ANN_POLY_POLICY_NO)
WHERE PT.TBIL_ANN_POLY_PROPSAL_NO = @PARAM_PROP_NUM
  AND PT.TBIL_ANN_POLY_FILE_NO = @PARAM_FILE_NUM
  AND PT.TBIL_ANN_POLY_MDLE IN(@PARAM_MODULE)
--  AND PT.TBIL_POLY_PROPSL_ACCPT_STATUS IN('P')




GO

