﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;


// ******************************************************
// *********** CPU side data structures *****************
// ******************************************************

public class Triangle {
    public int[] vertices;

    public Triangle(int v0, int v1, int v2) {
        vertices = new int[3];
        vertices[0] = v0;
        vertices[1] = v1;
        vertices[2] = v2;
    }
}

public class Edge {
    public int startIndex;
    public int endIndex;

    public Edge(int start, int end) {
        startIndex = Mathf.Min(start, end);
        endIndex = Mathf.Max(start, end);
    }
}

public class EdgeComparer : EqualityComparer<Edge> {
    public override int GetHashCode(Edge obj) {
        return obj.startIndex * 10000 + obj.endIndex;
    }

    public override bool Equals(Edge x, Edge y) {
        return x.startIndex == y.startIndex && x.endIndex == y.endIndex;
    }
}

public enum DampingMethod { noDamping, simpleDamping, smartDamping }
public enum BendingMethod { noBending, DihedralBending, isometricBending }
public enum PointConstraintType { none, topRow, topCorners, leftRow, leftCorners, custom }


// ******************************************************
// *********** GPU side data structures *****************
// ******************************************************

// TODO: might not need this at all. can just use setvector
struct VectorStruct {
    public Vector3 data;
}

struct UInt3Struct {
    public uint deltaXInt;
    public uint deltaYInt;
    public uint deltaZInt;
}

struct EdgeStruct {
    public int startIndex;
    public int endIndex;
};

struct DistanceConstraintStruct {
    public EdgeStruct edge;
    public float restLength;
    public float weight;
};