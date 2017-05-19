package br.ufv.dpi.lines2blocks;

import java.io.FileNotFoundException;

public class Line2Blocks {

    public static void main(String args[]) 
    {
        try {
            Point2Line points2Line = new Point2Line("dataset1");
            points2Line.readPoint("pontos");
            points2Line.createBase();
            points2Line.createXml();
            points2Line.printScenery();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

}
