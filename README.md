# Ifficient School API


Este projeto fornece uma API completa para gerenciar notas de alunos em uma instituição de ensino. A API oferece diversas funcionalidades, como consultar alunos aprovados e reprovados, obter o melhor aluno por matéria, buscar um aluno específico pelo número de matrícula e ordenar alunos por média de notas.


## Funcionalidades


A API possui as seguintes rotas:

  

### 1. **Obter Alunos Aprovados e Reprovados**

  

-  **Endpoint:**  `GET /api/students/approved-failed`

-  **Descrição:** Recupera uma lista de alunos aprovados e reprovados com base nas suas notas.

-  **Respostas:**

-  `200 OK`: Sucesso, retorna a lista de alunos aprovados e reprovados.

-  `500 Internal Server Error`: Erro interno no servidor.

  

### 2. **Obter Melhor Aluno por Matéria**

  

-  **Endpoint:**  `GET /api/students/best-by-subject`

-  **Descrição:** Recupera o melhor aluno por matéria, baseado nas notas.

-  **Respostas:**

-  `200 OK`: Sucesso, retorna o melhor aluno por matéria.

-  `500 Internal Server Error`: Erro interno no servidor.

  

### 3. **Obter Todos os Alunos**

  

-  **Endpoint:**  `GET /api/students`

-  **Descrição:** Recupera todos os alunos registrados no sistema.

-  **Respostas:**

-  `200 OK`: Sucesso, retorna a lista de todos os alunos.

-  `500 Internal Server Error`: Erro interno no servidor.

  

### 4. **Obter Aluno por Matrícula**

  

-  **Endpoint:**  `GET /api/students/{registration}`

-  **Descrição:** Recupera os detalhes de um aluno específico pelo número de matrícula.

-  **Respostas:**

-  `200 OK`: Sucesso, retorna os detalhes do aluno.

-  `404 Not Found`: Aluno não encontrado.

-  `500 Internal Server Error`: Erro interno no servidor.

  

### 5. **Ordenar Alunos por Média de Notas**

  

-  **Endpoint:**  `GET /api/students/sort`

-  **Descrição:** Ordena os alunos com base na média de notas utilizando o algoritmo de ordenação especificado (Bubble Sort ou Quick Sort).

-  **Parâmetros de Query:**

-  `strategy`: Estratégia de ordenação, pode ser `bubble` ou `quick` (padrão é `bubble`).

-  **Respostas:**

-  `200 OK`: Sucesso, retorna a lista de alunos ordenada pela média de notas.

-  `400 Bad Request`: Estratégia de ordenação inválida.

-  `500 Internal Server Error`: Erro interno no servidor.

  

## Controle do CSV

 **Rodar localmente:**

1.  `dotnet restore`

2. copiar a pasta Data e colar dentro de bin/Debug/net8.0 onde ficam os arquivos do projeto

3. dotnet run para iniciar o projeto & `dotnet watch` para visualizar alterações no código em tempo real

- obs: no ambiente do visual code ele realiza todo esse papel por debaixo dos panos.

  

## Testes Unitários

  

Este projeto inclui testes unitários para validar as funcionalidades da API.

  

### Como Rodar os Testes

  

1. Clone o repositório:

```bash

git clone https://github.com/seu-usuario/ifficient-school-api.git

```

2. Abra o projeto no Visual Studio ou utilize o terminal para rodar os testes com o comando:

```bash

dotnet test

```

  

## Swagger

  

A API está configurada com Swagger para documentação interativa das rotas. Para acessar a documentação, basta rodar o projeto e navegar até o seguinte URL:

  

```

http://localhost:{porta}/swagger

```

  

## Tecnologias Utilizadas

  

- ASP.NET Core 6

- Swashbuckle (Swagger)

- xUnit para testes unitários

  

## Como Rodar o Projeto com Docker

  

Este projeto já possui um `Dockerfile` configurado. Siga os passos abaixo para rodar a aplicação dentro de um container Docker:

  

### 1. **Certifique-se de que o Docker está instalado**

  

### 2. **Construir a imagem Docker**

  

