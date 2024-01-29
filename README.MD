# EXPLICAÇÕES

Foi criado o microserviço Consolidado.API, este é responsável por expor endpoints como:

	1. GET /Consolidado: mostra os lançamentos consolidados por data, contendo o saldo anterior, 
	os créditos, os débitos e o saldo do dia. Para executar deve-se informar um intervalo de datas.

O serviço tem uma classe WorkerService que fica rodando em background quando o serviço está UP e caso
uma mensagem seja adicionada na fila do RabbitMQ essa classe consome a fila e processa a mensagem.

# Instalação:

	- Clonar o repositório com o comando "git clone https://github.com/jeansantos77/Consolidado.git"
	
	- Após o container estiver startado deve-se entrar no banco via Sql Management Studio, informar como 
	server name "localhost, 1450", usuário "sa" e senha "Senha@2023", assim vai conseguir conectar no banco 
	de dados do container 
	
	- Clicar no menu na opção "Query", isso vai abrir o SqlQuery para que seja possível executar comandos
	
	- Copiar o script "InitialCreate.sql" contido em "Consolidado\Consolidado.API.Infra.Data\Migrations\Script"
	
	- Colar e executar na janela do SqlQuery, isso vai criar o banco de dados e a tabela do banco dbConsolidados
	
	- Após essa execução pode-se entrar na pasta "Consolidado\Consolidado.API" via CMD e digitar "dotnet run"
	
	- Abrir o browse e digitar "https://localhost:5011/swagger" que a aplicação Lancamento.API estará rodando.