Create  PROCEDURE [dbo].[DBPager]
	--@roleName nvarchar(2000),        --�ֶ�����
	@pageSize int,			--ҳ���С
	@pageIndex int,			--��ǰҳ�ţ���1��ʼ
	@tableName nvarchar(200),--����
	@TotalNum int OUTPUT
AS

  DECLARE @strSql nvarchar(2000)
	DECLARE @str nvarchar(2000)
	set @strSql=N'WITH  CTE_Table
          AS ( SELECT TOP ( @pageSize * @pageIndex )
                        ROW_NUMBER() OVER ( ORDER BY Row_ID DESC ) AS rownum ,
                        Row_ID
               FROM  '+   @tableName+')
SELECT  *
FROM    CTE_Table
        INNER JOIN '+ @tableName+' ON '+@tableName+'.Row_ID = CTE_Table.Row_ID
WHERE   CTE_Table.rownum > ( @pageSize * ( @pageIndex - 1 ) ) '
set @TotalNum = 0
set @str=N'select @TotalNum=@TotalNum+1 from ' + @tableName
exec sp_executesql @str,N'@TotalNum int output',@TotalNum output

exec sp_executesql @strSql,N'@pageSize int,@pageIndex int',@pageSize,@pageIndex