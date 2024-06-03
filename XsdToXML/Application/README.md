# Estrutura de pasta;
    - Application/
       - Configuration/
       - Domain/
       - Services/
       - Middleware/
       - Infrastructure/
    - Program.cs
# Detalhes sobre a nova estrutura:
    Application:
        Configuration: Para classes de configuração.
        Services: Serviços aplicacionais que contêm lógica de negócio.
        Middleware: Middlewares para a aplicação.
    /Domain:
        Models: Classes de modelo que representam os dados e comportamentos da aplicação.
        Services: Serviços de domínio que contêm lógica de negócio pura.
    /Infrastructure:
        Controllers: Controladores da API.
        DTOs: Objetos de transferência de dados.
