# Algoritmo de Dijkstra

O Algoritmo de Dijkstra soluciona o problema do caminho mais curto num grafo dirigido ou não dirigido com arestas de peso não negativo.

Execute no Visual Studio 2015 e terá a resposta dos caminhos mínimos para:

* Para o grafo abaixo adaptado do livro Algoritmos (Cormen) 3rd, página 480:
    
    ![Grafo adptado Comern](https://raw.githubusercontent.com/dedeco/dijkstra-bellman-ford/master/grafos-imagens/teste_cormen_djt.png)


# Algoritmo de Bellman-ford

O algoritmo de Bellman-Ford resolve o problema de caminhos mínimos de fonte única no caso geral no qual os pesos das arestas podem ser negativos. O algoritmo retorna um valor booleano que indica se existe ou não um ciclo de peso negativo.

Execute no Visual Studio 2015 e terá a resposta dos caminhos mínimos com pesos negativos para:

* Para o grafo abaixo adaptado do livro Algoritmos (Cormen) 3rd, página 474:

    ![Grafo adptado Comern](https://raw.githubusercontent.com/dedeco/dijkstra-bellman-ford/master/grafos-imagens/teste_cormen_bford.png)

* E ainda tem um outro grafo que retorna um ciclo negativo.

Eu desenvolvi o mesmo algoritmo em python. Veja aqui> https://github.com/dedeco/dijkstra-bellman-ford
