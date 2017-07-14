using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caminho_minimo
{
    class Program
    {
        static void initialize_single_source(Grafo g, Vertice s){
            foreach (KeyValuePair<string, Vertice> v in g.get_vertices()) {
                v.Value.set_distancia(int.MaxValue);
            }
            s.set_distancia(0);
        }

        static Vertice extract_min(SortedDictionary<string, Vertice> Q)
        {
            var key = Q.Keys.ToList()[0];
            Vertice min = Q[key];
            foreach (KeyValuePair<string, Vertice> v in Q)
            {
                if (v.Value.get_distancia() < min.get_distancia())
                {
                    min = v.Value;
                }
            }
            Q.Remove(min.get_id());
            return min;
        }

        static void relax(Vertice u, Vertice v) {

            int distancia = u.get_distancia() + u.get_peso(v);

            if (v.get_distancia() > distancia)
            {
                v.set_distancia(distancia);
                v.set_vertice_caminho_anterior(u);
                //Console.WriteLine("Atualizei a distância " + distancia + " do vértice " + u.get_id()  + " para o vértice " + v.get_id());
            }
            else {
                //Console.WriteLine("NÃO atualizei a distância " + distancia + " do vértice " + u.get_id() + " para o vértice " + v.get_id());
            }           
        }

        static void add_S(Vertice u, SortedDictionary<string, Vertice> S)
        {
            Vertice vertice;
            if (S.TryGetValue(u.get_id(), out vertice))
            {
                vertice = u;
            }
            else
            {
                S.Add(u.get_id(),u);
            }
        }

        static void calcula_caminho(Vertice alvo, ArrayList caminho)
        {
            if (caminho.Count == 0)
            {
                caminho.Add(alvo);
            }
            while (alvo.get_vertice_caminho_anterior() != null)
            {
                caminho.Add(alvo.get_vertice_caminho_anterior());
                alvo = alvo.get_vertice_caminho_anterior();
            }
        }

        static void imprime_caminho(ArrayList caminho, Vertice origem, Vertice vertice_alvo)
        {
            Console.Write("O custo caminho do ");
            for (int i = caminho.Count - 1; i >= 0; i--)
            {
                Vertice v = (Vertice)caminho[i];
                Console.Write(v.get_id() + ", ");
            }
            Console.Write(" é " + vertice_alvo.get_distancia() + ".");
            Console.WriteLine("");
        }

        public static void Dijkstra(Grafo g, Vertice s)
        {
            SortedDictionary<string, Vertice> Q = new SortedDictionary<string, Vertice>();            

            initialize_single_source(g, s);

            Q = g.get_vertices();

            SortedDictionary<string, Vertice> S = new SortedDictionary<string, Vertice>();

            while (Q.Count > 0) {

                Vertice u = extract_min(Q);

                u.set_visitado(true);

                foreach (KeyValuePair<Vertice, int> v in u.get_adjacentes()) {
                    if (v.Key.get_visitado() == true){
                        continue;
                    }
                    relax(u, v.Key);
                }
                add_S(u, S);                
            }

            /* S tem os pesos finais de caminho mínimos a partir da fonte determinada, assim atualiza o grafo 
             * com os vértices atualizados*/
            g.set_vertices(S); 
        }

        public static bool BellmanFord(Grafo g, Vertice s) {

            initialize_single_source(g, s);

            foreach (KeyValuePair<string, Vertice> u in g.get_vertices()){
                foreach (Tuple<Vertice, Vertice> a in g.get_arestas()){
                        relax(a.Item1, a.Item2);
                    }
            }                                

            foreach (Tuple<Vertice, Vertice> a in g.get_arestas()){                
                if (a.Item2.get_distancia() > a.Item1.get_distancia() + a.Item1.get_peso(a.Item2)){
                    return false;
                } 
            }
             
            return true;
        }        

        static void Main(string[] args)
        {
            Console.WriteLine("Dijkstra");

            Grafo g = new Grafo(true);

            ArrayList caminho = new ArrayList();
            Vertice origem = g.get_vertice("a");
            Vertice vertice_alvo = g.get_vertice("d");
            
            g.inserir_vertice("a");
            g.inserir_vertice("b");
            g.inserir_vertice("c");
            g.inserir_vertice("d");
            g.inserir_vertice("e");

            g.inserir_aresta("a", "b", 10);
            g.inserir_aresta("a", "c", 3);
            g.inserir_aresta("b", "c", 1);
            g.inserir_aresta("b", "d", 2);
            g.inserir_aresta("c", "b", 4);
            g.inserir_aresta("c", "d", 8);
            g.inserir_aresta("c", "e", 2);
            g.inserir_aresta("d", "e", 7);
            g.inserir_aresta("e", "d", 9);

            caminho = new ArrayList();
            origem = g.get_vertice("a");
            vertice_alvo = g.get_vertice("d");

            Console.WriteLine("Testando grafo de exemplo do livro Algoritmos 3rd (Cormen), página 480.");
            Dijkstra(g, origem);

            calcula_caminho(vertice_alvo, caminho);
            if (caminho.Count > 0){
                imprime_caminho(caminho, origem, vertice_alvo);
            }
            
            Console.WriteLine("");
            Console.WriteLine("Bellman-Ford");

            bool sucesso =  false;

            g = new Grafo(true);

            g.inserir_vertice("a");
            g.inserir_vertice("b");
            g.inserir_vertice("c");
            g.inserir_vertice("d");
            g.inserir_vertice("e");

            g.inserir_aresta("a", "b", 6);
            g.inserir_aresta("a", "c", 7);
            g.inserir_aresta("a", "e", 2);
            g.inserir_aresta("b", "d", 5);
            g.inserir_aresta("b", "c", 8);
            g.inserir_aresta("b", "e", -4);
            g.inserir_aresta("c", "d", -3);
            g.inserir_aresta("c", "e", 9);
            g.inserir_aresta("d", "b", -2);
            g.inserir_aresta("e", "d", 7);

            caminho = new ArrayList();
            origem = g.get_vertice("a");
            vertice_alvo = g.get_vertice("e");

            Console.WriteLine("Testando grafo de exemplo do livro Algoritmos 3rd (Cormen), página 474.");
            sucesso = BellmanFord(g, origem);

            if (sucesso)
            {
                calcula_caminho(vertice_alvo, caminho);
                if (caminho.Count > 0)
                {
                    imprime_caminho(caminho, origem, vertice_alvo);
                }
            }
            else{
                Console.WriteLine("Tem ciclo negativo não é possível calcular os caminhos mínimos a partir de uma fonte determinada.");
            }

            Console.WriteLine("");
            Console.WriteLine("Bellman-Ford com ciclo negativo");

            g = new Grafo(true);

            g.inserir_vertice("a");
            g.inserir_vertice("b");
            g.inserir_vertice("c");
            g.inserir_vertice("d");
            g.inserir_vertice("e");

            g.inserir_aresta("a", "c", 2);
            g.inserir_aresta("c", "b", 1);
            g.inserir_aresta("b", "a", -4);
            g.inserir_aresta("b", "e", 5);
            g.inserir_aresta("d", "e", 7);
            g.inserir_aresta("c", "d", 2);
            g.inserir_aresta("e", "a", 8);            

            caminho = new ArrayList();
            origem = g.get_vertice("a");
            vertice_alvo = g.get_vertice("d");

            sucesso = BellmanFord(g, origem);

            if (sucesso) {
                calcula_caminho(vertice_alvo, caminho);
                if (caminho.Count > 0){
                    imprime_caminho(caminho, origem, vertice_alvo);
                }
            }
            else{
                Console.WriteLine("Tem ciclo negativo não é possível calcular os caminhos mínimos a partir de uma fonte determinada.");
            }

            Console.ReadKey();  
        }

    }
}
