# EXPLICA��ES

Foi criado o microservi�o Consolidado.API, este � respons�vel por expor endpoints como:

	1. GET /Consolidado: mostra os lan�amentos consolidados por data, contendo o saldo anterior, 
	os cr�ditos, os d�bitos e o saldo do dia. Para executar deve-se informar um intervalo de datas.

O servi�o tem uma classe WorkerService que fica rodando em background quando o servi�o est� UP, caso
uma mensagem seja adicionada na fila do RabbitMQ essa classe consome a fila e processa a mensagem.

No caso de ocorrer um erro nesse processo o servi�o tem um tratamento de Retry onde ele tentar� executar
o processo diversas vezes conforme estiver configurado no appsettings. Est� configurado para repetir 5 vezes

# Evid�ncias
	- Na pasta Consolidado contem uma imagem "Evidencias.png" mostrando a execu��o do endpoint consolidado. 

# Instala��o:

	- Clonar o reposit�rio com o comando "git clone https://github.com/jeansantos77/Consolidado.git"
	
	- Ap�s o container estiver startado deve-se entrar no banco via Sql Management Studio, informar como 
	server name "localhost, 1450", usu�rio "sa" e senha "Senha@2023", assim vai conseguir conectar no banco 
	de dados do container 
	
	- Clicar no menu na op��o "Query", isso vai abrir o SqlQuery para que seja poss�vel executar comandos
	
	- Copiar o script "InitialCreate.sql" contido em "Consolidado\Consolidado.API.Infra.Data\Migrations\Script"
	
	- Colar e executar na janela do SqlQuery, isso vai criar o banco de dados e a tabela do banco dbConsolidados
	
	- Ap�s essa execu��o pode-se entrar na pasta "Consolidado\Consolidado.API" via CMD e digitar "dotnet run"
	
	- Abrir o browse e digitar "https://localhost:5011/swagger" que a aplica��o Lancamento.API estar� rodando.