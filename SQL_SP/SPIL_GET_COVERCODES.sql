USE [ABS_LIFE]
GO
/****** Object:  StoredProcedure [dbo].[SPIL_GET_COVERCODES]    Script Date: 08/05/2015 18:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SPIL_GET_COVERCODES]
AS
BEGIN
	SELECT WK.TBIL_COV_CD, WK.TBIL_COV_DESC
     FROM TBIL_COVER_DET AS WK ORDER BY TBIL_COV_DESC ASC
 END
