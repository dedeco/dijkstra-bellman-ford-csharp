using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caminho_minimo
{
    public class Grafo
    {
        private SortedDictionary<string, Vertice> vertices = new SortedDictionary<string, Vertice>();
        private Boolean _direcionado = false;

        public Grafo(Boolean direcionado) {
            _direcionado = direcionado;
        }

        public void inserir_vertice(string id) {
            Vertice v = new Vertice(id);
            vertices[id] = v;
        }

        public void inserir_aresta(string de, string para, int peso) {
            Vertice p = vertices[para];
            Vertice d = vertices[de];
            vertices[de].inserir_vertice_adjacente(p, peso);
            if (!_direcionado) {                                   
                vertices[para].inserir_vertice_adjacente(d, peso);
            } 
        }

        public List<Tuple<Vertice, Vertice>> get_arestas() {
            List<Tuple<Vertice, Vertice>> arestas = new List<Tuple<Vertice, Vertice>>();            
            foreach (KeyValuePair<string, Vertice> u in vertices){
                foreach (KeyValuePair<Vertice, int> v in u.Value.get_adjacentes()){
                    arestas.Add(Tuple.Create(u.Value, v.Key));                    
                }
            }
            return arestas;
        }

        public Vertice get_vertice(string vertice){
            if (vertices.ContainsKey(vertice))
            {
                return vertices[vertice];
            }
            else {
                return null;
            }
        }
        public SortedDictionary<string, Vertice> get_vertices() {
            return vertices;
        }

        public void set_vertices(SortedDictionary<string, Vertice> vs)
        {
            vertices = vs;
        }    
    }

}
