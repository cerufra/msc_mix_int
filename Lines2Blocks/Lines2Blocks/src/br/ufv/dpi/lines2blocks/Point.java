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
public class Point {
    private int px;// cordenada x do ponto 
    private int py;// cordenada y do ponto 

	
	public Point(int x, int y)
	{
		this.setPx(x);
		this.setPy(y);
	}

	public int getPx() 
	{
		return px;
	}

	public void setPx(int px) 
	{
		this.px = px;
	}

	public int getPy() 
	{
		return py;
	}

	public void setPy(int py) 
	{
		this.py = py;
	}
	
        public void print(){
            System.out.println("Valor de x= "+getPx());
            System.out.println("Valor de y= "+getPy());
        }
}
