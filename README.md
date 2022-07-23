# Cadastro simplificado de produtos.

## 1. Cenário

A DuckSales Ltda. tem um sistema de controle de produtos que fornece essas informações para a sua loja virtual. As informação de preço e estoque mudam com grande frequencia dado seu volume de vendas e as promoções, chegando à milhares de mudanças em um segundo.

Para minificar os acessos ao seu bando de dados transacional o time de TI decidiu criar um banco NOSQL onde os dados do produto seram salvos em um formato simplificado e a API de consulta de produtos, que esta em gestão de outro time da empresa.

## 2. Solução

![C4 dos containers da solução](./_assets/_assets/SistemaProdutos.png)

A solução desse repositório consiste Producer e um Consumer de mensagens Kafka.

O Producer é um Worker desenvolvido em .NET que gera dados de produtos fictícios, os salva em um banco de dados SQL e envia para um tópico Kafka. O volume de transações por segundo e o número de registro no banco de dados são configurados em seu appsettings ou em variáveis de ambiente utilizadas pelo serviço.

As mensagens do tópico Kafka são consumidas por uma Azure Function que utiliza Kafka Trigger. Nessa Function os dados da mensagem são simplificados e salvos em um banco de dados MongoDB.

Tanto o Producer quanto o Consumer são _containerizados_ para sua publicação na nuvem.

## 3. Execução Local

TDB

## 4. Arquitetura de Solução do Azure

TDB

## 5. Execução no Azure

TDB

## Referências

