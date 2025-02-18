# OmieSalesManagement

- Esse � um projeto com o prop�sito de gerenciar as vendas de uma empresa.

## Como configurar o ambiente
- Para configurar o ambiente � necess�rio subir o SQLServer atrav�s do docker-compose para acessar o banco de dados
- Para isso, � necess�rio usar o comando 'sudo docker-compose up -d' na pasta do projeto com WSL para subir o banco com sucesso antes de subir a aplica��o
- Tendo feito isso com sucesso, a aplica��o ir� criar e configurar o banco de maneira aut�noma atrav�s das Migrations do EF Core

## Autentica��o de uma vendedor para gerenciar o sistema
- Para autenticar � preciso criar um vendedor com senha no banco de dados
- Ap�s a cria��o, usa-se nome e senha do vendedor para pegar o Bearer Token que volta no endpoint GET authenticate-salesman
- O Bearer Token deve ser copiado e colado no bot�o Authorize no canto superior direito
- Lembrando que � necess�rio estar autenticado para usar os endpoints relacionados � gest�o de vendas

## Inser��o das vendas
- A inser��o das vendas acontece passando o nome do cliente e a lista de produtos, com nome, pre�o e valor unit�rio
- Payload de exemplo:
{
  "customer": "Sandro",
  "products": [
    {
      "name": "Macarr�o",
      "quantity": 2,
      "unitValue": 10
    },
    {
      "name": "Carne",
      "quantity": 2,
      "unitValue": 30
    }
  ]
}

## Busca das vendas para a p�gina inicial
- A busca das vendas para a p�gina inicial � feita de maneira paginada, pensando em trazer a quantidade pertinente, pensando em uma adi��o de registros no longo prazo.

## Remo��o de uma venda no sistema
- A remo��o de uma venda no sistema � efetuada atrav�s do endpoint de DELETE com o salesId que est� cadastrado no banco

## Atualiza��o de uma venda no sistema
- A atualiza��o de uma venda no sistema acontece ap�s buscar a entidade no banco de dados para permitir mudan�as no que j� est� cadastrado, adicionar um novo produto ou eliminar um produto que n�o � mais desejado..
- Payload de exemplo:
{
  "salesId": 1,
  "customer": "Denilton",
  "products": [
    {
      "name": "Macarr�o",
      "quantity": 4,
      "unitValue": 10
    },
    {
      "name": "Atum",
      "quantity": 1,
      "unitValue": 5
    }
  ]
}