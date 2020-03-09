using System.Collections.Generic;

namespace algoritmos_busqueda_bi {

  class Node {

    public char treeParent {get; set;}

    public Node parent {get; set;}

    public List<Node> children = new List<Node>();

    public char[,] value {get; set;}

    public Node(char treeParent, Node parent) {
      this.treeParent = treeParent;
      this.parent = parent;
    }

    public Node(char treeParent, Node parent, char[,] value) {
      this.treeParent = treeParent;
      this.parent = parent;
      this.value = value;
    }

  }

}