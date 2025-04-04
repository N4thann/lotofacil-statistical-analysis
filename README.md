# lotofacil-statistical-analysis
Este projeto foi desenvolvido em .NET 8, utilizando a Onion Architecture, um estilo de arquitetura que segue os princípios da Clean Architecture, proporcionando uma separação bem definida entre camadas e facilitando a manutenção, testes e evolução do sistema.

O foco principal deste projeto é o backend, porém o frontend também recebeu atenção visual com a utilização do template Admin LTE 3.2 adaptado com Bootstrap 4, que costumo usar em meus projetos para fins educacionais e prototipagem rápida.

✨ Funcionalidades principais
Cadastro e gerenciamento de concursos (CRUD)

Análise diária de novos concursos com base em concursos anteriores

Dashboard com visualização de análises estatísticas

Download de relatórios em planilhas Excel

Execução de jobs em segundo plano com Hangfire

Validações robustas com FluentValidation

Logs detalhados de atividades dos concursos

🧠 Lógica do sistema
Quase todos os dias, um novo Contest (concurso do dia) é adicionado ao sistema. Esse concurso é comparado com uma lista de até 10 BaseContests cadastrados previamente. Se houver pelo menos 11 a 15 acertos entre o novo concurso e algum concurso base, ambos são relacionados entre si.

Cada BaseContest possui listas separadas (por acerto: 11 a 15) com os concursos que tiveram essa correspondência.

Cada Contest também armazena os concursos base com os quais teve correspondência.

Este relacionamento é muitos para muitos, utilizando uma entidade intermediária chamada ContestActivityLog, que também registra a quantidade de acertos e a ação realizada.

Exemplo prático
Se o concurso #3234 teve 14 acertos em comum com o concurso base #44, então:

O BaseContest #44 adiciona o Contest #3234 à sua lista de acertos 14

O atributo Matched14 é incrementado

Um registro é criado na tabela ContestActivityLog com os detalhes da ação

⚙️ Serviços com Hangfire
MainJobHandler
Responsável pela lógica principal de comparação entre concursos. Ele evita repetições através do campo LastProcessed e utiliza UnitOfWork para reduzir operações excessivas no banco.

TopTenNumbers
Analisa os 10 números mais frequentes entre os concursos relacionados a cada BaseContest e armazena o resultado como uma string em um campo específico.

📦 Arquitetura
A arquitetura segue os princípios da Onion Architecture, separada nas seguintes camadas:

Domain – Entidades e regras de negócio

Application – Casos de uso, interfaces e validações

Infra.Data – Acesso a dados com Entity Framework e SQL Server

Infra.IoC – Injeção de dependência e configurações globais

Presentation – Camada Razor Pages para a interface do usuário

🧱 Entidades principais
BaseContest

Contest

ContestActivityLog

ContestBaseEntity (classe base herdada pelas demais)

🛠️ Extras e utilitários
Importação via Excel: Funcionalidade oculta que permite alimentar o banco com mais de 3000 concursos reais da Lotofácil, através de leitura de arquivos Excel.

Repositório genérico: Implementação de um repositório padrão para operações genéricas, com métodos específicos como GetAllQueryable() para relatórios e filtros avançados.

Jobs com interface IJobHandler: Cada job implementa uma interface comum, permitindo organização e execução padronizada via Hangfire.

⚠️ Observações
Este projeto foi desenvolvido exclusivamente para fins de aprendizado. A lógica estatística aplicada não tem validade para apostas reais, já que os concursos possuem natureza aleatória. O uso de práticas avançadas e arquitetura elaborada tem como objetivo o estudo e domínio das tecnologias envolvidas, mesmo que seja para um projeto de pequena escala.

![sysListadeConcursos](https://github.com/user-attachments/assets/a7ae3f3d-f297-4fcc-a2be-3ba2fee2a28d)

![sysTop2Concursos](https://github.com/user-attachments/assets/98419184-df13-4fd0-8ea8-e20ad773c614)

![sysDash3](https://github.com/user-attachments/assets/af7d7e24-b0d0-4166-a6e6-ca7f260a24a1)

![sysListaLogs](https://github.com/user-attachments/assets/ec41e684-f83d-43f4-be8d-9404c4ef9778)

![sysSobre](https://github.com/user-attachments/assets/dfa503c0-b548-4e6f-87a3-bf62db650b01)









