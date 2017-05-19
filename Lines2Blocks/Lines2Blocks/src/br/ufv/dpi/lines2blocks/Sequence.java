/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package br.ufv.dpi.lines2blocks;

/**
 *
 * @author dpimestrado
 */
public class Sequence {

    private String blockname;
    private int beginx;
    private int beginy;
    private int endx;
    private int endy;
    private double degree;

    public Sequence(String bn, int bx, int by, int ex, int ey, double dg) {
        this.setBlockname(bn);
        this.setBeginx(bx);
        this.setBeginy(by);
        this.setEndx(ex);
        this.setEndy(ey);
        this.setDegree(dg);
    }

    public void setBlockname(String blockname) 
    {
        this.blockname = blockname;
    }

    public String getBlockname() 
    {
        return blockname;
    }

    public void setBeginx(int beginx)
    {
        this.beginx = beginx;
    }

    public int getBeginx() 
    {
        return beginx;
    }

    public void setBeginy(int beginy)
    {
        this.beginy = beginy;
    }

    public int getBeginy() 
    {
        return beginy;
    }

    public void setEndx(int endx) 
    {
        this.endx = endx;
    }

    public int getEndx() 
    {
        return endx;
    }

    public void setEndy(int endy) 
    {
        this.endy = endy;
    }

    public int getEndy()
    {
        return endy;
    }
        public void setDegree(double degree) 
    {
        this.degree = degree;
    }
            public double getDegree()
    {
        return degree;
    }
}
