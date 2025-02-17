# OmieSalesManagement

- Esse é um projeto com o propósito de gerenciar as vendas de uma empresa.

## Como configurar o ambiente
- Para configurar o ambiente é necessário subir o SQLServer através do docker-compose para acessar o banco de dados
- Para isso, na pasta SolutionFiles no WSL é necessário usar o comando 'sudo docker-compose up -d' para subir o banco com sucesso antes de subir a aplicação
- Tendo feito isso com sucesso, a aplicação irá criar e configurar o banco de maneira autônoma através das Migrations do EF Core

## Autenticação de uma vendedor para gerenciar o sistema
- Para autenticar é preciso criar um vendedor com senha no banco de dados. 
- Após a criação, usando nome e senha, é possível pegar o Bearer Token que pode ser usado para autenticar os endpoints para gerenciar as vendas.

## Inserção das vendas
- A inserção das vendas acontece passando o nome do cliente e a lista de produtos, com nome, preço e valor unitário
- Payload de exemplo:
{
  "customer": "Sandro",
  "products": [
    {
      "name": "Macarrão",
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

## Busca das vendas para a página inicial
- A busca das vendas para a página inicial é feita de maneira paginada, pensando em trazer a quantidade pertinente pensando em uma adição de registros no longo prazo.

## Remoção de uma venda no sistema
- A remoção de uma venda no sistema é efetuada através do endpoint de DELETE com o salesId que está cadastrado no banco

## Atualização de uma venda no sistema
- A atualização de uma venda no sistema acontece após buscar a entidade no banco de dados para permitir mudanças no que já está cadastrado, ou adicionar um novo produto.
- Payload de exemplo:
{
  "salesId": 1,
  "customer": "Denilton",
  "products": [
    {
      "name": "Macarrão",
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