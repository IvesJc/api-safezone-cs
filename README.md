# <img src="https://github.com/user-attachments/assets/facb5d05-09b4-4096-afe4-ee873a92e40f" alt="safezone-icon" width="70"/> SafeZone API

API RESTful desenvolvida para gerenciamento de ocorrências relacionadas a desastres naturais e assistência a vítimas, com foco em integração de serviços modernos como Azure AI, RabbitMQ e MongoDB para logs. Este projeto visa ser robusto, escalável e altamente interoperável com serviços externos.

---

## 📌 Índice

- [Descrição](#descrição)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Arquitetura do Projeto](#arquitetura-do-projeto)
- [Entidades Principais](#entidades-principais)
- [Funcionalidades](#funcionalidades)
- [Integrações Externas](#integrações-externas)
- [Testes](#testes)
- [Como Executar](#como-executar)
- [Estrutura de Pastas](#estrutura-de-pastas)
- [Contribuições](#contribuições)
- [Licença](#licença)

---

## 🧾 Descrição

A SafeZone API tem como objetivo mapear e gerenciar ocorrências naturais (como enchentes e terremotos), permitindo o registro de vítimas e disparo de alertas automatizados com base em dados e predições de risco. O sistema inclui:

- Cadastro e gestão de **Ocorrências** e **Vítimas**
- Emissão de **Alertas** baseados em condições geográficas
- Uso de **Machine Learning (Azure AI)** para previsão de riscos
- Arquitetura de microsserviços com **RabbitMQ** para envio de notificações
- Registro de logs com **MongoDB**
- Segurança e autenticação com **OAuth2**
- Suporte a HATEOAS para hiperlinks nas respostas

---

## 💡 Tecnologias Utilizadas

| Tecnologia        | Finalidade                         |
|-------------------|-------------------------------------|
| ASP.NET Core 8    | Backend e estrutura RESTful         |
| Entity Framework  | ORM e Migrations                    |
| OracleSQL         | Banco de dados relacional principal |
| MongoDB           | Armazenamento de logs               |
| Azure AI Studio   | Predições de desastres naturais     |
| RabbitMQ          | Microsserviço de notificações       |
| Swagger / Swashbuckle | Documentação da API            |
| xUnit / Moq       | Testes unitários e de integração    |

---

## 🏗 Arquitetura do Projeto
📁 api-safezone-cs
├── Controllers
├── Domain
│ ├── Entities
│ ├── Enums
├── DTOs
│ ├── Request
│ ├── Response
├── Services
│ ├── Interfaces
│ ├── Implementations
├── Repositories
│ ├── Interfaces
│ ├── Implementations
├── Data
│ ├── Migrations
│ ├── ApplicationDbContext.cs
├── Mapper
├── RabbitMQ
├── AzureAI
└── Tests

## 🧩 Entidades Principais

### `Ocorrencia`
- Tipo (`Enchente`, `Terremoto`, etc.)
- Status (`Aberta`, `Fechada`)
- Prioridade (`Alta`, `Média`, `Baixa`)
- Localização (Latitude, Longitude)
- Lista de vítimas associadas

### `Vitima`
- Nome, idade, condição
- Relacionada a uma ocorrência

### `Alerta`
- Dados meteorológicos e de risco
- Integrado com Azure AI
- Pode gerar notificações via RabbitMQ

---

## 🔧 Funcionalidades

✅ Cadastrar, editar, deletar e listar:
- Ocorrências
- Vítimas
- Alertas

✅ HATEOAS nas respostas de recursos

✅ Integração com Azure AI para predição de risco (ex: enchentes iminentes)

✅ Envio de mensagens via RabbitMQ para um `notification-service`

✅ Testes unitários e de integração com cobertura de controllers e services

---

## 🔌 Integrações Externas

### Azure AI Studio
- Utilizado para inferência de modelos de machine learning relacionados à previsão de desastres.
- Consulta via HTTP ao endpoint do modelo.

### RabbitMQ
- Utilizado para envio assíncrono de mensagens de alerta.
- Exemplo: ao registrar uma nova ocorrência crítica, uma mensagem é publicada na fila de alertas.
---

## 🧪 Testes

- `xUnit` e `Moq` para unit e integration tests
- `WebApplicationFactory` para testes com controllers e endpoints
- Casos testados:
  - Criar/Atualizar/Deletar entidades
  - Geração de HATEOAS
  - Validação de DTOs

---

## ▶️ Como Executar
Acessar a API: <a>https://api-safezone-b6bkhmbtg0azebhd.brazilsouth-01.azurewebsites.net/index.html</a>
