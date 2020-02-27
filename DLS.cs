using System;
using System.Collections.Generic;

namespace algoritmos_busqueda_dfs {
    class DLS {
        public KeyValuePair<int, int[]> currStateLvl = new KeyValuePair<int, int[]>(1, new int[]{1, 1, 1, 1});
        List<int[]> offspring;

        int[] currState;
        int currLevel;

        public int limit;

        public Stack<KeyValuePair<int, int[]>> frontier = new Stack<KeyValuePair<int, int[]>>();

        /* static void Main(string[] args) {
            DLS reinas = new DLS();
            reinas.limit = 8;
            reinas.frontier.Push(reinas.currStateLvl);
            reinas.depthLimitedSearch(reinas.frontier, reinas.limit, 0);
        } */

        public bool depthLimitedSearch(Stack<KeyValuePair<int, int[]>> frontier, int limit, int atacksGoal) {
            if(frontier.Count > 0){
              currStateLvl = frontier.Pop();

              currLevel = currStateLvl.Key;
              currState = currStateLvl.Value;

              if(goalTest(currState, atacksGoal)) {
                  Console.WriteLine(string.Join(", ", currState));
                  return true;
              } else if(limit > currLevel){
                  currLevel++;
                  offspring = expand(currState);
                  offspring.Reverse();
                  foreach(int[] mov in offspring) {
                      frontier.Push(new KeyValuePair<int, int[]>(currLevel, mov));
                  }
              }
              return depthLimitedSearch(frontier, limit, atacksGoal);
            } else {
              Console.WriteLine("No se encontraron resultados");
              return false;
            }
        }

        bool goalTest(int[] currState, int atacksGoal) {
            int numAtacks = 0;

            for(int i = 0; i < currState.Length; i++) {
                for(int j = i + 1; j < currState.Length; j++) {
                    if(currState[i] == currState[j]) {
                        numAtacks += 2;
                    }
                    if(Math.Abs(i - j) - Math.Abs(currState[i] - currState[j]) == 0) {
                        numAtacks += 2;
                    }
                }
            }
            return numAtacks == atacksGoal;
        }

        List<int[]> expand(int[] cs) {
            List<int[]> listTemp = new List<int[]>();
            int[] arrTemp;

            for(int i = 0; i < cs.Length; i++) {
                listTemp.Add(new int[4]);
                Array.Copy(cs, listTemp[i], cs.Length );

                for(int j = 0; j < cs.Length; j++) {
                    if(i == j) {
                        arrTemp = listTemp[i];
                        arrTemp[j] = arrTemp[j] + 1;
                        listTemp[i] = arrTemp;
                    }
                }
            }

            return listTemp;
        }
    }
}