Para construir a imagem Docker a partir do `Dockerfile`, execute o seguinte comando no diretório raiz do projeto, onde o `Dockerfile` está localizado:

  

```bash

docker  build  -t  ifficient-school-api  .

```

  

### 3. **Rodar o container Docker**

  

Após a imagem ser construída, execute o container com o seguinte comando:

  

```bash

docker  run  -d  -p  80:80  --name  ifficient-school-container  ifficient-school-api

```

  

Isso irá:

- Rodar o container em segundo plano (`-d`).

- Mapear a porta 80 do container para a porta 80 da sua máquina local (`-p 80:80`).

- Nomear o container como `ifficient-school-container`.

  

### 4. **Acessar a aplicação**

  

Agora, a API estará rodando no Docker. Você pode acessar a API no seguinte URL:

  

```

http://localhost:80

```

  

### 5. **Parar o container Docker**

  

Caso precise parar o container, utilize o comando:

  

```bash

docker  stop  ifficient-school-container

```

  

### 6. **Remover o container Docker (opcional)**

  

Se você quiser remover o container após parar, utilize o comando:

  

```bash

docker  rm  ifficient-school-container

```
### 7. **Clean Architecture e Organização de Pastas**

Este projeto adota os princípios da **Clean Architecture**, propostos por Robert C. Martin (Uncle Bob). A Clean Architecture organiza o código em camadas, priorizando o isolamento de dependências e a separação clara de responsabilidades, o que resulta em maior flexibilidade, testabilidade e facilidade de manutenção.

### Estrutura de Pastas

A estrutura do projeto foi organizada para refletir os componentes principais da Clean Architecture:
```

src/
├── Application/
│   ├── UseCases/               # Casos de uso (regras de negócio específicas)
│   └── Interfaces/             # Interfaces para comunicação entre camadas
├── Domain/
│   ├── Entities/               # Entidades principais do domínio
│   └── ValueObjects/           # Objetos de valor (dados imutáveis, como matrícula ou notas)
├── Infrastructure/
│   ├── Repositories/           # Implementações concretas de repositórios
└── Presentation/
    ├── Controllers/            # Controladores que expõem os endpoints da API
    └── DTOs/                   # Objetos para transferência de dados (input/output)

```
### Princípios da Clean Architecture

A Clean Architecture organiza o código em **círculos concêntricos**, com cada camada tendo responsabilidades distintas e dependências direcionadas **de fora para dentro**:

1. **Domínio (Core)**:
   - Contém as **entidades** e **regras de negócio fundamentais** da aplicação.
   - Não depende de frameworks, bibliotecas ou infraestrutura.
   - Representa o núcleo da aplicação.

2. **Aplicação (Application)**:
   - Implementa os **casos de uso** específicos.
   - Define **interfaces** que o domínio precisa para interagir com componentes externos (ex.: repositórios, APIs).
   - É uma camada intermediária que coordena a lógica de aplicação entre o domínio e as interfaces externas.

3. **Interface Externa e Infraestrutura (Infrastructure e Presentation)**:
   - **Infraestrutura**: Implementa os repositórios concretos e serviços que fornecem dados ao sistema.
   - **Apresentação**: Define os controladores da API, transformando dados de entrada/saída para comunicar com o cliente.

### Benefícios

- **Independência de tecnologia**: O núcleo do sistema (Domínio e Aplicação) não depende de frameworks, tornando-o fácil de portar para outras tecnologias.
- **Alta testabilidade**: Camadas internas podem ser testadas isoladamente usando mocks ou stubs.
- **Facilidade de manutenção**: A separação clara de responsabilidades reduz a complexidade e o impacto de mudanças.

### Separação de Responsabilidades

- **Entidades** modelam os conceitos principais do domínio.
- **Casos de uso** contêm a lógica de aplicação, executando regras de negócio específicas.
- **Repositórios** implementam acesso a dados e são injetados via interfaces.
- **Controladores** recebem as requisições HTTP e delegam a lógica para os casos de uso.

Essa abordagem garante um projeto robusto e escalável, preparado para crescer e se adaptar a novas exigências com o tempo.