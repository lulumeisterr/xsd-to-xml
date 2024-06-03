# Estrutura de pasta;
    - src/
      - Application/
        - Configuration/
        - Services/
        - Middleware/
      - Domain/
        - Models/
        - Services/
      - Infrastructure/
        - Controllers/
        - DTOs/
      - Program.cs


# Detalhes sobre a nova estrutura:
    Application:
        Configuration: Para classes de configuração.
        Services: Serviços aplicacionais que contêm lógica de negócio.
        Middleware: Middlewares para a aplicação.
    Application/Domain:
        Models: Classes de modelo que representam os dados e comportamentos da aplicação.
        Services: Serviços de domínio que contêm lógica de negócio pura.
    Application/Infrastructure:
        Controllers: Controladores da API.
        DTOs: Objetos de transferência de dados.
