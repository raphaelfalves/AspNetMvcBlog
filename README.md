## Imersão ASP.NET MVC

<img width="52%" align="left" height="auto" src="https://storage.googleapis.com/assets.kiwify.com.br/PQ1efkcHFZfsSAM/COVER_ASPNET_6dc8ed55c93348f09a6780729fc51e05_fcd0c598a3fe4e5683294c88b95d55cd.png" alt="Logo do Curso"/>
</br>
Ganhei esse curso do <a href="https://github.com/mbanagouro">Michel Banagouro</a> que tem um coração imenso ao qual agradeço de MAIS. O projeto do curso foi originalmente contruido no .Net Framework 4.5 já que o curso foi feito em 2014, mas tenho certeza de que esse detalhe só agregou mais ao meu aprendizado e conhecimento já que o meu projeto foi feito no .Net 7, essa decisão me forneceu um caminho diferente dos cursos de hoje, o padrão copia e cola foi totalmente abandonado na marra, já que em grande parte das vezes isso não funcionava e ainda quebrava o codigo, <b>debugar e pesquisar</b> virou o lema nessas 2 semanas e isso me ensinou de verdade a como pesquisar e resolver problemas, essa experiência me fez evoluir muito e só tenho a agradecer ao Michel.
</br>

## Blog

Diversas mudanças foram feitas no código mas nenhuma funcionalidade foi deixada para trás, meu projeto mantém todas as funcionalidades da aplicação original ou as melhoram.(Mudanças feitas no fim do Readme)

#### Linguagens e Ferramentas

- .Net 7, ASP.NET core MVC, Partial View, View Components, Section.
- Visual Studio, Manager Secret user.
- Identity, Authentication, Authorization.
- Azure DevOps, Azure Boards, Azure Pipeline, Azure Blob.
- Git, GitHub.
- SendGrid, API SendGrid.
- Cache, IMemoryCache.
- Entity Framework, Migration, Code First.
- C#, Bootstrap, CSS3, HTML5, JavaScript, JQuery, Ajax. 
- Redactor, DataTable, Autocomplete.

## Lean Education/ Leanwork Group
<img width="250" align="left" src="https://media.licdn.com/dms/image/C4D0BAQHyEZH9yIkdMA/company-logo_200_200/0/1660671071509?e=1680739200&v=beta&t=5vbriGE2IVut3QoY_KN8StSuiZ2fhtssIQaTUSoFuM4" alt="Logo da empresa que eu vou trabalhar"/>
</br>

A Leanwork oferece soluções de transformação digital para o varejo,  
como aplicativos nativos e plataforma de comércio digital para empresas  
que estão pensando à frente do seu tempo e conectadas com o futuro.  
  
Proporciona autonomia, customização e exclusividade para que as marcas  
aproveitem tudo o que a tecnologia de ponta pode agregar para o negócio.  

Também conta com uma equipe de alta performance, que oferece todo  
o suporte necessário para a empresa se manter à frente no comércio  
digital.

</br>

### Professor : Michel Banagouro

<img width="150" align="left" src="https://github.com/raphael-rfa/AspNetMvcBlog/blob/main/imagens/michelimg.png" alt="foto do professor que vai ser meu senior"/> 

Co-Founder / CTO / Arquiteto de Software / ASP.NET Sênior na Leanwork Group

Mais de 10 anos de experiência como analista, desenvolvedor e arquiteto  
de software com expertise em desenvolvimento de aplicações web de grande  
porte.
</br>
</br>
</br>
# Principais mudanças e funcionalidades

No curso ele cria a aplicação no antigo VSO hoje Azure DevOps, para aprender a usar o Az DevOps fiz o cursinho no Microsoft Learn e usei o curso do Julio Arruda que esta disponível e organizado gratuitamente aqui no GitHub, organizei as tasks do projeto baseado nas aulas e todos os commits do projeto estão organizados e seus Pull Requests lincados as tasks do AZ Boards, o projeto só não está em produção por conta do alto valor dos bancos de dados da azure e eu ainda estar desempregado.

O Blog é construído no padrão MVC e isso não muda, mas para a parte de admin ele faz a autenticação pelo FormAuthentication que não existe no .net core. No meu projeto usei o Identity que requer a autenticação para criar, editar e deletar posts, a criação das postagens é feita com o redactor, editor de texto para a web que conectei ao Azure Blob para fazer o upload de imagens. No input de "Categories" contém o autocomplete para facilitar o cadastro, mas se a categoria não existir não tem problema a action cria uma categoria, no dashboard do adm os posts são listados com um DataTable que envia a requisição via Ajax e recebe os dados dos posts via Json e exibe os botões para editar e deletar cada post.

A View principal é paginada com um método que cria uma URL, porém suas propriedades não funcionam no .Net core então peguei uma classe de paginação de um tutorial da Microsoft que já tinha feito e melhorei criando Construtores e extensões deles deixando a aplicação mais rápida como no curso. A pagina details contém todas as informações de um post, os links das tags do post retornam uma lista dos posts com aquela tag, a pagina apresenta o conteúdo do post e os comentários, um comentário é adicionado através de um modal, que está armazenado em uma Partial View e exibidos através de uma View Component.

Na página home contém um Cache que evita a busca excessiva no banco de dados, mas são removidos quando um post é criado ou editado, o layout padrão do site contém uma Partial View com a Navbar do blog, uma Partial View com a Sidebar, e uma Partial View com o Footer. Na SideBar ha um campo de pesquisa que retorna os posts paginados e uma lista das categorias, no .Net Framework a listagem é feita com a tag helper @Html.Action() que chama a action diretamente já que essa action retorna uma Partial View, no .Net core essa funcionalidade foi incorporada ao View Component que é semelhante ao Controller, mas que tem algumas limitações. No Footer do site, contém um link de contato que leva o usuário para uma página de contato, o envio de e-mail pelo curso é feito com o pacote ActionMailer, porém esse pacote não foi atualizado deis daquela época e se encontra na mesma versão 0.7.4 e não tem suporte ao AspNetCore.Mvc, para o envio de e-mail no meu projeto, usei a API do SendGrid.
