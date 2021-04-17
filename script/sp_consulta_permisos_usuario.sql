ALTER PROCEDURE sp_consulta_permisos
(
	@usuario_login VARCHAR(30)
)
AS
BEGIN
	DECLARE @cols VARCHAR(500), @sql VARCHAR(max)
	select @cols = '';
	SELECT @cols += STUFF((SELECT ',' + QUOTENAME(PERMISO_NOMBRE)
						   FROM PERMISOS
						   FOR XML PATH('')), 1, 1, '');


	SET @sql = N'SELECT Usuario_Login,' + @cols + '
	FROM 
	(
		SELECT P.PERMISO_NOMBRE, CAST(ISNULL(PR.ACTIVO,0) AS TINYINT) AS ACTIVO, U.USUARIO_LOGIN
		FROM USUARIO U 
		LEFT JOIN PERMISOS_ROL PR ON (U.ROL_ID = PR.ROL_ID)
		LEFT JOIN PERMISOS P ON (PR.PERMISO_ID = P.PERMISO_ID)
		WHERE U.USUARIO_LOGIN = ''' + @usuario_login + '''
	) permiso
	pivot 
	(
		COUNT(ACTIVO)
		for PERMISO_NOMBRE in (' + @cols + ')
	) pivotPermiso;';

	execute(@sql)
END

