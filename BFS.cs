using System;
using System.Collections.Generic;

namespace algoritmos_busqueda_bfs {
    class BFS {

        int[] currState = {1, 1, 1, 1};
        List<int[]> offspring;
        Queue<int[]> frontier = new Queue<int[]>();

        /*static void Main(string[] args) {
            BFS reinas = new BFS();
            reinas.frontier.Enqueue(reinas.currState);
            Console.WriteLine(string.Join(", ", reinas.breadthFirst(reinas.frontier, 0)));
        }*/

        int[] breadthFirst(Queue<int[]> frontier, int atacksGoal) {

            currState = frontier.Dequeue();

            if(goalTest(currState, atacksGoal)) {
                return currState;
            } else {
                offspring = expand(currState);
                foreach(int[] mov in offspring) {
                    frontier.Enqueue(mov);
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
                listTemp.Add(new int[cs.Length]);
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
