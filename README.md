
# LSRE: Skill Rating Estimator 2

O LSRE 2 é uma biblioteca de classes que permite encontrar estimadores de Bayes para o nível de proficiência de cada jogador em um conjunto, com base em um histórico de resultados de partidas de um mesmo jogo. Em outras palavras, o LSRE 2 é capaz de observar um histórico de partidas entre jogadores, por menor que seja, e encontrar a estimativa mais "justa" para o nível de proficiênica de cada jogador.

Decidi criar essa biblioteca como uma reconstrução do software LSRE, também criado por mim há alguns meses para me ajudar a criar um modelo para prever o resultado de partidas no jogo Quake Champions. Basicamente, a mosca era acertar mais no bolão do que meus amigos (hehe) e eu construí um canhão pra matar.

Depois de trabalhar muito tempo na GLTech 2, minha engine gráfica de Raycasting, projeto de que mais sinto orgulho até hoje, minhas skills com C# e minha experiência com desenvolvimento mudaram drásticamente, me fazendo ter vontade de reconstruir do zero o sistema de estimação de pontuação assim que o vi de novo.

Estou deixando público o repositório da LSRE 1 para que possam ver e comparar a qualidade de código de um com o outro. Recomendo que vejam primeiro o de cá, uma vez que está documentadinho no Doxygen.

**Por que "LSRE"?**

LSRE são as iniciais de LSRE: Skill Rating Estimator, e eu costumo pronunciar como "lis-re". Basicamente achei bonita a pronúncia assim, mas jamais saberão que escolhi L por causa do meu sobrenome.

## Como compilar

Para compilar a bilbioteca, caso você tenha o Visual Studio Community 2019 instalado, abra o arquivo em ```src/LSRE 2.sln``` e _seja feliz_; o compilador vai gerar uma pasta chamada ```/bin``` na raiz do repositório contendo a DLL resultante.

Você pode referenciar tanto o arquivo DLL compilado quanto o projeto do LSRE se o seu ecossistema for compatível.

Essa versão do LSRE está escrita em C# é projetada para o ecossistema .NET Framework 4.7.2; no entanto, pode ser facilmente adaptado para outros ecossistemas do .NET sem muitas adaptações de código.

A documentação da biblioteca está disponível em https://king-witcher.github.io/lsre-2/.

Giuseppe Lanna
07-05-2021

> Written with [StackEdit](https://stackedit.io/).