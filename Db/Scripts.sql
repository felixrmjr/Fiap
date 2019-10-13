CREATE TABLE [dbo].[Parceria] (
    [Codigo]           INT            IDENTITY (1, 1) NOT NULL,
    [Titulo]           VARCHAR (255)  NOT NULL,
    [Descricao]        TEXT           NOT NULL,
    [UrlPagina]        VARCHAR (1000) NULL,
    [Empresa]          VARCHAR (40)   NOT NULL,
    [DataInicio]       DATE           NOT NULL,
    [DataTermino]      DATE           NOT NULL,
    [QtdLikes]         INT            NOT NULL,
    [DataHoraCadastro] DATETIME       DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([Codigo] ASC)
)
GO
CREATE TABLE [dbo].[ParceiraLike] 
(
    [Codigo]           	INT      IDENTITY (1, 1) 		NOT NULL,
    [CodigoParceira]   	INT      						NOT NULL,
    [DataHoraCadastro] 	DATETIME DEFAULT (getdate()) 	NOT NULL,
    CONSTRAINT [PK_ParceiraLike] PRIMARY KEY CLUSTERED ([Codigo] ASC),
    CONSTRAINT [FK_ParceiraLike_CodigoParceira] FOREIGN KEY ([CodigoParceira]) REFERENCES [dbo].[Parceria] ([Codigo])
)
GO
CREATE VIEW [dbo].[vParceria]
	AS SELECT [Codigo]          
	        , [Titulo]          
	        , [Descricao]       
	        , [UrlPagina]       
	        , [Empresa]         
	        , [DataInicio]      
	        , [DataTermino]     
	        , [QtdLikes]        
	        , [DataHoraCadastro]
	FROM [Parceria]
GO
CREATE VIEW [dbo].[vParceiraLike]
	AS SELECT [Codigo]          
			, [CodigoParceira]  
			, [DataHoraCadastro]
	FROM [ParceiraLike]
GO
CREATE PROCEDURE spParceria
	@operacao 	      INT,
	@titulo 	      VARCHAR(255),
	@descricao        TEXT,
	@urlPagina        VARCHAR(1000) = NULL,
	@empresa          VARCHAR(40),
	@dataInicio       DATE,
	@dataTermino      DATE,
	@qtdLikes         INT,
	@codigo 		  INT           = NULL
AS
BEGIN

	BEGIN TRY 
	
		IF @operacao = 1
		BEGIN 
			INSERT INTO [dbo].[Parceria]
				(Titulo, Descricao, UrlPagina, Empresa, DataInicio, DataTermino, QtdLikes, DataHoraCadastro)
			VALUES
				(@titulo, @descricao, @urlPagina, @empresa, @dataInicio, @dataTermino, 0, GETDATE())
				   
		END
		ELSE IF @operacao = 2
		BEGIN 
			UPDATE Parceria
			   SET Titulo 			= @titulo
				 , Descricao 		= @descricao
				 , UrlPagina 		= @urlPagina
				 , Empresa	  		= @empresa
				 , DataInicio 		= @dataInicio
				 , DataTermino 		= @dataTermino
				 , QtdLikes 		= @qtdLikes				 
			WHERE Codigo = @codigo
		
		END
		ELSE IF @operacao = 3
		BEGIN 
			DELETE FROM Parceria WHERE Codigo = @codigo
		END
		ELSE
			THROW 51000, 'Operação não informada de forma correta. (1-Insert, 2-Update, 3-Delete)', 1; 
		
	END TRY  
	BEGIN CATCH  
		SELECT ERROR_NUMBER() AS ErrorNumber  
			 , ERROR_SEVERITY() AS ErrorSeverity  
			 , ERROR_STATE() AS ErrorState  
			 , ERROR_PROCEDURE() AS ErrorProcedure  
			 , ERROR_LINE() AS ErrorLine  
			 , ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;  
	
END
GO
CREATE PROCEDURE spParceriaLike
	@codigoParceira   INT
AS
BEGIN

	BEGIN TRY
	
		INSERT INTO [dbo].[ParceiraLike] (CodigoParceira, DataHoraCadastro)
		VALUES (@codigoParceira, GETDATE())

	END TRY  
	BEGIN CATCH  
		SELECT ERROR_NUMBER() AS ErrorNumber  
			 , ERROR_SEVERITY() AS ErrorSeverity  
			 , ERROR_STATE() AS ErrorState  
			 , ERROR_PROCEDURE() AS ErrorProcedure  
			 , ERROR_LINE() AS ErrorLine  
			 , ERROR_MESSAGE() AS ErrorMessage;  
	END CATCH;  
	
END	
GO
CREATE TRIGGER tgiParceriaLike ON [dbo].[ParceiraLike] AFTER INSERT
AS
BEGIN
    DECLARE @codigoParceira int
    SELECT  @codigoParceira =  codigoParceira  FROM INSERTED
  
	UPDATE [dbo].[Parceria]
	SET QtdLikes = tb1.Total
	FROM (SELECT (QtdLikes + 1) AS Total FROM [dbo].[Parceria] WHERE Codigo = @codigoParceira) AS tb1
	WHERE Codigo = @codigoParceira;
END
GO