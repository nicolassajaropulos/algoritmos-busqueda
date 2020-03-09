using System;
using System.Linq;
using System.Collections.Generic;

namespace algoritmos_busqueda_bi {

  class Bidirectional {

    Random random = new Random();
    Node rootA;
    Node rootB;

    Node tmpNode;

    List<char[,]> movementsA = new List<char[,]>();
    List<char[,]> movementsB = new List<char[,]>();

    char[,] currState = new char[3, 3];

    char[,] finalState = new char[3, 3]{
      {'1', '2', '3'},
      {'4', '5', '6'},
      {'7', '8', 'X'}
    };

    Queue<Node> frontierA = new Queue<Node>();
    Queue<Node> frontierB = new Queue<Node>();

    public Bidirectional() {
      rootA = new Node('A', null);
      rootB = new Node('B', null);
    }

    static void Main(string[] args) {
      Bidirectional bi = new Bidirectional();

      bi.startGame();

      bi.currState = new char[3, 3]{
        {'X', '1', '3'},
        {'4', '2', '6'},
        {'7', '5', '8'}
      };

      bi.rootA.value = bi.currState;
      bi.rootB.value = bi.finalState;

      bi.frontierA.Enqueue(bi.rootA);
      bi.frontierB.Enqueue(bi.rootB);

      bi.biSearch(bi.frontierA.Dequeue(), bi.frontierB.Dequeue());

      //Console.WriteLine(string.Join(", ", bi.currState));
    }

    void biSearch(Node nodeA, Node nodeB) {
      
      Node nodeMatched = goalTest(nodeA, nodeB);

      if(nodeMatched != null) {
        Console.WriteLine("Resultado Encontrado");

        if(nodeMatched.treeParent == 'A') {
          fillMovoments(movementsA, nodeMatched);
          fillMovoments(movementsB, nodeB);
        } else {
          fillMovoments(movementsA, nodeA);
          fillMovoments(movementsB, nodeMatched);
        }

        movementsA.Reverse();

        foreach(char[,] mov in movementsA) {
          printMov(mov);
        }

        foreach(char[,] mov in movementsB) {
          printMov(mov);
        }

        return;
      } else {
        expand(nodeA);
        expand(nodeB);

        foreach(Node child in nodeA.children) {
            frontierA.Enqueue(child);
        }

        foreach(Node child in nodeB.children) {
            frontierB.Enqueue(child);
        }
      }

      biSearch(frontierA.Dequeue(), frontierB.Dequeue());
    }

    void startGame() {

      currState = finalState.Clone() as char[,];

      int lengthRow = currState.GetLength(1);

      for (int i = currState.Length - 1; i > 0; i--) {
          int i0 = i / lengthRow;
          int i1 = i % lengthRow;

          int j = random.Next(i + 1);
          int j0 = j / lengthRow;
          int j1 = j % lengthRow;

          char temp = currState[i0, i1];
          currState[i0, i1] = currState[j0, j1];
          currState[j0, j1] = temp;
      }
    }

    void expand(Node node) {
      char[,] currElement;
      List<Node> nodeExpand = new List<Node>();
      char tempNumber;

      for(int x = 0; x < node.value.GetLength(0); x++) {
        for(int y = 0; y < node.value.GetLength(1); y++) {
          if(node.value[x, y] == 'X') {
            if(y > 0) {
              currElement = node.value.Clone() as char[,];
              tempNumber = currElement[x, y - 1];
              currElement[x, y] = tempNumber;
              currElement[x, y - 1] = 'X';
              node.children.Add(new Node(node.treeParent, node, currElement));
            }
            if(y < 2) {
              currElement = node.value.Clone() as char[,];
              tempNumber = currElement[x, y + 1];
              currElement[x, y] = tempNumber;
              currElement[x, y + 1] = 'X';
              node.children.Add(new Node(node.treeParent, node, currElement));
            }
            if(x > 0) {
              currElement = node.value.Clone() as char[,];
              tempNumber = currElement[x - 1, y];
              currElement[x, y] = tempNumber;
              currElement[x - 1, y] = 'X';
              node.children.Add(new Node(node.treeParent, node, currElement));
            }
            if(x < 2) {
              currElement = node.value.Clone() as char[,];
              tempNumber = currElement[x + 1, y];
              currElement[x, y] = tempNumber;
              currElement[x + 1, y] = 'X';
              node.children.Add(new Node(node.treeParent, node, currElement));
            }
            return;
          }
        }
      }
    }

    Node goalTest(Node nodeA, Node nodeB) {

      tmpNode = valueInTree(nodeA.value, rootB);
      if(tmpNode != null) {
        return tmpNode;
      }

      tmpNode = valueInTree(nodeB.value, rootA);

      if(tmpNode != null) {
        return tmpNode;
      }

      return null;
      
    }

    Node valueInTree(char[,] mov, Node root) {

      foreach(Node node in root.children) {
        if(mov.Cast<char>().SequenceEqual(node.value.Cast<char>())) {
          return node;
        } else {
          return valueInTree(mov, node);
        }
      }

      return null;
    }

    void fillMovoments(List<char[,]> movList, Node node) {

      movList.Add(node.value);

      if(node.parent == null) {
        return;
      }

      fillMovoments(movList, node.parent);
    }

    void printMov(char[,] matrix) {
      Console.WriteLine("------------------------ START MOVEMENT -----------------------");
      for (int i = 0; i < matrix.GetLength(0); i++) {
        for (int j = 0; j < matrix.GetLength(1); j++) {
          Console.Write(matrix[i,j] + "\t");
        }
        Console.WriteLine();
      }
      Console.WriteLine("------------------------ END MOVEMENT -----------------------");
    }

  }

}