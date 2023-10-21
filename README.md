## Imersão em ASP.NET MVC

![Logo do Curso](https://storage.googleapis.com/assets.kiwify.com.br/PQ1efkcHFZfsSAM/COVER_ASPNET_6dc8ed55c93348f09a6780729fc51e05_fcd0c598a3fe4e5683294c88b95d55cd.png)

Recebi este curso do generoso [Michel Banagouro](https://github.com/mbanagouro), a quem agradeço imensamente. Originalmente, o projeto do curso foi construído em .NET Framework 4.5, já que o curso foi realizado em 2014. No entanto, tenho certeza de que esse detalhe só enriqueceu minha aprendizagem e conhecimento. Isso porque meu projeto foi desenvolvido no .NET 7, o que me levou por um caminho diferente dos cursos atuais. O hábito de copiar e colar foi abandonado, uma vez que muitas vezes isso não funcionava e quebrava o código. Debugar e pesquisar se tornaram as palavras de ordem durante essas semanas, e essa experiência me ensinou de verdade como pesquisar e resolver problemas. Estou extremamente grato a Michel Banagouro por essa oportunidade.

### Blog

Foram feitas várias alterações no código, mas nenhuma funcionalidade foi deixada para trás. Meu projeto mantém todas as funcionalidades da aplicação original e as aprimora. (Mudanças feitas no final do Readme)

#### Linguagens e Ferramentas

- .NET 7, ASP.NET Core MVC, Partial View, View Components, Section.
- Visual Studio, Manager Secret user.
- Identity, Authentication, Authorization.
- Azure DevOps, Azure Boards, Azure Pipeline, Azure Blob.
- Git, GitHub.
- SendGrid, API SendGrid.
- Cache, IMemoryCache.
- Entity Framework, Migration, Code First.
- C#, Bootstrap, CSS3, HTML5, JavaScript, JQuery, Ajax. 
- Redactor, DataTable, Autocomplete.

### Lean Education / Leanwork Group

A Leanwork oferece soluções de transformação digital para o varejo, como aplicativos nativos e uma plataforma de comércio digital para empresas que estão pensando à frente do seu tempo e conectadas com o futuro. Isso proporciona autonomia, customização e exclusividade para que as marcas aproveitem tudo o que a tecnologia de ponta pode agregar ao negócio. A empresa conta ainda com uma equipe de alta performance que oferece todo o suporte necessário para as empresas se manterem à frente no comércio digital.

### Professor: Michel Banagouro

![Foto do professor que será meu mentor](https://github.com/raphael-rfa/AspNetMvcBlog/blob/main/imagens/michelimg.png)

Michel Banagouro é Co-Fundador, CTO e Arquiteto de Software na Leanwork Group, com mais de 10 anos de experiência como analista, desenvolvedor e arquiteto de software. Ele tem expertise no desenvolvimento de aplicações web de grande porte.

# Principais Mudanças e Funcionalidades

No curso, a aplicação foi criada no antigo VSO (Visual Studio Online), que agora é Azure DevOps. Para aprender a usar o Azure DevOps, realizei um pequeno curso no Microsoft Learn e utilizei o curso do [Julio Arruda](https://github.com/julitogtu) que está disponível gratuitamente aqui no GitHub. Organizei as tarefas do projeto com base nas aulas, e todos os commits do projeto estão organizados, com seus Pull Requests vinculados às tarefas do Azure Boards. O projeto ainda não está em produção devido ao alto custo dos bancos de dados da Azure e ao meu atual desemprego.

O Blog segue o padrão MVC, mas a autenticação para a parte de administração foi originalmente feita com o FormAuthentication, que não existe no .NET Core. No meu projeto, utilizei o Identity para autenticação, o que é necessário para criar, editar e excluir postagens. A criação de postagens é feita com o Redactor, um editor de texto para web, conectado ao Azure Blob para o upload de imagens. O campo "Categories" contém um recurso de autocompletar para facilitar o cadastro. Se a categoria não existir, a ação cria uma categoria. No painel do administrador, as postagens são listadas com um DataTable que faz requisições via Ajax para receber os dados das postagens em formato JSON e exibir os botões de edição e exclusão para cada post.

A página principal é paginada com um método que cria uma URL. No entanto, suas propriedades não funcionam no .NET Core, então peguei uma classe de paginação de um tutorial da Microsoft e a aprimorei, criando construtores e extensões para tornar a aplicação mais eficiente, assim como no curso. A página "details" contém todas as informações de uma postagem, e os links das tags da postagem retornam uma lista de postagens com a mesma tag. A página exibe o conteúdo da postagem e os comentários. Um comentário pode ser adicionado por meio de um modal, que é armazenado em uma Partial View e exibido por meio de um View Component.

Na página inicial, há um sistema de cache que evita buscas excessivas no banco de dados. No entanto, o cache é invalidado quando uma postagem é criada ou editada. O layout padrão do site inclui uma Partial View com a Navbar do blog, uma Partial View com a Sidebar e uma Partial View com o Footer. Na Sidebar, há um campo de pesquisa que retorna as postagens paginadas e uma lista de categorias. No .NET Framework, a listagem é feita com a tag helper `@Html.Action()` que chama a action diretamente, uma vez que essa action retorna uma Partial View. No .NET Core, essa funcionalidade foi incorporada no View Component, que é semelhante ao Controller, mas com algumas limitações. No Footer do site, há um link de contato que leva o usuário a uma página de contato. Para o envio de e-mails, o curso utilizava o pacote ActionMailer, que, no entanto, não foi atualizado desde aquela época e permanece na versão 0.7.4, sem suporte para o AspNetCore.Mvc. No meu projeto, utilizei a API do SendGrid.
