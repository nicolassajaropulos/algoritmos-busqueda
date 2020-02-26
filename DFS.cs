using System;
using System.Linq;
using System.Collections.Generic;

namespace algoritmos_busqueda_dfs {
    class DFS {
        int[] currState = {1, 1, 1, 1};
        List<int[]> offspring;
        Stack<int[]> frontier = new Stack<int[]>();

        static void Main(string[] args) {
            DFS reinas = new DFS();
            reinas.frontier.Push(reinas.currState);
            Console.WriteLine(string.Join(", ", reinas.breadthFirst(reinas.frontier, 0)));
        }

        int[] breadthFirst(Stack<int[]> frontier, int atacksGoal) {

            currState = frontier.Pop();

            if(goalTest(currState, atacksGoal)) {
                return currState;
            } else {
                offspring = expand(currState);
                offspring.Reverse();
                foreach(int[] mov in offspring) {
                    frontier.Push(mov);
                }
                return breadthFirst(frontier, atacksGoal);
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
