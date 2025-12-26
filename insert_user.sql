-- Hash de "Test123!" usando SHA256 en Base64
-- Test123! = /V2Eb/D0vBDe4M2x1j6Bq7X3vYvG5+s1eHhQ2m5wJ9I=

INSERT INTO Usuarios (Nombre_Completo, Email, Password_Hash, Rol, Fecha_Creacion)
VALUES ('Test User', 'test@clinica.com', '/V2Eb/D0vBDe4M2x1j6Bq7X3vYvG5+s1eHhQ2m5wJ9I=', 'cliente', GETDATE());

SELECT 'Usuario creado exitosamente' AS Mensaje;
